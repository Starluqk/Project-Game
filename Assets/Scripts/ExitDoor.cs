using System;
using System.Collections;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private Animator anim;
    private bool aEteTouchee = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On vérifie si c'est le joueur ET qu'on n'a pas déjà activé la porte
        if (collision.CompareTag("Player") && !aEteTouchee)
        {
            aEteTouchee = true;
            Debug.Log("Niveau terminé ! Le joueur entre dans la porte.");
            
            // On lance la coroutine en lui passant l'objet du joueur
            StartCoroutine(SequenceSortie(collision.gameObject));
        }
    }

    IEnumerator SequenceSortie(GameObject player)
    {
        // 1. On joue l'animation de la porte
        if (anim != null)
        {
            anim.SetTrigger("FERMETURE");
        }

        // 2. On fait disparaître le joueur
        // On désactive le SpriteRenderer (le visuel)
        if (player.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            sr.enabled = false;
        }

        // 3. On bloque la physique du joueur pour qu'il ne tombe pas ou ne bouge plus
        if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.simulated = false; 
        }

        // On attend que l'animation de la porte se termine
        yield return new WaitForSeconds(0.4f);

        // On change de niveau
        GameManager.NextLevel();
    }
}
