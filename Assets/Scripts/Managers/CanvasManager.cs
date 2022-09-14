using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;
    public Button backButton;
    public Button resumeGame;
    public Button returnToMenu;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Text")]
    public Text volSliderText;


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
            volSlider.onValueChanged.AddListener((value) => SliderValueChange(value));
            volSliderText.text = volSlider.value.ToString();
        }

        if (resumeGame)
            resumeGame.onClick.AddListener(() => ResumeGame());

        if (returnToMenu)
            returnToMenu.onClick.AddListener(() => LoadMenu());
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void SliderValueChange(float value)
    {
        if (volSliderText)
            volSliderText.text = value.ToString();
    }

    void StartGame()
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
                    //do something to pause the game
                }
                else
                {
                    //do something to unpause the game
                }
            }
        }
    }
}
