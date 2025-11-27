using UnityEngine;

public class Bergmonch : BaseEnemy
{
    [Header("Dash Settings")]
    public float dashSpeed = 30f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    public float dashDetectionRange = 25f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;


    private Transform player;

    private void Update()
    {
        player = FindPlayer();

        if (player == null) return;

        TurnTowardsTarget(player.position);

        float distance = Vector3.Distance(transform.position, player.position);
        if (!isDashing && dashCooldownTimer <= 0f && distance < dashDetectionRange)
        {
            StartDash(player.position);
        }

        DashMovement();
    }

    private void StartDash(Vector3 targetPosition)
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        dashDirection = targetPosition - transform.position;
        dashDirection.Scale(new Vector3(Random.Range(1f, 5f), 0f, Random.Range(1f, 5f)));
        dashDirection.Normalize();
    }

    private void DashMovement()
    {
        if (!isActive) return;

        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        Vector3 move = Vector3.zero;

        if (isDashing)
        {
            move = dashDirection * dashSpeed * Time.deltaTime;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
                isDashing = false;
        }


        velocity.y += gravity * Time.deltaTime;
        move += velocity * Time.deltaTime;

        if (knockbackTimer > 0f)
        {
            move += knockbackVelocity * Time.deltaTime;
            knockbackTimer -= Time.deltaTime;
        }

        controller.Move(move);

        if (controller.isGrounded)
            velocity.y = 0f;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("player hit)");

                Vector3 dir = collider.transform.position - transform.position;
                dir.y = 0f; // No vertical knockback

                player.ApplyKnockback(dir);
            }
        }
    }
}
