using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool GameIsPaused;

    public GameObject gameOverUI;

    public GameObject levelWonUI;

    public GameObject pauseUI;

    public AudioSource elevatorMusic, gameAmbiance;

    public GameObject startPos;

    void Start()
    {
        gameAmbiance.Play();
        GameIsOver = false;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameIsOver)
        {
            return;
        }

        if (PlayerStats.Lives <= 0)
        {
            PlayerStats.Lives = 0;
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseTimeCheat();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseTimeCheat();
        }
	}

    public void EndGame()
    {
        GameIsOver = true;
        //gameAmbiance.Stop();
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL COMPLETED!");
        GameIsOver = true;
        gameAmbiance.Stop();
        levelWonUI.SetActive(true);

    }

    public void PauseGame()
    {
        GameIsPaused = true;
        pauseUI.SetActive(true);
        gameAmbiance.Pause();
        elevatorMusic.Play();
        Time.timeScale = 0f;
    }

    void Resume()
    {
        GameIsPaused = false;
        pauseUI.SetActive(false);
        gameAmbiance.UnPause();
        Time.timeScale = 1f;
    }

    void IncreaseTimeCheat()
    {
        Time.timeScale += 1f;
    }

    void DecreaseTimeCheat()
    {
        Time.timeScale -= 1f;
    }
}
