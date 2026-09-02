using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skyRenderer;
    [SerializeField] private SpriteRenderer mountainsRenderer;
    [SerializeField] private SpriteRenderer groundRenderer;
    [SerializeField] private SpriteRenderer floorRenderer;
    [SerializeField] private PlanetData[] planets;

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
    }

    public void GoToNextPlanet()
    {
        int currentIndex = PlayerPrefs.GetInt("CurrentPlanet", 0);
        int nextIndex = currentIndex + 1;

        if (nextIndex < planets.Length)
        {
            PlayerPrefs.SetInt("CurrentPlanet", nextIndex);
            PlayerPrefs.Save();
        }
    }
}