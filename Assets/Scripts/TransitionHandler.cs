using System.Collections;
using UnityEngine;

public delegate void TransitionDelegate();

public class TransitionHandler : MonoBehaviour
{
    [SerializeField] private AudioClip m_clip;
    [Space]
    public float m_transitionTime = 1.0f;

    public string m_start = "Start";
    public string m_end = "End";

    [Space, SerializeField] protected Animator m_animator;

    #region Start

    private void Awake()
    {
        if(m_animator == null)
            m_animator = GetComponent<Animator>();
    }

    #endregion Start

    public void StartTransition(TransitionDelegate onComplete) =>
        StartCoroutine(DoTransition(onComplete, () => { }, 0f));

    public void StartTransition(TransitionDelegate onComplete, TransitionDelegate onEnd, float duration) =>
        StartCoroutine(DoTransition(onComplete, onEnd, duration));

    private IEnumerator DoTransition(TransitionDelegate onComplete, TransitionDelegate onEnd, float wait)
    {
        m_animator.Play(m_start);
        SFXManager.Instance.PlaySound(m_clip, 1f, Random.Range(.95f, 1.05f));
        yield return new WaitForSeconds(m_transitionTime);
        onComplete?.Invoke();
        yield return new WaitForSeconds(wait);

        m_animator.Play(m_end);
        SFXManager.Instance.PlaySound(m_clip, 1f, Random.Range(.95f, 1.05f));
        yield return new WaitForSeconds(m_transitionTime);
        onEnd?.Invoke();
    }
}