using System;
using UnityEngine;

public class Key : MonoBehaviour, IClickable
{
    public event Action<Key> OnClick;

    public void Click()
    {
        OnClick?.Invoke(this);
    }
}