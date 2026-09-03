using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [Header("Spawner Típusa")]
    [SerializeField] private bool isGroundSpawner = false;

    [Header("Kezdeti idõközök")]
    [SerializeField] private float minSpawnTime = 1.5f;
    [SerializeField] private float maxSpawnTime = 3.0f;

    [Header("Nehezedési ütem")]
    [SerializeField] private float difficultyRate = 0.05f;
    [SerializeField] private float minPossibleTime = 0.6f;

    [SerializeField] private AudioClip tieSpawnSound;
    [SerializeField] private AudioSource audioSource;

    private float timer = 0f;
    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (minSpawnTime > minPossibleTime)
        {
            minSpawnTime -= difficultyRate * Time.deltaTime;
            maxSpawnTime -= difficultyRate * Time.deltaTime;
        }

        if (timer >= nextSpawnTime)
        {
            SpawnObstacle();
            timer = 0f;
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnObstacle()
    {
        float spawnY = isGroundSpawner ? -4.3f : Random.Range(-1f, 4.5f);

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        if (!isGroundSpawner && tieSpawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(tieSpawnSound);
        }
    }

    public void SetObstaclePrefab(GameObject newPrefab)
    {
        if (newPrefab != null)
        {
            obstaclePrefab = newPrefab;
        }
    }
}