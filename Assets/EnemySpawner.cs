using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 2f; // segundos para spawnear
    [SerializeField] float horizontalRange = 8f; // rango en el eje x
    [SerializeField] float spawnHeight = 6f; // altura donde spawnea

    private void SpawnEnemy()
    {
        // 5. El pool, a diferencia de las balas, tiene que tener un límite de 6 enemigos, por lo que si hay 6 enemigos que no se han podido eliminar, el spawner, al llegar a los dos segundos, deberá ignorar el comando de spawnear otro para evitar un overflow en el arreglo.
        // solo se spawnea un enemigo si el pool no esta lleno
        if (PoolManager.Instance.GetEnemies() < 6)
        {
            float randomX = Random.Range(-horizontalRange, horizontalRange);
            
            // 1. El comportamiento del spawner es que los enemigos deben instanciarse en un rango horizontal aleatorio antes de donde renderiza la cámara para que salgan desde la parte superior de la pantalla.
            Vector2 enemyOrigin = new Vector2(randomX, spawnHeight);
            PoolManager.Instance.AddEnemyToPool(enemyOrigin); // agrega un enemigo en el pool, en su coordenada
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 4. El spawner debe de estar creando o re-spawneando enemigos cada 2 segundos. 
        // spawnea enemigos cada intervalo, empieza al segundo 0 y spawnea cada spawnTime
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
