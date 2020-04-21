using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("吃食物音效")] public AudioClip eatFoodSourceClip;
    private AudioSource eatFoodSource;

    [Header("吃尾巴音效")] public AudioClip eatSelfSourceClip;
    private AudioSource eatSelfSource;

    [Header("成功音效")] public AudioClip successSourceClip;
    private AudioSource successSource;

    [Header("失败音效")] public AudioClip failSourceClip;
    private AudioSource failSource;

    [Header("背景音乐")] public AudioClip backgroundSourceClip;
    private AudioSource backgroundSource;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        eatFoodSource = gameObject.AddComponent<AudioSource>();
        eatFoodSource.clip = eatFoodSourceClip;

        eatSelfSource = gameObject.AddComponent<AudioSource>();
        eatSelfSource.clip = eatSelfSourceClip;

        successSource = gameObject.AddComponent<AudioSource>();
        successSource.clip = successSourceClip;

        failSource = gameObject.AddComponent<AudioSource>();
        failSource.clip = failSourceClip;

        backgroundSource = gameObject.AddComponent<AudioSource>();
        backgroundSource.clip = backgroundSourceClip;

        PlayBackground();
    }

    /**
     * 切换背景音乐 播放/暂停
     */
    public void ToggleBackgroundSound()
    {
        if (Instance.backgroundSource.isPlaying)
        {
            Instance.backgroundSource.Pause();
        }
        else
        {
            PlayBackground();
        }
    }

    public static void PlayBackground()
    {
        Instance.backgroundSource.volume = 0.3f;
        if (!Instance.backgroundSource.isPlaying)
            Instance.backgroundSource.Play();
    }

    public static void PlayEatFood()
    {
        if (!Instance.eatFoodSource.isPlaying)
            Instance.eatFoodSource.Play();
    }

    public static void PlayEatSelf()
    {
        if (!Instance.eatSelfSource.isPlaying)
            Instance.eatSelfSource.Play();
    }

    public static void PlaySuccess()
    {
        if (!Instance.successSource.isPlaying)
            Instance.successSource.Play();
    }

    public static void PlayFail()
    {
        if (!Instance.failSource.isPlaying)
            Instance.failSource.Play();
    }
}