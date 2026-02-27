using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;
    public GameObject impactVFX;
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
        { if(impactVFX!=null);
                     {
                         GameObject effect = Instantiate(impactVFX, transform.position, Quaternion.identity);
                     }
            Destroy(gameObject);
            Destroy(collision.gameObject);
           
        } if (collision.CompareTag("Ground"))
        {if(impactVFX!=null);
                              {
                                  GameObject effect = Instantiate(impactVFX, transform.position, Quaternion.identity);
                              }
            Destroy(gameObject);
            Debug.Log(" toucher le mur");
        }

        if (collision.CompareTag("Danger"))
        {if(impactVFX!=null);
            {
                GameObject effect = Instantiate(impactVFX, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}