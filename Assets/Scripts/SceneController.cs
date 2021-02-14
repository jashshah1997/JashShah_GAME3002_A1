using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("PenaltyScene");
    }

    public void OnButtonBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonResume()
    {
        GameObject obj = GameObject.Find("PauseMenu");
        PauseMenuScript pause = obj.GetComponent<PauseMenuScript>();
        pause.TogglePauseMenu();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
