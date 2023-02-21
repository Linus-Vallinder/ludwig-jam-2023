using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Manager Options")]
    [SerializeField] private GameObject m_sourcePrefab;

    private readonly List<GameObject> m_pool = new();

    #region Unity Methods

    private void Start()
    {
        for (int i = 0; i < 100; ++i)
        {
            GameObject audoObject = Instantiate(m_sourcePrefab, transform) as GameObject;
            m_pool.Add(audoObject);
        }
    }

    #endregion Unity Methods

    public void PlaySounds(AudioClip[] sounds, float volume, float pitch)
    {
        foreach (var sound in sounds)
        {
            PlaySound(sound, volume, pitch);
        }
    }

    public void PlayRandomSoundFromArray(AudioClip[] audioClips, float volume = 1.0f, float pitch = 1.0f)
    {
        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];
        if (audioClip != null)
        {
            PlaySound(audioClip, volume, pitch);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        GameObject audioObject = m_pool[0] as GameObject;
        
        if (audioObject.TryGetComponent<AudioSource>(out var audioSource))
        {
            m_pool.RemoveAt(0);
            audioSource.transform.position = Vector3.zero;
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
            StartCoroutine(ReturnToPool(audioObject));
        }
    }

    private IEnumerator ReturnToPool(GameObject audioObject)
    {
        yield return new WaitForSeconds(audioObject.GetComponent<AudioSource>().clip.length);
        m_pool.Add(audioObject);
    }
}
