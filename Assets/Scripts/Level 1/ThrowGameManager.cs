using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ThrowGameManager : MonoBehaviour
{
    [SerializeField] private MoneyVisual m_visual;

    [Space, SerializeField] private float m_pullForce = 5f;

    [Space, SerializeField] private InputActionReference m_pull;

    [Space, SerializeField] private LayerMask m_layer;
    [SerializeField] private LayerMask m_onTableMask;
    [SerializeField] private float m_tableCheckDistance = 4;

    [Header("On Done")]
    [SerializeField] private float m_respawnDelay;

    [SerializeField] private float m_dropDelay;
    [SerializeField] private UnityEvent m_onOff;

    [Header("Respawn")]
    [SerializeField] private Transform m_spawnPoint;

    [SerializeField] private List<Transform> m_spawnables;

    private Ipullable m_currentTarget = null;
    private Transform m_target;
    private Transform m_pullPoint;
    private float m_lastDistance;
    private LineRenderer m_line;

    private bool m_isFinished = false;

    public bool Done { get; set; } = false;

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
        if (Done) return;

        if (m_currentTarget != null)
        {
            var dir = GetPullDirection();
            m_currentTarget.Pull(-dir, m_pullForce);
        }

        if (!IsOnTable() && !m_isFinished && m_target)
        {
            m_isFinished = true;
            StartCoroutine(FallOffTable(m_target.GetComponent<MeshRenderer>()));
            m_target = null;
            ClearItem(new());
            StartRespawn();
        }
    }

    #endregion Unity Methods

    private bool IsOnTable()
    {
        if (m_currentTarget == null || m_target == null) return false;

        if (Physics.Raycast(m_target.position + new Vector3(0, 2, 0), Vector3.down, Mathf.Infinity, m_onTableMask)) return true;

        return false;
    }

    public void StartNow() =>
        StartCoroutine(Respawn(0f));

    public void StopMiniGame()
    {
        if (Done) return;
        Done = true;
        var handler = GameObject.Find("Paw Transition").GetComponent<TransitionHandler>();
        var index = FindObjectOfType<GameOrder>().GetNextSceneIndex();

        handler.StartTransition(() => SceneManager.LoadScene(index));
    }

    public void StartRespawn()
    {
        if (m_currentTarget != null) return;
        Debug.Log("Start Respawn");
        StartCoroutine(Respawn(m_respawnDelay));
    }

    private IEnumerator Respawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        var clone = Instantiate(m_spawnables[Random.Range(0, m_spawnables.Count)]);
        var dish = clone.AddComponent<Dish>();
        clone.transform.position = m_spawnPoint.position;

        m_isFinished = false;
    }

    private Vector3 GetPullDirection() =>
    m_pullPoint.position - GetWorldPointFromScreen(m_lastDistance);

    private IEnumerator FallOffTable(Renderer target)
    {
        yield return new WaitForSeconds(m_dropDelay);
        m_onOff?.Invoke();
        CameraShake.Instance.StartShake(.2f, .1f);
        var points = VolumeToPoints(target.bounds.size);

        m_visual.Amount += points;
        ScoreManager.Instance.Score += points;
        ScoreManager.Instance.LostMoney += points;

        Debug.Log("Item has fallen off!");
    }

    private int VolumeToPoints(Vector3 size) =>
        Mathf.FloorToInt(size.x * size.y * size.z) / 10;

    private void GetItem(InputAction.CallbackContext cxt) =>
        m_currentTarget = GetPullable();

    private void ClearItem(InputAction.CallbackContext cxt)
    {
        if (IsOnTable()) return;
        if (m_pullPoint) Destroy(m_pullPoint.gameObject);
        if (m_currentTarget != null) m_currentTarget = null;
        if (m_pullPoint != null) m_pullPoint = null;
        if (m_line != null) m_line = null;
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
                pullPoint.transform.SetPositionAndRotation(point, Quaternion.identity);
                pullPoint.transform.parent = hit.transform;

                m_lastDistance = hit.distance;
                m_pullPoint = pullPoint.transform;
                m_target = hit.collider.gameObject.transform;

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
        if (m_target)
            Gizmos.DrawRay(new Ray(m_target.position, Vector3.down));
    }

    #endregion Gizmos
}