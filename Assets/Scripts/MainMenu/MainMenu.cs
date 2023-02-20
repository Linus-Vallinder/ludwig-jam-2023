using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int m_gameIndex = 1;
    private TransitionHandler m_transition;

    #region Unity Methods

    private void Awake()
    {
        m_transition = TransitionHandler.Instance;
    }

    #endregion Unity Methods

    public void StartGame()
    {
        m_transition.StartTransition(() => SceneManager.LoadScene(m_gameIndex));
    }
}