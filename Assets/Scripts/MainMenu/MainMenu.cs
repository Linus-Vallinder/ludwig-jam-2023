using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int m_gameIndex = 2;
    [Space, SerializeField] private Transform m_main;
    [SerializeField] private Transform m_settings;
    private TransitionHandler m_transition;

    #region Unity Methods

    private void Awake()
    {
        m_transition = SceneTransition.Instance;
    }

    private void Start()
    {
        m_main.gameObject.SetActive(true);
        m_settings.gameObject.SetActive(false);
    }

    #endregion Unity Methods

    public void StartGame()
    {
        var index = FindObjectOfType<GameOrder>().GetNextSceneIndex();
        m_transition.StartTransition(() => SceneManager.LoadScene(index));
    }

    public void OpenMain()
    {
        m_transition.StartTransition(() => Toggle(true, false));
    }

    public void OpenSettings()
    {
        m_transition.StartTransition(() => Toggle(false, true));
    }

    private void Toggle(bool m, bool s)
    {
        m_main.gameObject.SetActive(m);
        m_settings.gameObject.SetActive(s);
    }
}