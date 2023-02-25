using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int Score
    {
        get
        {
            return m_score;
        }

        set
        {
            m_score = value;

            if (m_score > PlayerPrefs.GetInt("highscore"))
                PlayerPrefs.SetInt("highscore", m_score);

            Debug.Log($"Current Score: {value} | Current Highscore: {PlayerPrefs.GetInt("highscore")}");
        }
    }

    private int m_score;

    public int LostELO { get; set; }
    public int LostMoney { get; set; }
    public int LostViewers { get; set; }
    public int WordsTyped { get; set; }
}