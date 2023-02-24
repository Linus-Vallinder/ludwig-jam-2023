using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private List<Key> m_keys = new();

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

    private void OnClick(Key key)
    {
        Debug.Log($"{key.name} has been clicked!");

        if (key.IsHighlighted)
        {
            key.IsHighlighted = false;
            CameraShake.Instance.StartShake(.1f, .075f);
        }
    }
}