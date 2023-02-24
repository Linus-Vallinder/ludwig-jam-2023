using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    [SerializeField] private LayerMask m_layer;
    [Space, SerializeField] private InputActionReference m_click;

    #region Unity Methods

    private void Start()
    {
        m_click.action.performed += cxt =>
        {
            Click(GetClickable());
        };
    }

    private void OnDisable()
    {
        m_click.action.performed -= cxt =>
        {
            Click(GetClickable());
        };
    }

    #endregion Unity Methods

    private void Click(IClickable clickable)
    {
        if (clickable == null) return;
        clickable.Click();
    }

    public IClickable GetClickable()
    {
        var cam = PixelCamera.Instance;
        if (Physics.Raycast(cam.ScreenToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, m_layer))
        {
            if (hit.collider.TryGetComponent<IClickable>(out var clickable)) return clickable;
        }

        return null;
    }
}