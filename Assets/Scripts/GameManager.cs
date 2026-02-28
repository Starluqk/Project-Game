using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int NbLevels = 10;
    public static int currentLevel = 1;

    public static void ClickLevel(string level)
    {
        currentLevel = int.Parse(level);
        SceneManager.LoadScene("Level " + level);
        ShowLevelNumber();
    }

    public static void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public static void ShowLevels()
    {
        SceneManager.LoadScene("Select Level");
        ShowLevelNumber();
    }

    public static void NextLevel()
    {
        if (currentLevel < NbLevels)
        {
            currentLevel++;
            SceneManager.LoadScene("Level " + currentLevel.ToString());
            ShowLevelNumber();
        }
    }
    
    public static void ReloadScene()
    {
        // Recharge le niveau actuel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ShowLevelNumber()
    {
        Debug.Log(currentLevel);
    }
}
