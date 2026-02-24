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
}
