// tag ennemie 
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;
    public GameObject impactVFX;

    void Start()
    {
        // Détruit la boule de feu après un certain temps
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. MUR DESTRUCTIBLE (On détruit tout)
        if (collision.CompareTag("MurDestructble"))
        {
            if (impactVFX != null)
            {
                Instantiate(impactVFX, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        // 2. GROUND (On détruit juste le projectile)
        if (collision.CompareTag("Ground"))
        {
            if (impactVFX != null)
            {
                Instantiate(impactVFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            AudioManager.Instance.PlaySound(AudioType.fireballWallHit, AudioSourceType.game);
            Debug.Log("Touché le sol/mur");
        }

        // 3. ENNEMIE (On détruit l'ennemi et le projectile)
        if (collision.CompareTag("Ennemie"))
        {
            if (impactVFX != null)
            {
                Instantiate(impactVFX, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
            AudioManager.Instance.PlaySound(AudioType.ennemieDeath, AudioSourceType.game);

        }

        // 4. DANGER / PICS (On détruit UNIQUEMENT le projectile)
        if (collision.CompareTag("Danger"))
        {
            if (impactVFX != null)
            {
                Instantiate(impactVFX, transform.position, Quaternion.identity);
            }
            // On NE détruit PAS collision.gameObject ici pour garder les pics
            Destroy(gameObject);
        }
    }
}