using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class IntroTransition : MonoBehaviour
{
    [SerializeField] private float m_introDelay;
    [Space, SerializeField] private float m_transitionShowTime;

    [Header("Events")]
    [SerializeField] private UnityEvent m_onComplete;

    private TransitionHandler m_handler;

    #region Unity Methods

    private void Awake()
    {
        m_handler = GetComponent<TransitionHandler>();
    }

    private void Start()
    {
        StartCoroutine(DoIntro());
    }

    #endregion Unity Methods

    public IEnumerator DoIntro()
    {
        yield return new WaitForSeconds(m_introDelay);
        m_handler.StartTransition(() => { }, () => m_onComplete?.Invoke(), m_transitionShowTime);
    }

}