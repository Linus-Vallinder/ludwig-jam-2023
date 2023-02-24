using TMPro;
using UnityEngine;

public class MoneyVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text;

    public int Amount
    {
        get => m_amount;
        set
        {
            m_amount = value;
        }
    }

    private int m_amount;
    private float m_speed;

    private float m_smooth;
    private int m_displayAmount;

    #region Unity Methods

    private void Update()
    {
        m_smooth = Mathf.SmoothDamp(m_smooth, (float)Amount, ref m_speed, 0.2f, Mathf.Infinity, Time.deltaTime);

        int toDisplay = (int)Mathf.Round(m_smooth);
        if (toDisplay != m_displayAmount)
        {
            m_displayAmount = toDisplay;
            m_text.text = $"${toDisplay}";
        }
    }

    #endregion Unity Methods
}