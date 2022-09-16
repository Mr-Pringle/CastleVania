using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    public AudioMixer auioMixer;

    [Header("Buttons")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;
    public Button backButton;
    public Button resumeGame;
    public Button returnToMenu;
    public Button goToCredits;
    public Button backFromCreds;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject creditsMenu;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Text")]
    public Text volSliderText;
    public Text lifeText;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => StartGame());

        if (settingButton)
            settingButton.onClick.AddListener(() => ShowSettingsMenu());

        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (volSlider)
        {
            float mixerValue;
            auioMixer.GetFloat("MusicVol", out mixerValue);
            volSlider.onValueChanged.AddListener((value) => MusicValueChange(value));
            volSlider.value = mixerValue + 80;
            volSliderText.text = volSlider.value.ToString();
        }

        if (resumeGame)
            resumeGame.onClick.AddListener(() => ResumeGame());

        if (returnToMenu)
            returnToMenu.onClick.AddListener(() => LoadMenu());

        if (backFromCreds)
            backFromCreds.onClick.AddListener(() => LoadMenu());

        if (goToCredits)
            goToCredits.onClick.AddListener(() => ShowCredits());

        if (lifeText)
            GameManager.instance.OnLifeValueChange.AddListener((value) => UpdateLifeText(value));

        if (scoreText)
            GameManager.instance.OnScoreValueChange.AddListener((value) => UpdateScoreText(value));
    }

    void UpdateLifeText(int value)
    {
        if (lifeText)
            lifeText.text = "Lives: " + value.ToString();
    }

    void UpdateScoreText(int value)
    {
        if (scoreText)
            scoreText.text = "Score: " + value.ToString();
            
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            SceneManager.LoadScene("Title");
            return;
        }

        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void MusicValueChange(float value)
    {
        if (volSliderText)
        {
            volSliderText.text = value.ToString();
            auioMixer.SetFloat("MusicVol", value - 80);
        }  
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Title");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                //HINT FOR LAB 8
                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0.0f;
                    //SceneManager.LoadScene("PauseMenu");
                }
                else
                {
                    Time.timeScale = 1.0f;
                    //SceneManager.LoadScene("Level");
                }
            }
        }
    }
}
