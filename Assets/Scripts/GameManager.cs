using System.Linq;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int NbLevels = 10;
    public static int currentLevel = 1;
    public static bool hasKey = false;
    public static bool alreadyLoad = false;

    // --- NOUVELLES VARIABLES DE COÛT ---
    public static int dashCost = 25;
    public static int fireballCost = 20;
    
    // Variable nombre mort
    public static int nbDeath = 0;

    public static void ClickLevel(string level)
    {
        currentLevel = int.Parse(level);
        SceneManager.LoadScene("Level " + level);
        ShowLevelNumber();
    }

    public static void PlayButton()
    {
        currentLevel = 1; // On reset au niveau 1
        SceneManager.LoadScene("Level 1");
        StartTransition();
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
            hasKey = false;
            currentLevel++;
            SceneManager.LoadScene("Level " + currentLevel.ToString());
            ShowLevelNumber();
            StartTransition();
        }
    }
    
    public static void ReloadScene()
    {
        OnCloseMenu();
        nbDeath++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartTransition();
    }

    public static void ShowLevelNumber()
    {
        Debug.Log("Niveau actuel : " + currentLevel);
    }

    public static void EchapMenu()
    {
        if (!alreadyLoad)
        {
            Time.timeScale = 0;
            alreadyLoad = true;
            SceneManager.LoadScene("Echap Menu", LoadSceneMode.Additive);
        }
        
    }

    public static void BackToMainMenu()
    {
        OnCloseMenu();
        SceneManager.LoadScene("Main Menu");
    }

    public static void CloseMenu()
    {
        OnCloseMenu();
        SceneManager.UnloadSceneAsync("Echap Menu");
    }

    public static void OnCloseMenu()
    {
        Time.timeScale = 1;
        alreadyLoad = false;
    }

    public static void StartTransition()
    {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
    }
    
    public static void EndTransition()
    {
        SceneManager.LoadScene("EndTransition", LoadSceneMode.Additive);
    }
    
}