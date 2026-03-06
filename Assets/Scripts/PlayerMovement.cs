using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Énergie")]
    public int Stamina;
    public static int StaminaDisplay;
    public int StaminaMax = 100;

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
    private Canvas canvas;
    
    [Header("vfx")]
    public GameObject switchvfx;

    void Start()
    {
        Stamina = StaminaMax;
        StaminaDisplay = Stamina;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gravitydefault = rb.gravityScale;
        if (tr != null) tr.emitting = false;
        canvas = FindAnyObjectByType<Canvas>();
        UpdateCharacterAppearance();
    }

    void Update()
    {
        // Mise à jour de l'affichage UI
        
        StaminaDisplay = Stamina;
        Stamina = Mathf.Clamp(Stamina, 0, StaminaMax);
        int coutActuel = GameManager.dashCost;
        int actionsRestantes = Stamina / coutActuel;
        
        // SWITCH PERSO (Clic Droit)
        if (Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.alreadyLoad)
        {
            isPlayerOne = !isPlayerOne;
            UpdateCharacterAppearance();
            AudioManager.Instance.PlaySound(AudioType.transformation, AudioSourceType.player);
            SpawnSwitchVFX();
        }

        if (isDashing) return;

        moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("isGrounded", isGrounded);

        if (moveInput > 0)
        {
            sr.flipX = false;
            ShowCanva();
            
            AudioManager.Instance.PlaySound(AudioType.step, AudioSourceType.player);
        }
        else if (moveInput < 0)

        {
            AudioManager.Instance.PlaySound(AudioType.step, AudioSourceType.player);
            sr.flipX = true;
            canvas.enabled = true;
        }
        
        

        // Direction Tir
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
            AudioManager.Instance.PlaySound(AudioType.jump, AudioSourceType.player);
            ShowCanva();
        }

        // --- ACTIONS (Clic Gauche) ---
        if (Input.GetKeyDown(KeyCode.Mouse1) && !GameManager.alreadyLoad)
        {
            ShowCanva();
            // DASH (Perso 1)
            if (isPlayerOne && canDash)
            {
                if (Stamina >= GameManager.dashCost) // Utilise la valeur du GameManager
                {
                    Stamina -= GameManager.dashCost;
                    StartCoroutine(DashHorizontal());
                    AudioManager.Instance.PlaySound(AudioType.dash, AudioSourceType.player);
                }
            }
            // TIR (Perso 2)
            else if (!isPlayerOne)
            {
                if (Stamina >= GameManager.fireballCost) // Utilise la valeur du GameManager
                {
                    Stamina -= GameManager.fireballCost;
                    Shoot();
                    AudioManager.Instance.PlaySound(AudioType.fireballLaunch, AudioSourceType.player);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            canvas.enabled = false;
            GameManager.EchapMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(GameManager.SequenceRestart());
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
        float horizontalDir = moveInput != 0 ? moveInput : (sr.flipX ? -1f : 1f);
        rb.linearVelocity = new Vector2(horizontalDir * dashForce, 0f);
        if (tr != null) tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        if (tr != null) tr.emitting = false;
        rb.gravityScale = gravitydefault;
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void ShowCanva()
    {
        canvas.enabled = true;
    }

    void SpawnSwitchVFX()
    {
        if (switchvfx != null)
        {
            Instantiate(switchvfx,transform.position, Quaternion.identity);
        }
    }
}