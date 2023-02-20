using System.Collections;
using UnityEngine;

public delegate void TransitionDelegate();

public class TransitionHandler : Singleton<TransitionHandler>
{
    [Header("Transition Options")]
    [SerializeField] private float m_transitionTime;

    [Space, SerializeField] private string m_start;
    [SerializeField] private string m_end;

    [Space, SerializeField] private Animator m_animator;

    public void StartTransition(TransitionDelegate onComplete) =>
        StartCoroutine(DoTransition(onComplete));

    private IEnumerator DoTransition(TransitionDelegate onComplete)
    {
        m_animator.Play(m_start);
        yield return new WaitForSeconds(m_transitionTime);
        onComplete?.Invoke();

        m_animator.Play(m_end);
        yield return new WaitForSeconds(m_transitionTime);
    }
}