using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class Dish : MonoBehaviour, Ipullable
{
    private Rigidbody m_rigidBody;
    private MeshCollider m_collider;

    #region Unity Methods

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_collider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        m_collider.convex = true;
        gameObject.layer = 8;
    }

    #endregion Unity Methods

    void Ipullable.Pull(Vector3 direction, float force)
    {
        m_rigidBody.AddForce(direction.normalized * force, ForceMode.Force);
    }
}