using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip m_hover;
    [SerializeField] private AudioClip m_click;
    
    public void OnPointerEnter(PointerEventData eventData) =>
        SFXManager.Instance.PlaySound(m_hover, Random.Range(.95f, 1.05f), Random.Range(.8f, 1.2f));

    public void OnPointerClick(PointerEventData eventData) =>
        SFXManager.Instance.PlaySound(m_click, Random.Range(.95f, 1.05f), Random.Range(.8f, 1.2f));
}
