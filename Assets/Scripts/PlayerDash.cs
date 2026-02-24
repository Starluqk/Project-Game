using System.Collections;
using UnityEngine;
// ne pas utiliser
public class PlayerDash : MonoBehaviour
{
    

    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
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
       yield return new WaitForSeconds(dashDuration);
       isDashing = false;
       yield return new WaitForSeconds(dashCooldown);
       canDash = true;

   }
}

   
