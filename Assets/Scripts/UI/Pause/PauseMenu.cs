using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool allowPause = true;

    public GameObject pauseMenuUI;

    public GameObject InitialSelectedBtn;

    // Update is called once per frame
    void Update()
    {
        // Pause Game / Unpause Game when the Escape key is pressed and pausing is allowed
        if (Input.GetKeyDown(KeyCode.Escape) && allowPause)
        {
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // ((DEBUG)) - Left in for Capstone Demo
        // Allows player to enable Invinsibility for the rest of the level (D + Left Shift)
        if (GamePaused)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift)){
                    Debug.Log("PauseMenu (Debug): Player Invulnerability Activated");
                    GameObject.Find("Player").GetComponent<Player>().SetInvulnerability(true);
                }
            }
        }
    }

    // ResumeGame()
    // --Resumes time and closes pause menu panel
    public void ResumeGame()
    {

        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        
        GamePaused = false;
    }

    // PauseGame()
    // --Freezes time and opens up pause menu panel.
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;

        // Clear Selected Button and set to initial.
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(InitialSelectedBtn);
    }

    // RestartLevel()
    // --Changes necessary variables to default and Reloads current scene
    public void RestartLevel()
    {
        // Set default settings for level & load
        Time.timeScale = 1f;
        GamePaused = false;
        allowPause = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // LoadMainMenu()
    // --Switches back to the main menu scene
    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        allowPause = true;

        SceneManager.LoadScene("MenuScene");
        ResumeGame();
    }

    // QuitGame()
    // --Quits Game Application
    public void QuitGame()
    {
        Application.Quit();
    }
}
