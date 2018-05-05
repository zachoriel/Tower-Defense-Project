using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;

    public string mainMenuScene = "MainMenu";

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = 0;
    }

    public void Menu()
    {
        Debug.Log("Going to menu");
        sceneFader.FadeTo(mainMenuScene);
        Time.timeScale = 1f;
    }
}
