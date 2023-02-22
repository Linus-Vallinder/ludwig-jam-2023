using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class Dish : MonoBehaviour, Ipullable
{
    private Rigidbody m_rigidBody;
    private MeshCollider m_collider;

    #region Unity Methods

    private void Awake()
    {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_collider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        transform.localScale = new(10, 10, 10);
        m_collider.convex = true;
        gameObject.layer = 8;
    }

    private void Update()
    {
        if (transform.position.y < -25f)
            Destroy(gameObject);
    }

    #endregion Unity Methods

    void Ipullable.Pull(Vector3 direction, float force)
    {
        m_rigidBody.AddForce(direction.normalized * force, ForceMode.Force);
    }
}