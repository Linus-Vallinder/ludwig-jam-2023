using UnityEngine;

[ExecuteInEditMode]
public class PixelCamera : MonoBehaviour
{
    [Space, SerializeField] private int m_width = 500;
    [SerializeField] private int m_height = 500;
    [Space, SerializeField] private MeshRenderer m_quadRenderer;

    [Space, SerializeField] private Camera m_camera;
    [SerializeField] private Camera m_cameraRenderer;

    #region Unity Methods

    private void Awake() =>
        m_camera = GetComponent<Camera>();

    private void LateUpdate() =>
        Scale();

    #endregion Unity Methods

    private void Scale()
    {
        float targetAspectRatio = m_width / (float)m_height;
        float windowAspectRatio = Screen.width / (float)Screen.height;
        float scaleHeight = windowAspectRatio / targetAspectRatio;

        if (scaleHeight < 1f)
            m_quadRenderer.transform.localScale = new Vector3(targetAspectRatio * scaleHeight,
                                                              scaleHeight,
                                                              1f);
        else
            m_quadRenderer.transform.localScale = new Vector3(targetAspectRatio,
                                                              1f,
                                                              1f);
    }

    public Vector3 ScreenToWorldPosition(Vector3 screenPosition)
    {
        int targetWidth = Screen.width;
        int targetHeight = Screen.height;

        float xScaleFactor = (float)targetWidth / m_width;
        float yScaleFactor = (float)targetHeight / m_height;
        float scalefactor = Mathf.Min(xScaleFactor, yScaleFactor);

        targetWidth = (int)(m_width * scalefactor);
        targetHeight = (int)(m_height * scalefactor);

        Vector3 offset = new(
            (Screen.width - targetWidth) / 2,
            (Screen.height - targetHeight) / 2,
            0.0f);

        Vector3 correctedPosition = (screenPosition - offset) / scalefactor;

        return m_camera.ScreenToWorldPoint(correctedPosition);
    }
}