using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        StartTransition();
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

    public static void QuitGame()
    {
        Application.Quit();
    }
    public static IEnumerator SequenceRestart()
    {
        EndTransition();
        yield return new WaitForSeconds(1f);
        ReloadScene();
    }
    
    public static IEnumerator SequenceClickButton(int mode, string lvl = "")
    {
        EndTransition();
        yield return new WaitForSeconds(1f);
        switch (mode)
        {
            case 1: SceneManager.LoadScene("Main Menu");
                break;
            case 2:
                PlayButton();
                break;
            case 3:
                ShowLevels();
                break;
            case 4:
                ClickLevel(lvl);
                break;
        }
    }
}