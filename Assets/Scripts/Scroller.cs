using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [Header("Scroller Options")]
    [SerializeField, Range(0f, 1f)] private float m_speedX;

    [SerializeField, Range(0f, 1f)] private float m_speedY;

    private RawImage m_image;

    #region Unity Methods

    private void Awake() =>
        m_image = GetComponent<RawImage>();

    private void Update() =>
        m_image.uvRect = new Rect(m_image.uvRect.position + new Vector2(m_speedX, m_speedY) * Time.deltaTime,
                                  m_image.uvRect.size);

    #endregion Unity Methods
}