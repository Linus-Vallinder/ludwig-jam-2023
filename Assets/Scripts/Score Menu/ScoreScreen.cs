using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private int m_gameIndex = 2;

    [Space, SerializeField] private TMP_Text m_viewers;
    [SerializeField] private TMP_Text m_elo;
    [SerializeField] private TMP_Text m_money;
    [SerializeField] private TMP_Text m_wpm;
    private TransitionHandler m_transition;

    #region Unity Methods

    private void Awake() =>
        m_transition = SceneTransition.Instance;

    private void Start()
    {
        m_viewers.text = $"{ScoreManager.Instance.LostViewers}";
        m_money.text = $"${ScoreManager.Instance.LostMoney}";
        m_elo.text = $"{ScoreManager.Instance.LostELO}";
        m_wpm.text = $"{ScoreManager.Instance.WordsTyped}";
    }

    #endregion Unity Methods

    public void LoadScene() =>
        m_transition.StartTransition(() => SceneManager.LoadScene(m_gameIndex));
}