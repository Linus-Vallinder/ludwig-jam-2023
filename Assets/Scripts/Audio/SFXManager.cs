using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Manager Options")]
    [SerializeField] private GameObject sourcePrefab;

    private readonly List<GameObject> m_pool = new();

    #region Unity Methods

    private void Start()
    {
        for (var i = 0; i < 100; ++i)
        {
            var audioObject = Instantiate(sourcePrefab, transform) as GameObject;
            m_pool.Add(audioObject);
        }
    }

    #endregion Unity Methods

    public void PlaySounds(IEnumerable<AudioClip> sounds, float volume, float pitch)
    {
        foreach (var sound in sounds)
        {
            PlaySound(sound, volume, pitch);
        }
    }

    public void PlayRandomSoundFromArray(AudioClip[] audioClips, float volume = 1.0f, float pitch = 1.0f)
    {
        var audioClip = audioClips[Random.Range(0, audioClips.Length)];
        if (audioClip != null)
        {
            PlaySound(audioClip, volume, pitch);
        }
    }

    private void PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        var audioObject = m_pool[0] as GameObject;

        if (!audioObject.TryGetComponent<AudioSource>(out var audioSource)) return;
        
        m_pool.RemoveAt(0);
        audioSource.transform.position = Vector3.zero;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        StartCoroutine(ReturnToPool(audioObject));
    }

    private IEnumerator ReturnToPool(GameObject audioObject)
    {
        yield return new WaitForSeconds(audioObject.GetComponent<AudioSource>().clip.length);
        m_pool.Add(audioObject);
    }
}
