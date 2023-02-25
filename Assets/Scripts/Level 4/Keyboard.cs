using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private List<Key> m_keys = new();

    [Header("SFX")] [SerializeField] private List<AudioClip> m_clicks = new();

    [Header("Word List")] [SerializeField] private List<string> m_words = new();
    [SerializeField] private TMP_Text m_WPM;
    
    private int m_wordsTyped;

    #region Unity Methods

    private void Start() =>
        m_keys.ForEach(key => key.OnClick += this.OnClick);

    private void OnDisable() =>
        m_keys.ForEach(key => key.OnClick -= this.OnClick);

    #endregion Unity Methods

    public IEnumerable<Key> GetActiveKeys() =>
        m_keys.Where(key => key.IsHighlighted == true);

    public void HighlightRandomKey()
    {
        var keys = m_keys.Where(key => !key.IsHighlighted).ToList();
        keys[Random.Range(0, keys.Count)].IsHighlighted = true;
    }

    public int GetScore() =>
        m_wordsTyped * 4;

    private void OnClick(Key key)
    {
        Debug.Log($"{key.name} has been clicked!");

        SFXManager.Instance.PlayRandomSoundFromArray(m_clicks.ToArray(), Random.Range(.95f, 1.05f), Random.Range(.95f, 1.05f));
        
        if (!key.IsHighlighted) return;
        key.IsHighlighted = false;
        
        m_wordsTyped++;
        m_WPM.text += $" {m_words[Random.Range(0, m_words.Count)]}";
        
        CameraShake.Instance.StartShake(.1f, .075f);
    }
}