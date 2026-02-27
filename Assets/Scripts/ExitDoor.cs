using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On vérifie si c'est le joueur qui touche la porte
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Niveau terminé ! Passage au suivant...");
            GameManager.NextLevel();
        }
    }
}
