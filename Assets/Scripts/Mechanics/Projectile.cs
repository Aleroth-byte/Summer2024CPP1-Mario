using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(float xVel, float yVel)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag("Floor"))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy") && CompareTag("PlayerProjectile"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile"))
        {
            GameManager.Instance.lives--;
            Destroy(gameObject);
        }
    }
}
