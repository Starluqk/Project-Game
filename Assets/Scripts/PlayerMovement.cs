using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpBufferTime = 0.2f;

    [Header("Dash")]
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private float moveInput;

    private bool isGrounded;
    private bool isDashing = false;
    private bool canDash = true;
    
    private float jumpBufferCounter;
    private SpriteRenderer sr;
    
    [Header("Réglages Tir")]
    public Transform FirePoint;
    private float FirePointX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        // --- FIX : Assure-toi que le FirePoint n'est pas à 0 dans l'inspecteur ---
        FirePointX = FirePoint.localPosition.x;
        if (FirePointX == 0) FirePointX = 0.6f; // Valeur de secours si oublié
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Retourner le joueur et le point de tir
        if (moveInput > 0)
        {
            sr.flipX = false;
            FirePoint.localPosition = new Vector3(FirePointX, FirePoint.localPosition.y, FirePoint.localPosition.z);
            FirePoint.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
            FirePoint.localPosition = new Vector3(-FirePointX, FirePoint.localPosition.y, FirePoint.localPosition.z);
            FirePoint.localRotation = Quaternion.Euler(0, 180, 0);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && isGrounded && !isDashing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 dashDirection = new Vector2(horizontal, vertical);

        if (dashDirection == Vector2.zero)
        {
            // --- FIX : On utilise le flipX pour savoir où dasher par défaut ---
            float direction = sr.flipX ? -1f : 1f;
            dashDirection = new Vector2(direction, 0);
        }

        dashDirection.Normalize();
        rb.linearVelocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // --- ASTUCE : Voir le cercle de détection du sol ---
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}