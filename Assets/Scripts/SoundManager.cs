using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public AudioSource bgSound;
    public AudioSource bladeSound;
    public AudioSource guardSound;
    public AudioClip[] bgList;
    public AudioClip[] soundList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        BgSoundPlay(bgList[0]);
    }

    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                    instance = new SoundManager();
            }
            return instance;
        }
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.3f;
        bgSound.Play();
    }

    public void SoundPlay(AudioClip clip)
    {
        bladeSound.time = 0.3f;
        bladeSound.clip = clip;
        bladeSound.volume = 0.3f;
        bladeSound.Play();
    }

    public void GuardSoundPlay(AudioClip clip)
    {
        guardSound.clip = clip;
        guardSound.volume = 0.3f;
        guardSound.Play();
    }
}
