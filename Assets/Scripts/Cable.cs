using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour, Ipullable
{
    [SerializeField] private float m_minPullPower = 1f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem m_sparkEffect;
    [Space, SerializeField] private AudioClip m_pullClip;
    
    [Space, SerializeField] private List<Material> m_mats = new();

    //Objects that will interact with the rope
    [Space] public Transform whatTheRopeIsConnectedTo;

    public Transform whatIsHangingFromTheRope;

    //A list with all rope sections
    public List<Vector3> allRopeSections = new();

    //Rope data
    private float ropeLength = 1f;

    private float minRopeLength = 1f;
    private float maxRopeLength = 20f;

    //Mass of what the rope is carrying
    private float loadMass = 100f;

    //How fast we can add more/less rope
    private float winchSpeed = 2f;

    //The joint we use to approximate the rope
    private SpringJoint springJoint;

    #region Unity Methods

    private void Start()
    {
        if (whatTheRopeIsConnectedTo)
        {
            springJoint = whatTheRopeIsConnectedTo.GetComponent<SpringJoint>();

            if (!springJoint)
                springJoint = whatTheRopeIsConnectedTo.AddComponent<SpringJoint>();

            //Add the weight to what the rope is carrying
            whatIsHangingFromTheRope.GetComponent<Rigidbody>().mass = loadMass;
        }

        //Init the line renderer we use to display the rope
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.material = m_mats[Random.Range(0, m_mats.Count)];

        //Init the spring we use to approximate the rope from point a to b
        UpdateSpring();
    }

    private void Update()
    {
        if (whatIsHangingFromTheRope == null || whatIsHangingFromTheRope == null) return;

        //Add more/less rope
        UpdateWinch();

        //Display the rope with a line renderer
        DisplayRope();
    }

    #endregion Unity Methods

    #region Cable Rendering

    //Update the spring constant and the length of the spring
    private void UpdateSpring()
    {
        //Someone said you could set this to infinity to avoid bounce, but it doesnt work
        //kRope = float.inf

        //
        //The mass of the rope
        //
        //Density of the wire (stainless steel) kg/m3
        float density = 7750f;
        //The radius of the wire
        float radius = 0.02f;

        float volume = Mathf.PI * radius * radius * ropeLength;

        float ropeMass = volume * density;

        //Add what the rope is carrying
        ropeMass += loadMass;

        //
        //The spring constant (has to recalculate if the rope length is changing)
        //
        //The force from the rope F = rope_mass * g, which is how much the top rope segment will carry
        float ropeForce = ropeMass * 9.81f;

        //Use the spring equation to calculate F = k * x should balance this force,
        //where x is how much the top rope segment should stretch, such as 0.01m

        //Is about 146000
        float kRope = ropeForce / 0.01f;

        //print(ropeMass);

        //Add the value to the spring
        if (springJoint != null)
        {
            springJoint.spring = kRope * 1.0f;
            springJoint.damper = kRope * 0.8f;

            //Update length of the rope
            springJoint.maxDistance = ropeLength;
        }
    }

    //Display the rope with a line renderer
    private void DisplayRope()
    {
        //This is not the actual width, but the width use so we can see the rope
        float ropeWidth = 0.2f;

        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;

        //Update the list with rope sections by approximating the rope with a bezier curve
        //A Bezier curve needs 4 control points
        Vector3 A = whatTheRopeIsConnectedTo.position;
        Vector3 D = whatIsHangingFromTheRope.position;

        //Upper control point
        //To get a little curve at the top than at the bottom
        Vector3 B = A + whatTheRopeIsConnectedTo.up * (-(A - D).magnitude * 0.1f);
        //B = A;

        //Lower control point
        Vector3 C = D + whatIsHangingFromTheRope.up * ((A - D).magnitude * 0.5f);

        //Get the positions
        BezierCurve.GetBezierCurve(A, B, C, D, allRopeSections);

        //An array with all rope section positions
        Vector3[] positions = new Vector3[allRopeSections.Count];

        for (int i = 0; i < allRopeSections.Count; i++)
        {
            positions[i] = allRopeSections[i];
        }

        //Just add a line between the start and end position for testing purposes
        //Vector3[] positions = new Vector3[2];

        //positions[0] = whatTheRopeIsConnectedTo.position;
        //positions[1] = whatIsHangingFromTheRope.position;

        //Add the positions to the line renderer
        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positions);
    }

    //Add more/less rope
    private void UpdateWinch()
    {
        bool hasChangedRope = false;

        //More rope
        if (Input.GetKey(KeyCode.O) && ropeLength < maxRopeLength)
        {
            ropeLength += winchSpeed * Time.deltaTime;

            hasChangedRope = true;
        }
        else if (Input.GetKey(KeyCode.I) && ropeLength > minRopeLength)
        {
            ropeLength -= winchSpeed * Time.deltaTime;

            hasChangedRope = true;
        }

        if (hasChangedRope)
        {
            ropeLength = Mathf.Clamp(ropeLength, minRopeLength, maxRopeLength);

            //Need to recalculate the k-value because it depends on the length of the rope
            UpdateSpring();
        }
    }

    #endregion Cable Rendering

    public void Pull(Vector3 direction, float force)
    {
        if (!(direction.magnitude * force > m_minPullPower)) return;
        
        FindObjectOfType<CableMiniGameGeneric>().RemoveCable(this);
        SFXManager.Instance.PlaySound(m_pullClip, Random.Range(.95f, 1.05f), Random.Range(.95f, 1.15f));
        var clone = Instantiate(m_sparkEffect, transform.position, Quaternion.identity);
        clone.Play();
    }
}