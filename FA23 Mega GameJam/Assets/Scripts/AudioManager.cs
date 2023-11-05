using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    
    //how long crossfades take, in seconds
    static readonly float FADE_TIME = 2f;
    
    //enum for current music track
    public enum MusicState
    {
        Menu,
        Aiming,
        InFlight,
        None
    }
    //enum for sfx
    public enum SoundEffect
    {
        Aim,
        Steer,
        Launch,
        Collect,
        Win,
        Lose,
        UIButton
    }
    //singleton
    public static AudioManager instance;

    //global volume settings
    public float masterVol = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    //music track audio sources. we need multiple for crossfading
    [SerializeField] private AudioSource menuMusicSource;
    [SerializeField] private AudioSource aimingMusicSource;
    [SerializeField] private AudioSource inFlightMusicSource;
    //sfx audio source
    [SerializeField] private AudioSource sfxSource;
    //music files
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private AudioClip aimingMusicClip;
    [SerializeField] private AudioClip inFlightMusicClip;
    //sfx files
    [SerializeField] private AudioClip aimSfxClip;
    [SerializeField] private AudioClip engineSfxClip;
    [SerializeField] private AudioClip launchSfxClip;
    [SerializeField] private AudioClip collectSfxClip;
    [SerializeField] private AudioClip winSfxClip;
    [SerializeField] private AudioClip loseSfxClip;
    [SerializeField] private AudioClip menuButtonSfxClip;

    //audio management stuff
    [SerializeField] private MusicState currentMusicState;
    //get the relevant AudioSource component being used to play music
    public AudioSource GetMusicSource(MusicState state)
    {
        switch (state)
        {
            case MusicState.Menu:
                return menuMusicSource;
            case MusicState.Aiming:
                return aimingMusicSource;
            case MusicState.InFlight:
                return inFlightMusicSource;
            default:
                return null;
        }
    }

    //singleton stuff and sound initialization
    void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //initialize sfx source
        sfxSource.volume = sfxVolume * masterVol;
        //initialize music tracks
        aimingMusicSource.clip = aimingMusicClip;
        inFlightMusicSource.clip = inFlightMusicClip;
        menuMusicSource.clip = menuMusicClip;
        aimingMusicSource.volume = 0;
        inFlightMusicSource.volume = 0;
        menuMusicSource.volume = 0;
        currentMusicState = MusicState.None;
        aimingMusicSource.loop = true;
        inFlightMusicSource.loop = true;
        menuMusicSource.loop = true;
        aimingMusicSource.Play();
        inFlightMusicSource.Play();
        menuMusicSource.Play();
    }

    //fades to the selected music track. you can crossfade to MusicState.None for a fadeout
    public void CrossfadeToTrack(MusicState newState)
    {
        StartCoroutine(FadeOut(currentMusicState));
        StartCoroutine(FadeIn(newState));
    }
    //coroutines for crossfade
    private IEnumerator FadeIn(MusicState state)
    {
        if (state != MusicState.None)
        {
            AudioSource source = GetMusicSource(state);
            float targetVolume = musicVolume * masterVol;
            float fadeAmount = targetVolume / FADE_TIME;
            while (source.volume < targetVolume)
            {
                source.volume += fadeAmount * Time.deltaTime;
                yield return null;
            }
            source.volume = targetVolume;
        }
        currentMusicState = state;
    }
    private IEnumerator FadeOut(MusicState state)
    {
        if (state != MusicState.None)
        {
            AudioSource source = GetMusicSource(state);
            float fadeAmount = source.volume / FADE_TIME;
            while (source.volume > 0)
            {
                source.volume -= fadeAmount * Time.deltaTime;
                yield return null;
            }
            source.volume = 0;
        }
    }

    //play the selected sound effect
    public void PlaySFX(SoundEffect sfx)
    {
        //update volume and prevent looping (just in case something funky happens)
        sfxSource.volume = sfxVolume * masterVol;
        sfxSource.loop = false;

        switch (sfx)
        {
            case SoundEffect.Aim:
                sfxSource.PlayOneShot(aimSfxClip);
                break;
            case SoundEffect.Steer:
                sfxSource.PlayOneShot(engineSfxClip);
                break;
            case SoundEffect.Launch:
                sfxSource.PlayOneShot(launchSfxClip);
                break;
            case SoundEffect.Collect:
                sfxSource.PlayOneShot(collectSfxClip);
                break;
            case SoundEffect.Win:
                sfxSource.PlayOneShot(winSfxClip);
                break;
            case SoundEffect.Lose:
                sfxSource.PlayOneShot(loseSfxClip);
                break;
            case SoundEffect.UIButton:
                sfxSource.PlayOneShot(menuButtonSfxClip);
                break;
            default:
                Debug.Log("Missing SFX Audio Clip");
                break;
        }
    }

    private void Update()
    {
        //testing stuff
        if (Input.GetKeyDown(KeyCode.M))
        {
            CrossfadeToTrack(MusicState.Menu);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            CrossfadeToTrack(MusicState.None);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            CrossfadeToTrack(MusicState.Aiming);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            CrossfadeToTrack(MusicState.InFlight);
        }
    }
}