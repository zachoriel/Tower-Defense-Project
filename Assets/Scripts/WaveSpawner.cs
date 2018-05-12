using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public AudioSource voicedCountdown;

    public static int EnemiesAlive = 0;

    [HideInInspector]
    public bool noEnemiesAlive;

    [HideInInspector]
    public int ea;             // IGNORE THIS, USED FOR BUG TESTING

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 20f;                                                     // Time between waves of enemies. 5.5 is used to avoid jumpy countdown in the UI
    private float countdown = 10f;                                                           // Time until 1st wave spawn

    public Text waveCountdownText;                                                             // Referencing our text object
    public Text enemiesAliveText;

    public GameManager gameManager;

    [HideInInspector]
    public int waveIndex = 0;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        ea = EnemiesAlive;       // SEE ABOVE

        if (EnemiesAlive > 0)
        {
            noEnemiesAlive = false;
            enemiesAliveText.text = "Enemies alive: " + EnemiesAlive.ToString();
            return;
        }
        else
        {
            noEnemiesAlive = true;
            enemiesAliveText.text = "Enemies alive: " + EnemiesAlive.ToString();
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;                                                      // Reset countdown timer to = time to next wave
            return;
        }

        countdown -= Time.deltaTime;                                                           // Decreases countdown by 1 every second

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);                             // Mathf.Round eliminates decimals by rounding down to an integer, and .ToString() converts our numeric values to text

        if (waveIndex == waves.Length)
        {
            if (PlayerStats.Lives <= 0)
            {
                return;
            }

            if (noEnemiesAlive == true && PlayerStats.Lives > 0)
            {
                gameManager.WinLevel();
                this.enabled = false;        // Disables the entire script
            }
        }
    }

    IEnumerator SpawnWave()                                                                    // Ienumerator allows us to wait x amount of seconds before the next iteration of the for loop
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count + wave.count2 + wave.count3 + wave.count4 + wave.count5 + wave.count6;
        enemiesAliveText.text = "Enemies alive: " + EnemiesAlive.ToString();


        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);                                             // Waits x amount of time before next iteration
        }
        for (int i = 0; i < wave.count2; i++)
        {
            SpawnEnemy(wave.enemy2);
            yield return new WaitForSeconds(1f / wave.rate2);                                             // Waits x amount of time before next iteration
        }
        for (int i = 0; i < wave.count3; i++)
        {
            SpawnEnemy(wave.enemy3);
            yield return new WaitForSeconds(1f / wave.rate3);                                             // Waits x amount of time before next iteration
        }
        for (int i = 0; i < wave.count4; i++)
        {
            SpawnEnemy(wave.enemy4);
            yield return new WaitForSeconds(1f / wave.rate4);                                             // Waits x amount of time before next iteration
        }
        for (int i = 0; i < wave.count5; i++)
        {
            SpawnEnemy(wave.enemy5);
            yield return new WaitForSeconds(1f / wave.rate5);                                             // Waits x amount of time before next iteration
        }
        for (int i = 0; i < wave.count6; i++)
        {
            SpawnEnemy(wave.enemy6);
            yield return new WaitForSeconds(1f / wave.rate6);                                             // Waits x amount of time before next iteration
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
       GameObject spawnedEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);                    // Spawns our enemy prefab at the given spawn point
        spawnedEnemy.GetComponent<Enemy>().manager = gameManager;                                                 // Creates a reference to the game manager in the Enemy scrip at runtime since I can't put it in the prefab
    }

}
