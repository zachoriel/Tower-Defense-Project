using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuEnemy))]
public class MenuEnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private MenuEnemy menuEnemy;

    void Start()
    {
        menuEnemy = GetComponent<MenuEnemy>();

        target = Waypoints.waypoints[0];                                                                       // Sets the enemy's target to the first waypoint
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;                                                    // Direction of the spawned enemy
        transform.Translate(dir.normalized * menuEnemy.speed * Time.deltaTime, Space.World);                             // Enemy movement

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)                                     // If enemy has reached waypoint
        {
            GetNextWaypoint();
        }

        menuEnemy.speed = menuEnemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)                                                  // If current waypoint is >= total waypoints
        {
            EndPath();
            return;                                                                                            // Ensures that the code will not move on from here without the object being destroyed
        }

        wavepointIndex++;                                                                                      // Increase current waypoint by 1
        target = Waypoints.waypoints[wavepointIndex];                                                          // Sets enemy target to current waypoint in the waypoint index
    }

    void EndPath()
    {
        wavepointIndex = 0;
        target = Waypoints.waypoints[0];
        //Destroy(gameObject);
    }
}
