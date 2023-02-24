using UnityEngine;
using UnityEngine.InputSystem;

public class Pull : MonoBehaviour
{
    [SerializeField] private LayerMask m_layer;
    [Space, SerializeField] private InputActionReference m_pull;

    private Transform m_pullPoint;
    private Transform m_pullTarget;
    private float m_lastDistance;

    private Ipullable m_pullable;

    #region Unity Methods

    private void Start()
    {
        m_pull.action.performed += cxt =>
        {
            m_pullable = GetPullable();
        };

        m_pull.action.canceled += cxt =>
        {
            if (m_pullPoint) Destroy(m_pullPoint.gameObject);
            m_pullTarget = null;
            m_pullable = null;
        };
    }

    private void OnDisable()
    {
        m_pull.action.performed -= cxt =>
        {
            m_pullable = GetPullable();
            if (m_pullable != null) Debug.Log("You pulled that shit good!");
        };

        m_pull.action.canceled -= cxt => { if (m_pullPoint) Destroy(m_pullPoint.gameObject); m_pullTarget = null; m_pullable = null; };
    }

    private void Update()
    {
        if (m_pullable != null && m_pullPoint != null)
        {
            m_pullable.Pull(GetPullDirection(), 1.5f);
        }
    }

    #endregion Unity Methods

    private Vector3 GetPullDirection() =>
        m_pullPoint.position - GetWorldPointFromScreen(m_lastDistance);

    public Vector3 GetWorldPointFromScreen(float dist) =>
        PixelCamera.Instance.ScreenToRay(Input.mousePosition).GetPoint(dist);

    public Ipullable GetPullable()
    {
        var cam = PixelCamera.Instance;
        if (Physics.Raycast(cam.ScreenToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, m_layer))
        {
            CameraShake.Instance.StartShake(.05f, .05f);

            if (hit.collider.GetComponent<Ipullable>() != null)
            {
                var point = hit.point;

                var pullPoint = new GameObject("pull point");
                pullPoint.transform.SetPositionAndRotation(point, Quaternion.identity);
                pullPoint.transform.parent = hit.transform;

                m_lastDistance = hit.distance;
                m_pullPoint = pullPoint.transform;
                m_pullTarget = hit.collider.gameObject.transform;

                return hit.collider.GetComponent<Ipullable>();
            }
        }

        if (m_pullPoint != null) Destroy(m_pullPoint.gameObject);
        m_pullPoint = null;
        return null;
    }
}