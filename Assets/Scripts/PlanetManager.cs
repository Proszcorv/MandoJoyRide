using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skyRenderer;
    [SerializeField] private SpriteRenderer mountainsRenderer;
    [SerializeField] private SpriteRenderer groundRenderer;
    [SerializeField] private SpriteRenderer floorRenderer;
    [SerializeField] private PlanetData[] planets;
    [SerializeField] private ObstacleSpawner groundSpawner;

    void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("CurrentPlanet", 0);
        if (savedIndex < planets.Length)
        {
            ApplyPlanet(planets[savedIndex]);
        }
    }

    public void ApplyPlanet(PlanetData planet)
    {
        skyRenderer.sprite = planet.skySprite;
        mountainsRenderer.sprite = planet.mountainsSprite;
        groundRenderer.sprite = planet.groundSprite;
        floorRenderer.color = planet.floorColor;
        skyRenderer.sprite = planet.skySprite;
        skyRenderer.transform.localScale = planet.skyScale;
        if (groundSpawner != null && planet.groundObstaclePrefab != null)
        {
            groundSpawner.SetObstaclePrefab(planet.groundObstaclePrefab);
        }
    }

    public void SelectPlanet(int planetIndex)
    {
        if (planetIndex >= 0 && planetIndex < planets.Length)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);

            if (highScore >= planets[planetIndex].scoreThreshold)
            {
                PlayerPrefs.SetInt("CurrentPlanet", planetIndex);
                PlayerPrefs.Save();
                ApplyPlanet(planets[planetIndex]);
            }
            else
            {
                Debug.Log("Ez a bolygó még zárolva van! Szükséges pont: " + planets[planetIndex].scoreThreshold);
            }
        }
    }
}