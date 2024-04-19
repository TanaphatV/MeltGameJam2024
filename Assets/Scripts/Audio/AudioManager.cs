using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] sfxSounds, bgmSounds;
    public AudioSource bgmSource, sfxSource, bgmSource2;

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private Coroutine blendCoroutine;

    [SerializeField] AudioClip testclip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM("Champ");
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgmSounds, x => x.soundName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            bgmSource.clip = s.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // Method to blend from current BGM to a new BGM
    [ContextMenu("Blend")]
    public void StartBlend()
    {
        if (blendCoroutine != null)
        {
            StopCoroutine(blendCoroutine);
        }
        blendCoroutine = StartCoroutine(BlendAudioSources());
    }
    private IEnumerator BlendAudioSources()
    {
        float timer = 0f;
        float initialVolume1 = bgmSource.volume;
        float initialVolume2 = 1.0f;
        bgmSource2.mute = false;

        while (timer < 1f)
        {
            float t = timer / 1f;
            bgmSource.volume = Mathf.Lerp(initialVolume1, 0f, t);
            bgmSource2.volume = Mathf.Lerp(0f, initialVolume2, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure volumes are set to the correct final values
        bgmSource.volume = 0f;
        bgmSource2.volume = initialVolume2;

        // Reset coroutine
        blendCoroutine = null;
    }
}
