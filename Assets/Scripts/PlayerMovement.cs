using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int Stamina;
    public static int StaminaDisplay;
    public int StaminaMax;
    public int dashCost;
    public int FireballCost;
    [Header("Switch Personnage")]
    public bool isPlayerOne = true; 
    public RuntimeAnimatorController animPerso1;
    public RuntimeAnimatorController animPerso2;

    [Header("Mouvement & Physique")]
    public float speed = 8f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private float moveInput;
    private SpriteRenderer sr;
    private Animator anim;

    [Header("Détection Sol")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool isGrounded;

    [Header("Dash (Perso 1 uniquement)")]
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private float gravitydefault;
    [SerializeField] private TrailRenderer tr;

    [Header("Système de Tir (Perso 2 uniquement)")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    private Vector2 lastDirection = Vector2.right;

    void Start()
    {
        StaminaDisplay = Stamina;
        Stamina = StaminaMax;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gravitydefault = rb.gravityScale;
        tr.emitting = false;
        
        UpdateCharacterAppearance();
    }

    void Update()
    {
        if (Stamina > StaminaMax)
        {
            Stamina = StaminaMax;
        }
        if (Stamina < 0)
        {
            Stamina = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isPlayerOne = !isPlayerOne;
            UpdateCharacterAppearance();
        }

        if (isDashing) return;

        moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("isGrounded", isGrounded);

        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // Direction pour le TIR (garde la diagonale)
        if (moveInput != 0 || verticalInput != 0)
            lastDirection = new Vector2(moveInput, verticalInput).normalized;
        else
            lastDirection = new Vector2(sr.flipX ? -1f : 1f, 0);

        // SAUT
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        // DASH (JOUEUR 1)
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDash && isPlayerOne)
        {
            if(Stamina - dashCost !> -1)
            {
                StartCoroutine(DashHorizontal());
                Stamina = Stamina - dashCost;
            }
        }

        // TIR (JOUEUR 2)
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPlayerOne)
        {
            if (Stamina - FireballCost !> -1)
            {
                Shoot();
                Stamina = Stamina - FireballCost;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void UpdateCharacterAppearance()
    {
        if (isPlayerOne && animPerso1 != null) anim.runtimeAnimatorController = animPerso1;
        else if (!isPlayerOne && animPerso2 != null) anim.runtimeAnimatorController = animPerso2;
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

        if (projRb != null)
        {
            projRb.linearVelocity = lastDirection * projectileSpeed;
            float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    IEnumerator DashHorizontal()
    {
        canDash = false;
        isDashing = true;
        
        rb.gravityScale = 0;

        // --- FORCE LE DASH HORIZONTAL UNIQUEMENT ---
        float horizontalDir = moveInput;
        if (horizontalDir == 0) {
            horizontalDir = sr.flipX ? -1f : 1f; // Dash vers où on regarde si pas d'input
        }
        
        rb.linearVelocity = new Vector2(horizontalDir * dashForce, 0f);
        // --------------------------------------------
        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rb.gravityScale = gravitydefault;
        rb.linearVelocity = new Vector2(0, 0); // Stop le perso après le dash
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}