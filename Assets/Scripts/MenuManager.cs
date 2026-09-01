using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private GameObject planetsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenPlanetsPanel()
    {
        if (planetsPanel != null) planetsPanel.SetActive(true);
    }

    public void ClosePlanetsPanel()
    {
        if (planetsPanel != null) planetsPanel.SetActive(false);
    }
}
