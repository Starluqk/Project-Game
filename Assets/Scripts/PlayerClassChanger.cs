using UnityEngine;

public class PlayerClassChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite character1Sprite;
    public Sprite character2Sprite;

    private bool isCharacter1 = true;

    void Start()
    {
        spriteRenderer.sprite = character1Sprite;
    }

    void Update()
    {
        // Appuie sur E pour changer de personnage
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCharacter();
            AudioManager.Instance.PlaySound(AudioType.transformation, AudioSourceType.player);
        }
    }

    void SwitchCharacter()
    {
        if (isCharacter1)
        {
            spriteRenderer.sprite = character2Sprite;
            isCharacter1 = false;
        }
        else
        {
            spriteRenderer.sprite = character1Sprite;
            isCharacter1 = true;

        }
    }
}