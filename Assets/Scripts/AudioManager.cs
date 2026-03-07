using UnityEngine;

public enum AudioType
{ 
    death,
    ennemieDeath,
    fireballLaunch,
    fireballWallHit,
    levelEnd,
    jump,
    transformation,
    dash,
    fireballWallBreak,
    getKey,
    step
}

public enum AudioSourceType
{
    game,
    player,
    musicSource,
    stepSource
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager Instance;
    public float volume = 1f;
    public float volumeMusic = 1f;
    public AudioSource gameSource;
    public AudioSource playerSource;
    public AudioSource musicSource;
    public AudioSource stepSource;

    [System.Serializable]
    public struct AudioData
    {
        public AudioClip clip;
        public AudioType type;
    }
    
    public AudioData[] audioData;
    
    private static MusicManager instance;
    void Awake()
    {
        gameSource.volume = volume;
        playerSource.volume = volume;
        stepSource.volume = volume;
        musicSource.volume = volumeMusic;
        if (Instance == null)
        {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        }
        else
        {
        Destroy(gameObject);
         }
    }
    public void PlaySound(AudioType type, AudioSourceType sourceType)
    {
        AudioClip clip = getClip(type);
        if (sourceType == AudioSourceType.game)
        {
            gameSource.PlayOneShot(clip);
        }
        else if (sourceType == AudioSourceType.player)
        {
            playerSource.PlayOneShot(clip);
        }
        else if (sourceType == AudioSourceType.musicSource)
        {
            musicSource.PlayOneShot(clip);
        }
        else if (sourceType == AudioSourceType.stepSource)
        {
            stepSource.PlayOneShot(clip);
        }

        
    }

    AudioClip getClip(AudioType type)
    {
        foreach (AudioData data in audioData)
        {
            if (data.type == type)
            {
                return data.clip;
            }
        }

        Debug.LogError("AudioMAnager : pas de clip trouvé pour le type " + type);
        return null;
    }
    
    
}