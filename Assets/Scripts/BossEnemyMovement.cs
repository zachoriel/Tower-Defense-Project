using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class BossEnemyMovement : MonoBehaviour
{
    public GameManager manager;

    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.waypoints[0];                                                                       // Sets the enemy's target to the first waypoint

        enemy.speed = enemy.startSpeed;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;                                                    // Direction of the spawned enemy
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);                             // Enemy movement

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)                                     // If enemy has reached waypoint
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)                                                  // If current waypoint is >= total waypoints
        {
            BossEndPath();
            return;                                                                                            // Ensures that the code will not move on from here without the object being destroyed
        }

        wavepointIndex++;                                                                                      // Increase current waypoint by 1
        target = Waypoints.waypoints[wavepointIndex];                                                          // Sets enemy target to current waypoint in the waypoint index
    }

    void BossEndPath()
    {
        PlayerStats.Lives -= PlayerStats.Lives;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);

        if (PlayerStats.Lives <= 0)
        {
            PlayerStats.Lives = 0;
            manager.GetComponent<GameManager>().EndGame();
        }
    }
}
