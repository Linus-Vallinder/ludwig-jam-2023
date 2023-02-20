using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private float m_delay;

    private TransitionHandler m_transition;

    #region Unity Methods

    private void Awake()
    {
        m_transition = TransitionHandler.Instance;
    }

    private void Start()
    {
        LoadScreen();
    }

    #endregion Unity Methods

    public void LoadScreen()
    {
        StartCoroutine(DoLoad());
    }

    private IEnumerator DoLoad()
    {
        yield return new WaitForSeconds(m_delay);
        m_transition.StartTransition(() => SceneManager.LoadScene(1));
    }
}