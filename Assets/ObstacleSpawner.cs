using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnRate = 2f;      
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float randomY = Random.Range(-2f, 4f);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}