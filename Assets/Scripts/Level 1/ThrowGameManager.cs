using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowGameManager : MonoBehaviour
{
    [SerializeField] private float m_pullForce = 5f;

    [Space, SerializeField] private InputActionReference m_pull;

    [Space, SerializeField] private LayerMask m_layer;

    private Ipullable m_currentTarget = null;
    private Transform m_pullPoint;
    private float m_lastDistance;

    #region Unity Methods

    private void OnEnable()
    {
        var action = m_pull.action;

        action.performed += GetItem;
        action.canceled += ClearItem;
    }

    private void OnDisable()
    {
        var action = m_pull.action;

        action.performed -= GetItem;
        action.canceled -= ClearItem;
    }

    private void Update()
    {
        if (m_currentTarget != null)
        {
            var dir = GetPullDirection();
            m_currentTarget.Pull(-dir, m_pullForce);
        }
    }

    #endregion Unity Methods

    private Vector3 GetPullDirection()
    {
        return m_pullPoint.position - GetWorldPointFromScreen(m_lastDistance);
    }

    private void GetItem(InputAction.CallbackContext cxt) =>
        m_currentTarget = GetPullable();

    private void ClearItem(InputAction.CallbackContext cxt)
    {
        if(m_pullPoint) Destroy(m_pullPoint.gameObject);
        if(m_currentTarget != null) m_currentTarget = null;
        if(m_pullPoint != null) m_pullPoint = null;
    }

    public Vector3 GetWorldPointFromScreen(float dist)
    {
        var cam = PixelCamera.Instance;
        Ray ray = cam.ScreenToRay(Input.mousePosition);

        return ray.GetPoint(dist);
    }

    public Ipullable GetPullable()
    {
        var cam = PixelCamera.Instance;
        if (Physics.Raycast(cam.ScreenToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, m_layer))
        {
            if (hit.collider.GetComponent<Ipullable>() != null)
            {
                var point = hit.point;
                var pullPoint = new GameObject("pull point");
                pullPoint.transform.position = point;
                pullPoint.transform.rotation = Quaternion.identity;
                pullPoint.transform.parent = hit.transform;

                m_lastDistance = hit.distance;
                m_pullPoint = pullPoint.transform;
                return hit.collider.GetComponent<Ipullable>();
            }
        }

        if (m_pullPoint != null) Destroy(m_pullPoint.gameObject);
        m_pullPoint = null;
        return null;
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
    }

    #endregion
}