using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Réglages du mouvement")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        // On commence par aller vers le point B
        target = pointB.position;
    }

    void Update()
    {
        // Déplacement de la plateforme
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Si on atteint la destination, on change de cible
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    // --- L'ASTUCE POUR LE JOUEUR ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'objet qui touche la plateforme a le tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Quand le joueur saute ou quitte la plateforme, il reprend son indépendance
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
