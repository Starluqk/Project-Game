using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public TextMeshProUGUI actionsText;

    // Update is called once per frame
    void Update()
    {
     int cout = GameManager.dashCost;
     int nbActions = PlayerController.StaminaDisplay / cout;
     actionsText.text = " Action left : " + nbActions.ToString();
    }
}
