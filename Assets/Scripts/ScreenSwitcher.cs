using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScreenSwitcher : MonoBehaviour
{
    [FormerlySerializedAs("m_screen")]
    [Header("Options")]
    [SerializeField] private Image screen;
    [Space, SerializeField] private float m_min, m_max;

    [Header("Screens")] [SerializeField] private List<Sprite> m_screens = new();

    private float m_currentDelay;
    private float m_currentTime = 0;
    
    #region  Unity Methods

    private void Start() =>
        SetNewRandomScreen();

    private void Update()
    {
        if (m_currentTime < m_currentDelay) 
            m_currentTime += Time.deltaTime;
        else SetNewRandomScreen();
    }

    #endregion

    private float GetTimeToNext() =>
        Random.Range(m_min, m_max);

    private Sprite GetRandomScreen()
    {
        var screens = m_screens.Where(x => screen.sprite != x).ToList();
        return screens[Random.Range(0, screens.Count)];
    }
    
    private void SetNewRandomScreen()
    {
        m_currentTime = 0;
        m_currentDelay = GetTimeToNext();
        screen.sprite = GetRandomScreen();
    }
}
