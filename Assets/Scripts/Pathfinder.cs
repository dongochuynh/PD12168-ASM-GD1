using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = Object.FindFirstObjectByType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not found in the scene.");
        }
    }

    void Start()
    {
        waveConfig = enemySpawner != null ? enemySpawner.GetCurrentWave() : null;
        if (waveConfig == null)
        {
            Debug.LogError("WaveConfigSO not found or not set in EnemySpawner.");
            return;
        }

        waypoints = waveConfig.GetWaypoints();
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("Waypoints not set in WaveConfigSO.");
            return;
        }

        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
