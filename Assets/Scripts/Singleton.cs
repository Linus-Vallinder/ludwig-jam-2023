using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<T>();
            return m_instance;
        }
    }
}