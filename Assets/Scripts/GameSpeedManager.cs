using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance { get; private set; }

    [Header("Alapsebesség (ezt követi a háttér és minden akadály)")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float accelerationRate = 0.1f;
    [SerializeField] private float maxBaseSpeed = 15f;

    public float CurrentBaseSpeed { get; private set; }

    void Awake()
    {
        Instance = this;
        CurrentBaseSpeed = baseSpeed;
    }

    void Update()
    {
        if (CurrentBaseSpeed < maxBaseSpeed)
        {
            CurrentBaseSpeed += accelerationRate * Time.deltaTime;
        }
    }
}
