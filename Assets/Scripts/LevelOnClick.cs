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
        GameManager.PlayButton();
    }

    public void OnChooseLevel()
    {
        GameManager.ShowLevels();
    }
}
