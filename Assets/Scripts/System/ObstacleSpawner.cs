using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private int obstacleCount;
    [SerializeField] private float spawnRadius;

    private void Start() {
        for(int i = 0; i < obstacleCount; ++i) {
            Vector3 pos = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0);
            Instantiate(obstacles[Random.Range(0, obstacles.Count)], pos, Quaternion.identity, transform);
        }
    }

}
