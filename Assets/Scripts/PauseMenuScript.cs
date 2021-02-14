using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private GameObject pauseMenu;
    private bool m_is_game_over = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("MenuCanvas");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_is_game_over && Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }

        if (!m_is_game_over && !SoccerBallManager.Instance().HasRemaningSoccerBalls() && !is_any_active_ball())
        {
            m_is_game_over = true;
            TogglePauseMenu();
            GameObject.Find("ResumeButton").SetActive(false);
            GameObject.Find("MenuTitle").GetComponent<Text>().text = "SCORE: " + SoccerBallManager.Instance().goal_count + " / " + SoccerBallManager.Instance().GetMaximumGoals();
        }

    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        } else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }            
    }

    private bool is_any_active_ball()
    {
        var balls = GameObject.FindGameObjectsWithTag("ball");
        foreach(var ball in balls)
        {
            if (ball.GetComponent<SoccerBallController>().m_is_active)
            {
                return true;
            }
        }
        return false;
    }
}
