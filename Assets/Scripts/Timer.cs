using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent OnTimerStop;

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

        if (m_currentTime >= m_time && m_isTiming)
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
        OnTimerStop?.Invoke();
    }

    private void UpdateDisplay() =>
        m_display.text = $"{Mathf.Clamp(m_time - m_currentTime, 0, Mathf.Infinity):F1}";
}