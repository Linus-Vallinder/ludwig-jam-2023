using System.Collections.Generic;
using UnityEngine;

public class GameOrder : MonoBehaviour
{
    [SerializeField] private List<int> m_miniGameOrder = new();

    private int m_currentIndex = 0;

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
}