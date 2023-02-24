using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private List<Key> m_keys = new();

    #region Unity Methods

    private void Start()
    {
        m_keys.ForEach(key => key.OnClick += this.OnClick);
    }

    private void OnDisable()
    {
        m_keys.ForEach(key => key.OnClick -= this.OnClick);
    }

    #endregion Unity Methods

    private void OnClick(Key key)
    {
        Debug.Log($"{key.name} has been clicked!");
    }
}