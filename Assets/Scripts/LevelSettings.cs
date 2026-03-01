using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public int dashCostThisLevel = 25;
    public int fireCostThisLevel = 20;

    void Awake() // Awake s'exécute avant le Start du joueur
    {
        GameManager.dashCost = dashCostThisLevel;
        GameManager.fireballCost = fireCostThisLevel;
    }
}