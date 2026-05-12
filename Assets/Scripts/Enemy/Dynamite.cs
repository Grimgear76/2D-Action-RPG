using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.left;
    public float lifeSpan = 2;
    public float speed;
    public float lobStrength = 5f;
    public float verticalSpeedMultiplier = 1.5f;
    public LayerMask playerLayer;
    private int damage;

    public void Initialize(int damageAmount)
    {
        damage = damageAmount;
    }

    void Start()
    {
        rb.gravityScale = 1;

        float horizontalAmount = Mathf.Abs(direction.x);
        float verticalAmount = Mathf.Abs(direction.y);

        // Only boost speed when throwing upward, not downward
        bool throwingUp = direction.y > 0;
        float verticalSpeed = throwingUp
            ? speed * (1f + verticalAmount * (verticalSpeedMultiplier - 1f))
            : speed;

        Vector2 lobDirection = new Vector2(
            direction.x * speed,
            direction.y * verticalSpeed + lobStrength * horizontalAmount
        );

        rb.linearVelocity = lobDirection;
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(Mathf.Abs(rb.linearVelocity.y), Mathf.Abs(rb.linearVelocity.x)) * Mathf.Rad2Deg;

            if (rb.linearVelocity.x < 0)
                angle = 180 - angle;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            Destroy(gameObject);
        }
    }
}