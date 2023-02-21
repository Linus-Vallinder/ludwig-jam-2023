using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float m_time;

    [Header("Display")]
    [SerializeField] private TextMeshProUGUI m_display;

    private bool m_isTiming = false;
    private float m_currentTime;

    #region Unity Methods

    private void Update()
    {
        UpdateDisplay();

        if (m_isTiming)
            m_currentTime += Time.deltaTime;

        if (m_currentTime >= m_time)
            StopTimer();
    }

    #endregion Unity Methods

    public void StartTimer()
    {
        m_isTiming = true;
        m_currentTime = 0f;
    }

    public void StopTimer()
    {
        m_isTiming = false;
    }

    private void UpdateDisplay()
    {
    }
}