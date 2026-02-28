using System.Collections;
using UnityEngine;
// ne pas utiliser
public class PlayerDash : MonoBehaviour
{
    

    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    private bool isDashing;
 
    private bool canDash;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.A) &&  canDash)
        {
            
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah");
            StartCoroutine(Dash());
        }
    }

   IEnumerator Dash()
   {
       canDash = false;
       isDashing = true;
       float direction = transform.localScale.x;
       rb.linearVelocity = new Vector2(direction*dashForce,0);
       tr.emitting = true;
       yield return new WaitForSeconds(dashDuration);
       tr.emitting = false;
       isDashing = false;
       yield return new WaitForSeconds(dashCooldown);
       canDash = true;

   }
}

   
