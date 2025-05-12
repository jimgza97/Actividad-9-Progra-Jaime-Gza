using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    [Header("Bullet Config")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] float bulletSpeed;

    Rigidbody2D rb;

    InputSystem_Actions input;

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Awake()
    {
        input = new();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input.Player.Attack.performed += ctx =>
        {
            GameObject bullet = PoolManager.Instance.AddToPool(bulletOrigin.position);
            if (bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D bulletRB))
            {
                bulletRB.linearVelocityY = bulletSpeed * Time.fixedDeltaTime;
            }
        };
    }

    private void FixedUpdate()
    {
        Vector2 move = input.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = move * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet")) // checa si choca con bala del enemigo
        {
            // desactiva la bala del enemigo
            PoolManager.Instance.DeacivateEnemyBullet(collision.gameObject);

            // toma daño
            GameManager.Instance.PlayerHit();
        }

    }
}
