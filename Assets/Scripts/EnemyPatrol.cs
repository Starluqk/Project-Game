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

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == destinationAEnemie.position) ? destinationBEnemie.position : destinationAEnemie.position;
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

  
}