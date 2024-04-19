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
    public void BlendBGM(string name)
    {
        Sound newBGM = Array.Find(bgmSounds, x => x.soundName == name);
        if (newBGM == null)
        {
            Debug.Log("New BGM not found");
            return;
        }

        if (blendCoroutine != null)
        {
            StopCoroutine(blendCoroutine);
        }
        blendCoroutine = StartCoroutine(BlendBGMCoroutine(newBGM.clip));
    }
    [ContextMenu("Blend")]
    public void BlendTest()
    {
        blendCoroutine = StartCoroutine(BlendBGMCoroutine(testclip));
    }

    private IEnumerator BlendBGMCoroutine(AudioClip newBGMClip)
    {
        float timer = 0f;
        float initialVolume = bgmSource.volume;

        while (timer < bgmVolume / 2f)
        {
            float t = timer / (bgmVolume / 2f);
            float oldVolume = Mathf.Lerp(initialVolume, 0f, t);
            float newVolume = Mathf.Lerp(0f, bgmVolume, t);

            bgmSource.volume = oldVolume;
            bgmSource2.volume = newVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        // Switch to the new BGM track
        bgmSource.Stop();
        bgmSource.clip = newBGMClip;
        bgmSource.Play();

        // Continue blending to full volume for the new BGM track
        timer = 0f;
        while (timer < bgmVolume / 2f)
        {
            float t = timer / (bgmVolume / 2f);
            float oldVolume = Mathf.Lerp(0f, initialVolume, t);
            float newVolume = Mathf.Lerp(bgmVolume, 0f, t);

            bgmSource.volume = oldVolume;
            bgmSource2.volume = newVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        blendCoroutine = null;
    }
}
