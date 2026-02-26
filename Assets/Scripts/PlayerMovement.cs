using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Switch Personnage")]
    public bool isPlayerOne = true; // True = Dash / False = Shoot
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

    [Header("Système de Tir (Perso 2 uniquement)")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    private Vector2 lastDirection = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gravitydefault = GetComponent<Rigidbody2D>().gravityScale;
        
        UpdateCharacterAppearance();
    }

    void Update()
    {
        // 1. SWITCH DE PERSONNAGE (Touche Click droit)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isPlayerOne = !isPlayerOne;
            UpdateCharacterAppearance();
        }

        if (isDashing) return;

        // 2. RÉCUPÉRATION DES INPUTS
        moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("isGrounded", isGrounded);

        // 3. ORIENTATION
        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // Calcul de la direction (pour Dash ou Tir)
        if (moveInput != 0 || verticalInput != 0)
            lastDirection = new Vector2(moveInput, verticalInput).normalized;
        else
            lastDirection = new Vector2(sr.flipX ? -1f : 1f, 0);

        // 4. SAUT
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        // 5. DASH (RESTRINT AU JOUEUR 1)
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDash && isPlayerOne)
        {
            StartCoroutine(Dash());
        }

        // 6. TIR (RESTRINT AU JOUEUR 2 - Touche F)
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPlayerOne)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void UpdateCharacterAppearance()
    {
        // Change l'animator selon le perso actif
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

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Rigidbody2D gravity = GetComponent<Rigidbody2D>();
        gravity.gravityScale = 0;
        rb.linearVelocity = lastDirection * dashForce;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        gravity.gravityScale = gravitydefault;
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