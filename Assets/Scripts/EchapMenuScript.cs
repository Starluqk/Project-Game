using TMPro;
using UnityEngine;

public class EchapMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject Canva;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ButtonMainMenu()
    {
        GameManager.BackToMainMenu();
    }

    public void ButtonBack()
    {
        GameManager.CloseMenu();
    }

    public void RestartButton()
    {
        GameManager.ReloadScene();
    }

    
    
    
}
