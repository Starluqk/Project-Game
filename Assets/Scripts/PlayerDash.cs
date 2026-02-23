using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput * dashForce, 0);
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;
        Invoke(nameof(StopDash), dashDuration);
    }

    void StopDash()
    {
        isDashing = false;
    }
}
