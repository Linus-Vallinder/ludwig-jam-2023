using UnityEngine;
using UnityEngine.InputSystem;

public class ClickParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_particleSystem;

    [SerializeField] private LayerMask m_layer;
    [Space, SerializeField] private InputActionReference m_click;

    #region Unity Methods

    private void Start()
    {
        m_click.action.performed += cxt => Click();
    }

    private void OnDisable()
    {
        m_click.action.performed -= cxt => Click();
    }

    #endregion Unity Methods

    private void Click()
    {
        if (Physics.Raycast(PixelCamera.Instance.ScreenToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, m_layer))
        {
            var position = hit.point;
            Instantiate(m_particleSystem, position, Quaternion.identity);
        }
    }
}