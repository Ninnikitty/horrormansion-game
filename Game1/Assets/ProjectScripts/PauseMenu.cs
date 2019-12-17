using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    GameObject getCamera;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0;i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    } 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (GameIsPaused) {Resume();}
            else {Pause();}
        }
    }
    //Resumes Game 
   public void Resume()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        allowCamera();
        GameIsPaused = false;
    }
    //Pauses the game
    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        disableCamera();
        GameIsPaused = true;
    }
    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

    void allowCamera()
    {
        getCamera = GameObject.Find("1stPCamera");
        getCamera.GetComponent<cameraController>().enabled = true;
    }

    void disableCamera()
    {
        getCamera = GameObject.Find("1stPCamera");
        getCamera.GetComponent<cameraController>().enabled = false;
    }
}
