using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardMiniGameLogic : MonoBehaviour
{
    [SerializeField] private int m_concurentKeys = 3;

    private Keyboard m_keyboard;
    private bool m_isGoing;

    #region Unity Methods

    private void Awake() =>
        m_keyboard = GetComponent<Keyboard>();

    private void Update()
    {
        if (m_isGoing)
        {
            if (m_keyboard.GetActiveKeys().Count() < 3)
                m_keyboard.HighlightRandomKey();
        }
    }

    #endregion Unity Methods

    public void StartMiniGame() =>
        m_isGoing = true;

    public void StopMiniGame()
    {
        m_isGoing = false;

        ScoreManager.Instance.Score += m_keyboard.GetScore();
        ScoreManager.Instance.WordsTyped += m_keyboard.GetScore();

        var handler = GameObject.Find("Paw Transition").GetComponent<TransitionHandler>();
        var index = FindObjectOfType<GameOrder>().GetNextSceneIndex();
        handler.StartTransition(() => SceneManager.LoadScene(index));
    }
}