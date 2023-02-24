using System;
using UnityEngine;

public class Key : MonoBehaviour, IClickable
{
    [SerializeField] private Transform m_highlightEffect;

    public bool IsHighlighted
    {
        get => m_highlighted;

        set
        {
            m_highlighted = value;
            Highlight();
        }
    }

    private bool m_highlighted = false;

    public event Action<Key> OnClick;

    #region Unity Methods

    private void Start() =>
        IsHighlighted = false;

    #endregion Unity Methods

    private void Highlight()
    {
        m_highlightEffect.gameObject.SetActive(IsHighlighted);
    }

    //Maybe to some tweening like we are moving the keys
    public void Click()
    {
        OnClick?.Invoke(this);
        if (m_highlighted)
            CameraShake.Instance.StartShake(.1f, .075f);
    }
}