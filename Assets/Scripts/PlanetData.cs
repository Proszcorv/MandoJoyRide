using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanet", menuName = "Game/Planet")]
public class PlanetData : ScriptableObject
{
    public string planetName;
    public Sprite skySprite;
    public Sprite mountainsSprite;
    public Sprite groundSprite;
    public Color floorColor = Color.white;
    public int scoreThreshold;
}
