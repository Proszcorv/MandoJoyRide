using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Spawner Típusa")]
    [SerializeField] private bool isGroundSpawner = false;

    [Header("Kezdeti idõközök")]
    [SerializeField] private float minSpawnTime = 1.5f;
    [SerializeField] private float maxSpawnTime = 3.0f;

    [Header("Nehezedési ütem")]
    [SerializeField] private float difficultyRate = 0.05f;
    [SerializeField] private float minPossibleTime = 0.6f;

    [SerializeField] private AudioClip tieSpawnSound;
    [SerializeField] private AudioSource tieaudioSource;

    [SerializeField] private AudioClip projectileSpawnSound;
    [SerializeField] private AudioSource projectileAudioSource;

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
        float spawnY = isGroundSpawner ? -4.3f : Random.Range(-1f, 5.5f);

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        GameObject spawnedObj = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        if (projectilePrefab != null)
        {
            if (isGroundSpawner)
            {
                if (Random.value > 0.3f)
                {
                    StartCoroutine(SpawnProjectileDelayed(spawnedObj, true));
                }
            }
            else
            {
                StartCoroutine(SpawnProjectileDelayed(spawnedObj, false));
            }
        }



        if (!isGroundSpawner && tieSpawnSound != null && tieaudioSource != null)
        {
            tieaudioSource.PlayOneShot(tieSpawnSound);
        }
    }

    public void SetObstaclePrefab(GameObject newPrefab)
    {
        if (newPrefab != null)
        {
            obstaclePrefab = newPrefab;
        }
    }

    public void SetProjectilePrefab(GameObject newProjectilePrefab)
    {
        if (newProjectilePrefab != null)
        {
            projectilePrefab = newProjectilePrefab;
        }
    }

    public void SetProjectileSound(AudioClip newSound)
    {
        projectileSpawnSound = newSound;
    }

    IEnumerator SpawnProjectileDelayed(GameObject parentObj, bool isGround)
    {
        float delay = isGround ? Random.Range(3f, 4.5f) : Random.Range(0.3f, 0.7f);

        yield return new WaitForSeconds(delay);

        if (parentObj != null)
        {
            Vector3 projPos = parentObj.transform.position;

            if (isGround)
            {
                projPos += new Vector3(0, 2f, 0);
            }
            else {
                projPos += new Vector3(-0.7f, -0.23f, 0);
            }

            Instantiate(projectilePrefab, projPos, Quaternion.identity);

            if (projectileSpawnSound != null && projectileAudioSource != null)
            {
                projectileAudioSource.PlayOneShot(projectileSpawnSound);
            }
        }
    }
}