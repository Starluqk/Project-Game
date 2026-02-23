using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHP;
    public int attack;
    public int defense;
    public int speed;

    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void ApplyStats(int hp, int atk, int def, int spd)
    {
        maxHP = hp;
        attack = atk;
        defense = def;
        speed = spd;

        currentHP = maxHP;
    }
}
