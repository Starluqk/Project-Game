using UnityEngine;

public enum AudioType
{
    jump,
    death,
    dash,
    fireball,
}

public enum AudioSourceType
{
    game,
    player,
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager Instance;
    public float volume = 1f;
    public AudioSource gameSource;
    public AudioSource playerSource;

    [System.Serializable]
    public struct AudioData
    {
        public AudioClip clip;
        public AudioType type;
    }
    
    public AudioData[] audioData;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameSource.volume = volume;
        playerSource.volume = volume;
    }

    void PlaySound(AudioType type, AudioSourceType sourceType)
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