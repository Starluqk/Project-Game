using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int NbLevels = 10;

    public static void ClickLevel(string level)
    {
        SceneManager.LoadScene("Level " + level);
    }
}
