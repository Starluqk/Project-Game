using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
  [Header("Parametres de patrouille")]
  public Transform destinationAEnemie;
  public Transform destinationBEnemie;
  public float speed = 3f;
  private Vector3 target;
  private SpriteRenderer spriteRenderer;
    void Start()
    {
        target = destinationBEnemie.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // deplacement vers la cible
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        // il  fait demi tour
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = ( target == destinationAEnemie.position) ? destinationBEnemie.position : destinationAEnemie.position ;
            Flip();
        }
    }

    void Flip()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            // recharger la scene sinon pour plus de facilite
        }
    }
}
