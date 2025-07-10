using UnityEngine;
/// <summary>
/// This is from Copilot
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] AudioClip selectingCard;
    [SerializeField] AudioClip pairingCard;
    [SerializeField] AudioClip falseCard;
    [SerializeField] AudioClip gameOver;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")] public AudioClip defaultBGM;

    private void Awake()
    {
        EventManager.Subscribe(EventNames.OnClickObject,PlaySelectingCard);
        EventManager.Subscribe(EventNames.OnScorePair,PlayScoreSound);
        EventManager.Subscribe(EventNames.OnFalsePair,PlayFalseSound);
        EventManager.Subscribe(EventNames.OnGameOver,PlayGameOver);
        
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Optional: Start default BGM
        if (defaultBGM != null)
            PlayBGM(defaultBGM);
    }

    // 🔊 Play background music
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    // 🔇 Stop background music
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 💥 Play sound effect
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }


    public void PlayScoreSound(object o)
    {
        PlaySFX(pairingCard);
    }

    public void PlayFalseSound(object o)
    {
        PlaySFX(falseCard);
    }

    public void PlaySelectingCard(object o)
    {
        PlaySFX(selectingCard);
    }

    public void PlayGameOver(object o)
    {
        PlaySFX(gameOver);
    }
}