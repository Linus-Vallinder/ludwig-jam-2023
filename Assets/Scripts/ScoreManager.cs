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
        }
    }

    private int m_score;
}
