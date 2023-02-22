using System.Collections.Generic;
using UnityEngine;

public class CableMiniGameGeneric : MonoBehaviour
{
    [SerializeField] private List<Transform> m_connections = new();

    [Space, SerializeField] private Cable m_cablePrefab;
}
