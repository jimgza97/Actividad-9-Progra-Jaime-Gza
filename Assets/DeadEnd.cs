using UnityEngine;

public class DeadEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            PoolManager.Instance.Deacivate(collision.gameObject);
        }

        if(collision.CompareTag("EnemyBullet"))
        {
            PoolManager.Instance.DeacivateEnemyBullet(collision.gameObject);
        }
    }
}
