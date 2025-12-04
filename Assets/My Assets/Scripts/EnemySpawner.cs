using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;  // Assign your enemy prefab here
    public float spawnRate = 2f;    // Seconds between spawns
    public int maxEnemies = 10;     // How many enemies can exist at once
    public float spawnRadius = 5f;  // Radius around the spawner where enemies appear

    private float nextSpawnTime;
    private int currentEnemyCount = 0;

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        currentEnemyCount++;

        // Track when an enemy is destroyed
        RandomMovement enemyScript = enemy.GetComponent<RandomMovement>();
        if (enemyScript != null)
        {
            enemyScript.onDeath += OnEnemyDeath;
        }
        
    }

    void OnEnemyDeath()
    {
        currentEnemyCount--;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
