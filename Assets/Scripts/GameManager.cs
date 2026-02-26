using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int NbLevels = 10;

    public static void ClickLevel(string level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public static void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public static void ShowLevels()
    {
        SceneManager.LoadScene("Select Level");
    }
}
