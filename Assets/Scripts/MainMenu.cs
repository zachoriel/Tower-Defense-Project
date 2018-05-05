using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "LevelSelector";

    public SceneFader sceneFader;

    public void Play ()
    {
        sceneFader.FadeTo(levelToLoad);
        WaveSpawner.EnemiesAlive = 0;
    }

    public void Quit ()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Time.timeScale -= 1f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Time.timeScale += 1f;
        }
    }
}
