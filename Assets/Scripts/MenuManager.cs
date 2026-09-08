using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private GameObject planetsPanel;

    [System.Serializable]
    public class PlanetSlot
    {
        public Button planetButton;
        public PlanetData planetData;
        public Outline outlineComponent;
        public GameObject lockObject;
        public TextMeshProUGUI lockTextComponent;
    }

    [Header("Bolygó Gombok Beállításai")]
    public PlanetSlot[] planets;

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenPlanetsPanel()
    {
        UpdatePlanetButtons();
        if (planetsPanel != null) planetsPanel.SetActive(true);
    }

    public void ClosePlanetsPanel()
    {
        if (planetsPanel != null) planetsPanel.SetActive(false);
    }

    public void SelectPlanetFromMenu(int index)
    {
        PlayerPrefs.SetInt("CurrentPlanet", index);
        PlayerPrefs.Save();

        PlanetManager pManager = FindAnyObjectByType<PlanetManager>();

        if (pManager != null)
        {
            pManager.SelectPlanet(index);
        }

        ClosePlanetsPanel();
    }

    private void UpdatePlanetButtons()
    {
        int currentPlanet = PlayerPrefs.GetInt("CurrentPlanet", 0);

        for (int i = 0; i < planets.Length; i++)
        {
            var slot = planets[i];
            bool isUnlocked = false;

            if (i == 0)
            {
                isUnlocked = true;
            }
            else
            {
                int prevPlanetHighScore = PlayerPrefs.GetInt("HighScore_" + (i - 1), 0);

                if (prevPlanetHighScore >= slot.planetData.scoreThreshold)
                {
                    isUnlocked = true;
                }
            }

            slot.planetButton.interactable = isUnlocked;

            if (slot.outlineComponent != null)
            {
                slot.outlineComponent.enabled = (i == currentPlanet);
            }

            if (slot.lockObject != null)
            {
                slot.lockObject.SetActive(!isUnlocked);
            }

            if (slot.lockTextComponent != null && !isUnlocked)
            {
                slot.lockTextComponent.text = "Reach " + slot.planetData.scoreThreshold;
            }
        }
    }
}