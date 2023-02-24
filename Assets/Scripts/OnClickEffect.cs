using System.Collections;
using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private float m_destroyDelay;

    #region Unity Methods

    private void Start() =>
        StartCoroutine(DestroySelf());

    #endregion Unity Methods

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(m_destroyDelay);
        Destroy(gameObject);
    }
}