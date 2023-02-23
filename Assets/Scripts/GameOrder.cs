using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOrder : MonoBehaviour
{
    [SerializeField] private int m_miniGameAmount = 10;
    [Space, SerializeField] private List<int> m_miniGameOrder = new();

    private List<int> m_order = new();
    private int m_currentIndex = 0;

    #region Unity Methods

    private void Start()
    {
        m_order = GenerateGameOrder().Take(m_miniGameAmount).ToList();
    }

    #endregion Unity Methods

    public int GetNextSceneIndex()
    {
        if (m_currentIndex >= m_miniGameOrder.Count)
        {
            m_currentIndex = 0;
            return 1;
        }
        var result = m_miniGameOrder[m_currentIndex];
        m_currentIndex++;
        return result;
    }

    private List<int> GenerateGameOrder()
    {
        var result = new List<int>();
        var tries = 0;

        while (result.Count < m_miniGameAmount || tries < 100)
        {
            var next = m_miniGameOrder[Random.Range(0, m_miniGameOrder.Count)];
            if (result.Count == 0)
            {
                result.Add(next);
                continue;
            }

            if (next == result.Last())
            {
                tries++;
                continue;
            }
            else if (next != result.Last())
            {
                result.Add(next);
                continue;
            }
        }

        return result;
    }
}