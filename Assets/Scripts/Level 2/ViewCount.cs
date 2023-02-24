using UnityEngine;
using TMPro;

public class ViewCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    public int Count { get; set; } = 10000;
    private float m_percentageMovementRange = .05f;
    private float m_min, m_max;

    private float m_target;
    private float m_current;

    #region Unity Methods

    private void Start()
    {
        m_min = 1f;
        m_max = 2f;

        ChangeNumber();
    }

    private void Update()
    {
        if(m_current < m_target)
        {
            m_current += Time.deltaTime;
            return;
        }
        else
        {
            ChangeNumber();
        }
    }

    #endregion

    private void ChangeNumber()
    {
        var movement = Random.Range(-m_percentageMovementRange, m_percentageMovementRange);
        var current = Count * (1 + movement);
        var timeTillNext = Random.Range(m_min, m_max);
        m_current = 0;
        m_target = timeTillNext;
        m_text.text = $"{Mathf.FloorToInt(current)}";
    }
}
