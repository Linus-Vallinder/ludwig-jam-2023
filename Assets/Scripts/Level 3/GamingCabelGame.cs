using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingCabelGame : MonoBehaviour
{
    private CableMiniGameGeneric m_miniGame;
    private Elo m_elo;

    #region Unity Methods

    private void Awake()
    {
        m_miniGame = GetComponent<CableMiniGameGeneric>();
        m_elo = FindObjectOfType<Elo>();
    }

    private void Start() =>
        m_miniGame.OnRemoveCable += OnPull;

    private void OnDisable() =>
        m_miniGame.OnRemoveCable -= OnPull;

    #endregion Unity Methods

    public void StopMiniGame()
    {
        ScoreManager.Instance.Score += m_elo.GetScore();
        var handler = GameObject.Find("Paw Transition").GetComponent<TransitionHandler>();
        var index = FindObjectOfType<GameOrder>().GetNextSceneIndex();

        handler.StartTransition(() => SceneManager.LoadScene(index));
    }

    private void OnPull()
    {
        CameraShake.Instance.StartShake(.1f, .25f);
        m_elo.LowerElo();
    }
}