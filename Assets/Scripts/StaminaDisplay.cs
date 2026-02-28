using UnityEngine;
using TMPro;

public class StaminaDisplay : MonoBehaviour
{
    public TMP_Text StaminaText;

    void DisplayStamina()
    {
        StaminaText.text = PlayerController.StaminaDisplay.ToString();
    }

    void update()
    {
        DisplayStamina();
    }
}
