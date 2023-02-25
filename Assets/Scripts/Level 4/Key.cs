using System;
using UnityEngine;

public class Key : MonoBehaviour, IClickable
{
    [SerializeField] private Transform m_highlightEffect;

    private Animator m_animator;
    
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

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start() =>
        IsHighlighted = false;

    #endregion Unity Methods

    private void Highlight() =>
        m_highlightEffect.gameObject.SetActive(IsHighlighted);

    public void Click()
    {
        m_animator.Play("Key");
        OnClick?.Invoke(this);
    }
}