using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CableMiniGameGeneric))]
public class RouterPullMiniGame : MonoBehaviour
{
    private CableMiniGameGeneric m_miniGame;
    private ViewCount m_count;

    #region Unity Methods

    private void Awake()
    {
        m_miniGame = GetComponent<CableMiniGameGeneric>();
        m_count = FindObjectOfType<ViewCount>();
    }

    private void Start() =>
        m_miniGame.OnRemoveCable += OnPull;

    private void OnDisable() =>
        m_miniGame.OnRemoveCable -= OnPull;

    #endregion Unity Methods

    public void StopMiniGame()
    {
        ScoreManager.Instance.Score += 10000 - m_count.Count;
        var handler = GameObject.Find("Paw Transition").GetComponent<TransitionHandler>();
        var index = FindObjectOfType<GameOrder>().GetNextSceneIndex();

        handler.StartTransition(() => SceneManager.LoadScene(index));
    }

    private void OnPull()
    {
        CameraShake.Instance.StartShake(.1f, .25f);
        m_count.Count -= 250;
    }
}