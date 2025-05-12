using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f; // velocidad
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float shootInterval = .5f; // intervalo de disparo de .5 segundos
    private Vector2 respawnPosition = new Vector2(0, 6f); // lugar de respawn random en la parte de arriba
    private float screenBottom = -6f; // limite parte de abajo de la pantalla

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        // 3. Usa un InvokeRepeating para hacer que el enemigo siempre dispare cada x segundos que tu definas. 
        InvokeRepeating(nameof(Shoot), shootInterval, shootInterval); // cuando se prende el objeto en el pool, empieza a disparar
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Shoot)); // cuando se apaga el objeto en el pool, deja de disparar
    }

    // Update is called once per frame
    void Update()
    {
        // pa abajo
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // si se pasa, respawnea arriba
        if (transform.position.y <= screenBottom)
        {
            Respawn();
        }
    }

    // 2. Cuando lleguen a la parte de abajo, si el jugador no es capaz de dispararles, este automáticamente se va hacia arriba en una parte aleatoria horizontalmente, como si se hubiera re-spawneado.
    private void Respawn()
    {
        // respawn random arriba
        float randomX = Random.Range(-8f, 8f); // pos random eje x
        transform.position = new Vector2(randomX, 6f); // altura de respawn
    }

    // 3. También, si el jugador les dispara y atina, entonces deberá desactivarse a menos que el pool los vuelva a ocupar.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet")) // checa si choca con bala del player
        {
            // desactiva la bala
            PoolManager.Instance.Deacivate(collision.gameObject);

            // desactiva al enemigo
            PoolManager.Instance.DeactivateEnemy(gameObject);

            // puntos por matar enemigo
            GameManager.Instance.AddScore();
        }

        if (collision.CompareTag("Player")) // checa si choca con player
        {
            // -1 vida si choca contigo
            GameManager.Instance.PlayerHit();

            // desactiva al enemigo
            PoolManager.Instance.DeactivateEnemy(gameObject);
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.AddToEnemyBulletPool(bulletOrigin.position); // igualito que el de player.cs
        if (bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D bulletRB))
        {
            bulletRB.linearVelocityY = -bulletSpeed * Time.fixedDeltaTime; // nadamas aqui le ponemos negativo para que se vaya hacia abajo
        }
    }
}
