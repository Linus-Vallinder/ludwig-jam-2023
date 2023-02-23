using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CableMiniGameGeneric : MonoBehaviour
{
    [SerializeField] private List<Transform> m_connections = new();

    [Space, SerializeField] private Cable m_cablePrefab;
    [SerializeField] private Vector3 m_connectionOffset;

    private Dictionary<Transform, Cable> m_activeConnections = new();

    public event Action OnRemoveCable;

    public void SetupMiniGame()
    {
        AddRandomCableConnection();
        AddRandomCableConnection();
    }

    private bool IsFull() =>
        ActiveConnections() == m_connections.Count;

    private int ActiveConnections() =>
        m_activeConnections.Distinct().Count();

    private Transform GetRandomConnection()
    {
        var active = m_activeConnections.Distinct().Select(x => x.Key);
        var empty = m_connections.Where(x => !active.Contains(x));
        return empty.ToList()[Random.Range(0, empty.Count())];
    }

    private Cable SpawnCable() =>
        Instantiate(m_cablePrefab);

    private IEnumerator StartSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        AddRandomCableConnection();
    }

    public void RemoveCable(Cable cable)
    {
        var value = m_activeConnections.ContainsValue(cable);
        if (value == false) return;
        var key = m_connections.Where(x => x.position == cable.transform.position - m_connectionOffset).FirstOrDefault();
        m_activeConnections.Remove(key);

        OnRemoveCable?.Invoke();
        Destroy(cable.gameObject);

        StartCoroutine(StartSpawn(1f));
    }

    public (Cable, Transform) AddRandomCableConnection()
    {
        if (IsFull())
        {
            Debug.Log("No empty spaces left in the cable game!");
            return (null, null);
        }

        var connection = GetRandomConnection();
        var clone = SpawnCable();
        clone.transform.position = connection.position + m_connectionOffset;

        m_activeConnections.Add(connection, clone);

        return (clone, connection);
    }
}