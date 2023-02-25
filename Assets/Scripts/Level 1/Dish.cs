using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class Dish : MonoBehaviour, Ipullable
{
    private Rigidbody m_rigidBody;
    private MeshCollider m_collider;

    private float m_speed;
    private Vector3 m_target;

    private readonly float m_moveTime = .18f;
    private float m_current;
    
    #region Unity Methods

    private void Awake()
    {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_collider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        transform.localScale = new Vector3(10, 10, 10);
        m_collider.convex = true;
        gameObject.layer = 8;
    }

    private void Update()
    {
        if (transform.position.y < -25f)
            Destroy(gameObject);

        if (m_current < m_moveTime)
        {
            m_current += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, m_target, m_speed * Time.deltaTime);
        }
        
    }

    #endregion Unity Methods

    public void SetTarget(Vector3 position, float speed)
    {
        m_target = position;
        m_speed = speed;
    }
    
    void Ipullable.Pull(Vector3 direction, float force)
    {
        m_rigidBody.AddTorque(direction.normalized * 1.5f);
        m_rigidBody.AddForce(direction.normalized * force, ForceMode.Force);
    }
}