using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMasterManager : MonoBehaviour
{
    public float timer = 0;
    float hintInterval = 30f;

    public bool gameFinished = false;

    public TMP_Text startButtonText;

    public GameObject menuPanel;
    public GameObject gameWonPanel;

    public static GameMasterManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startButtonText.text = "START";

        Time.timeScale = 0;
        if (!menuPanel.activeSelf)
            Time.timeScale = 1;
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        if(!PlayerPrefs.HasKey("TutorialSeen"))
        {
            PlayerPrefs.SetInt("TutorialSeen", 1);
            TutorialManager.instance.StartTutorial();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OpenMenu()
    {
        startButtonText.text = "CONTINUE";

        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= hintInterval)
        {
            timer = 0;
            //TriggerRandomHint();
        }
    }

    void TriggerRandomHint()
    {
        int randomVal = UnityEngine.Random.Range(0, 100);

        if (randomVal < 5)
            LogTextManager.instance.TriggerKnowledgeHint(3);
        else
            LogTextManager.instance.TriggerKnowledgeHint(2);
    }


    public void GameFinished()
    {
        MusicManager.instance.PlayTrack(0);
        AudioManager.instance.PlayGameWonSound();
        Time.timeScale = 0;
        gameFinished = true;
        gameWonPanel.SetActive(true);
    }

    public void CloseGameWonPanel()
    {
        gameWonPanel.SetActive(false);
        OpenMenu();
    }
}
