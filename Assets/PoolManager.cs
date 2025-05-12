using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] string targetTag;
    [SerializeField] GameObject prefab; // bullet prefab
    [SerializeField] GameObject enemyBullet; // enemy bullet prefab
    [SerializeField] GameObject enemyPrefab; // enemy prefab
    [SerializeField] List<GameObject> objectPool; // pool para las balas
    [SerializeField] List<GameObject> enemyBulletPool; // pool para las balas enemigas
    [SerializeField] List<GameObject> enemyPool; // pool para los enemigos

    private const int MaxEnemies = 6; // limite de enemigos

    // agrega un objeto al pool general
    public void AddToPool(GameObject obj)
    {
        objectPool.Add(obj);
    }

    // agrega una bala al pool
    public GameObject AddToPool(Vector2 position)
    {
        foreach (GameObject obj in objectPool)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                return obj;
            }
        }

        GameObject instance = Instantiate(prefab, position, Quaternion.identity);
        objectPool.Add(instance);
        return instance;
    }

    // agrega un objeto al pool de balas enemigas
    public void AddToEnemyBulletPool(GameObject obj)
    {
        enemyBulletPool.Add(obj);
    }
    
    // agrega una bala al pool de balas del enemigo
    public GameObject AddToEnemyBulletPool(Vector2 position)
    {
        foreach (GameObject obj in enemyBulletPool)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                return obj;
            }
        }

        GameObject instance = Instantiate(enemyBullet, position, Quaternion.identity);
        enemyBulletPool.Add(instance);
        return instance;
    }

    // agrega enemigos al pool
    public GameObject AddEnemyToPool(Vector2 position)
    {
        // revisa si hay enemigos inactivos en el pool
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                enemy.transform.position = position;
                return enemy;
            }
        }

        // si no hay enemigos disponibles y no hemos alcanzado el limite
        if (enemyPool.Count < MaxEnemies)
        {
            GameObject instance = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemyPool.Add(instance);
            return instance;
        }

        return null; // si ya tenemos 6 enemigos activos, no se agrega otro
    }

    // no se puede usar pool.Count por fuera (en el spawner), entonces use esto
    public int GetEnemies()
    {
        return enemyPool.Count;
    }



    // desactivar un enemigo
    public void DeactivateEnemy(GameObject enemy)
    {
        if (enemyPool.Contains(enemy))
        {
            enemy.SetActive(false);
        }
    }

    // desactivar balas
    public void Deacivate(GameObject obj) // jaja esta mal escrito
    {
        if(objectPool.Contains(obj))
        {
            obj.SetActive(false);
        }
    }

    // desactivar balas enemigas
    public void DeacivateEnemyBullet(GameObject obj) // seguimos el ejemplo
    {
        if(enemyBulletPool.Contains(obj))
        {
            obj.SetActive(false);
        }
    }

    // limpiar pools
    public void Clear()
    {
        objectPool.Clear();
        enemyPool.Clear();
    }
}
