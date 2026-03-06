using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelOnClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelLabell;
    
    public void OnClick()
    {
        GameManager.ClickLevel(levelLabell.text);
    }

    public void OnPlay()
    {
        //GameManager.PlayButton();
        StartCoroutine(GameManager.SequenceClickButton(2));
    }

    public void OnChooseLevel()
    {
        StartCoroutine(GameManager.SequenceClickButton(3));
    }

    public void OnQuit()
    {
        GameManager.QuitGame();
    }
}
