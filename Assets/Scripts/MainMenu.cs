using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InitialSelectedBtn;

    // Start is called before the first frame update
    void Start()
    {
        // Clear Selected Button and set to initial.
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(InitialSelectedBtn);
    }

    // LoadDemo()
    // --Switches to Demo Level
    public void LoadDemo()
    {
        Debug.Log("Loading Demo...");
        SceneManager.LoadScene("GameScene");
    }

    // LoadTutorial()
    // --Switches to Tutorial Level
    public void LoadTutorial()
    {
        Debug.Log("Loading Tutorial...");
        SceneManager.LoadScene("TutorialScene");
    }

    // QuitGame()
    // --Quits Game Application
    public void QuitGame()
    {
        Application.Quit();
    }
}
