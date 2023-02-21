using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float m_time;

    [Header("Display")]
    [SerializeField] private TextMeshProUGUI m_display;

    private float m_currentTime;

    #region Unity Methods

    #endregion
}
