using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Reaction
{
    Sad, Happy, Angry, Slime, QT
}

public class ReactionHandler : Singleton<ReactionHandler>
{
    [Header("Images")] 
    [SerializeField] private List<Sprite> m_sad = new();
    [SerializeField] private List<Sprite> m_happy = new();
    [SerializeField] private List<Sprite> m_angry = new();
    [Space, SerializeField] private List<Sprite> m_slime = new();
    [SerializeField] private List<Sprite> m_qt = new();

    [Header("References")]
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private Image m_image;
    [Space, SerializeField] private Transform m_cam;
    
    public static Action<Reaction, string> OnReact;
    public static Action OnStop;
    
    #region Unity Methods

    private void Start()
    {
        OnReact += React;
        OnStop += StopReact;
    }

    private void OnDisable()
    {
        OnReact -= React;
        OnStop += StopReact;
    }

    #endregion

    private void StopReact()
    {
        m_cam.GetComponent<Animator>().Play("Exit");
        m_text.text = string.Empty;;
        m_image.sprite = null;
        m_cam.gameObject.SetActive(false);
    }

    private void React(Reaction reaction, string message)
    {
        if (!m_cam.gameObject.activeInHierarchy)
        {
            m_cam.gameObject.SetActive(true);
            m_cam.GetComponent<Animator>().Play("Entry");
        }
        
        m_cam.gameObject.SetActive(true);
        SetReactionImage(reaction: reaction);
        SetMessageBubble(message);
    }

    private void SetMessageBubble(string message)
    {
        m_text.text = message;
    }
    
    private void SetReactionImage(Reaction reaction)
    {
        Sprite GetRandomSprite(IReadOnlyList<Sprite> sprites) =>
            sprites[Random.Range(0, sprites.Count)];

        m_image.sprite = reaction switch
        {
            Reaction.Sad => GetRandomSprite(m_sad),
            Reaction.Happy => GetRandomSprite(m_happy),
            Reaction.Angry => GetRandomSprite(m_angry),
            Reaction.Slime => GetRandomSprite(m_slime),
            Reaction.QT => GetRandomSprite(m_qt),
            _ => m_image.sprite
        };
    }
}
