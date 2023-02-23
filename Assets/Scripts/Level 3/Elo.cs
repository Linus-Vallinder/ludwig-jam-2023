using TMPro;
using UnityEngine;

public class Elo : MonoBehaviour
{
    [SerializeField] private TMP_Text m_elo;

    private int m_startElo = 600;
    private int m_currentElo;
    private int m_score;

    #region Unity Methods

    private void Awake()
    {
        m_startElo = Random.Range(600, 800);
        m_currentElo = m_startElo;
    }

    private void Update()
    {
        m_elo.text = m_currentElo.ToString();
    }

    #endregion Unity Methods

    public int GetScore() =>
        m_score;

    public void LowerElo()
    {
        var amount = Random.Range(8, 25);
        m_currentElo -= amount;
        m_score += Mathf.FloorToInt(amount * 8.6f);
    }
}