using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] sfxSounds, bgmSounds;
    public AudioSource sfxSource, bgmSourceNon, bgmSourceBronze, bgmSourceSilver, bgmSourceMinigame;

    public AudioSource currentAudioPlay;

    public AudioSource currentReputationBGM;

    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

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
        //PlayBGM("Champ");
        currentAudioPlay = bgmSourceNon;
        currentReputationBGM = bgmSourceNon;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSourceNon.volume = bgmVolume;
        bgmSourceBronze.volume = bgmVolume;
        bgmSourceSilver.volume = bgmVolume;
        bgmSourceMinigame.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }

    public void ToggleBGM()
    {
        bgmSourceNon.mute = !bgmSourceNon.mute;
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
            bgmSourceNon.clip = s.clip;
            bgmSourceNon.Play();
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
    public void StartBlendBGM(AudioSource prevBGMSource, AudioSource nextBGMSource)
    {
        if (blendCoroutine != null)
        {
            StopCoroutine(blendCoroutine);
        }
        if(prevBGMSource != nextBGMSource)
        {
            blendCoroutine = StartCoroutine(BlendAudioSources(prevBGMSource, nextBGMSource));
        }
    }
    private IEnumerator BlendAudioSources(AudioSource prevBGMSource, AudioSource nextBGMSource)
    {
        float timer = 0f;
        float initialVolume1 = prevBGMSource.volume;
        float initialVolume2 = bgmVolume;
        nextBGMSource.mute = false;

        while (timer < 1f)
        {
            float t = timer / 1f;
            prevBGMSource.volume = Mathf.Lerp(initialVolume1, 0f, t);
            nextBGMSource.volume = Mathf.Lerp(0f, initialVolume2, t);
            timer += Time.deltaTime;
            yield return null;
        }

        prevBGMSource.volume = 0f;
        prevBGMSource.mute = true;
        nextBGMSource.volume = initialVolume2;
        currentAudioPlay = nextBGMSource;

        blendCoroutine = null;
    }
}
