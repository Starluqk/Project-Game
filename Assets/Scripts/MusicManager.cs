using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        AudioManager.Instance.PlaySound(AudioType.music, AudioSourceType.music);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
