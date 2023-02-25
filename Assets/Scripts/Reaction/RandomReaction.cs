using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ReactionData
{
    [Header("Reaction Data")]
    public Reaction Reaction;
    [Space, TextArea] public string Message;
}

public class RandomReaction : MonoBehaviour
{
    [SerializeField] private List<ReactionData> m_reactions = new();

    [SerializeField] private float m_min, m_max;

    private bool m_stopped = true;
    
    private float m_target = 3;
    private float m_currentTime = 0;
    #region Unity Methods

    private void Start()
    {
        m_stopped = false;
        m_target = Random.Range(m_min, m_max);
    }

    private void Update()
    {
        if(m_stopped) return;
        
        if (m_currentTime < m_target) m_currentTime += Time.deltaTime;
        else if (m_currentTime > m_target)
        {
            m_target = Random.Range(m_min, m_max);
            m_currentTime = 0;

            ShowRandomReaction();
        }
    }

    #endregion
    
    private ReactionData GetRandomReaction() =>
        m_reactions[Random.Range(0, m_reactions.Count)];

    public void StopReactions()
    {
        m_stopped = true;
        ReactionHandler.Instance.StopReact();
    }
    
    public void ShowRandomReaction()
    {
        m_stopped = false;
        var reaction = GetRandomReaction();
        ReactionHandler.Instance.React(reaction.Reaction, reaction.Message);
    }
}
