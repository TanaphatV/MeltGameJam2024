using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] sfxSounds, bgmSounds;
    public AudioSource bgmSource, sfxSource;

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

        while (timer < bgmVolume)
        {
            float t = timer / bgmVolume;
            bgmSource.volume = Mathf.Lerp(initialVolume, 0f, t);
            timer += Time.deltaTime;
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = initialVolume;
        bgmSource.clip = newBGMClip;
        bgmSource.Play();
        blendCoroutine = null;
    }
}
