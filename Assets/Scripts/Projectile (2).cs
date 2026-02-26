using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MurDestructble"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        } if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log(" toucher le mur");
        }

        if (collision.CompareTag("Danger"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}