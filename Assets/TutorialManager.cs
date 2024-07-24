using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    private int currentPanelIndex = -1;

    public static TutorialManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Optionally, you can call StartTutorial() automatically when the game starts
        // StartTutorial();
    }

    public void StartTutorial()
    {
        if (tutorialPanels.Length == 0 || currentPanelIndex != -1)
            return;

        currentPanelIndex = 0;
        Time.timeScale = 0f; // Pause the game
        tutorialPanels[currentPanelIndex].SetActive(true);
    }

    public void NextTutorial()
    {
        if (currentPanelIndex == -1)
            return;

        tutorialPanels[currentPanelIndex].SetActive(false);

        currentPanelIndex++;

        if (currentPanelIndex < tutorialPanels.Length)
        {
            tutorialPanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
            currentPanelIndex = -1;
        }
    }
}