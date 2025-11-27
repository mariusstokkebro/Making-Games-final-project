using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip backgroundMusicCalm;
    [SerializeField] private AudioClip backgroundMusicCalmChants;
    [SerializeField] private AudioClip backgroundMusicMid;
    [SerializeField] private AudioClip backgroundMusicIntense;

    private float lastPlayTime = 0f;
    private const float minInterval = 0.03f;
    private AudioClip lastClip;
    private Coroutine musicFadeRoutine;

    public void FadeToMusic(AudioClip newClip, float fadeDuration = 0.25f)
    {
        if (musicFadeRoutine != null)
            StopCoroutine(musicFadeRoutine);

        musicFadeRoutine = StartCoroutine(FadeMusicRoutine(newClip, fadeDuration));
    }

    private IEnumerator FadeMusicRoutine(AudioClip newClip, float duration)
    {
        float startVolume = musicSource.volume;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, startVolume, t / duration);
            yield return null;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (Time.time - lastPlayTime < minInterval && clip == lastClip)
            return;

        lastPlayTime = Time.time;
        lastClip = clip;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlayAttackSound(AudioClip clip)
    {
        sfxSource.volume = 0.5f;
        sfxSource.PlayOneShot(clip);
    }

    public void UpdateFloorMusic(int floor)
    {
        Debug.Log($"Floor: {floor}");
        switch (floor)
        {
            case 0:
                FadeToMusic(backgroundMusicCalm);
                break;
            case 1:
                FadeToMusic(backgroundMusicCalmChants);
                break;
            case 2:
                FadeToMusic(backgroundMusicMid);
                break;
            case 3:
            default:
                FadeToMusic(backgroundMusicIntense);
                break;
        }
    }

    public void PlayMenuMusic()
    {
        PlayMusic(backgroundMusicCalm);
    }
}
