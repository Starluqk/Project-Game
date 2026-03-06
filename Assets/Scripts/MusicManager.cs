using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Start()
    {
        AudioManager.Instance.PlaySound(AudioType.music, AudioSourceType.musicSource);
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
