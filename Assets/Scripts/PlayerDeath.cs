using UnityEngine;
using UnityEngine.SceneManagement; // Pour pouvoir recharger la scène

public class PlayerDeath : MonoBehaviour
{
    public GameObject exploisionPrefab;
    private bool estMort = false;
    private SpriteRenderer sr;
private Rigidbody2D rb;

void Start()
{
    sr = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
}
    // Détection pour les objets solides (Ennemis, Pics au sol)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger") ||collision.gameObject.CompareTag("Ennemie"))
        {
            Mourir();
            AudioManager.Instance.PlaySound(AudioType.death, AudioSourceType.game);
        }
    }

    // Détection pour les zones invisibles (Zones de chute, Triggers)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Danger"))
        {
            Mourir();
            AudioManager.Instance.PlaySound(AudioType.death, AudioSourceType.game);
        }
    }

    public void Mourir()
    {
        if (estMort) return; // Évite de mourir plusieurs fois d'un coup

        estMort = true;
        Debug.Log("Mort ! Rechargement...");

        // 1. On désactive les mouvements pour que le joueur ne puisse plus bouger
        GetComponent<PlayerController>().enabled = false;
        

        if (exploisionPrefab != null)
        {
            Instantiate(exploisionPrefab, transform.position, Quaternion.identity);
        }
        //sr.enabled = false;
        rb.simulated = false;

        // 4. On attend 1 seconde avant de recharger 
        Invoke("ReloadScene", 1f);
    }

    void ReloadScene()
    {
        // Recharge le niveau actuel
        Debug.Log("Reload de la scène ! Opa");
        GameManager.ReloadScene();
    }
}
