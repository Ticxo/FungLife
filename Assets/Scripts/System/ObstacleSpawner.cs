using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private int obstacleCount;
    [SerializeField] private float spawnRange;
    [SerializeField] private float noSpawnRange;

    private void Start() {
        for(int i = 0; i < obstacleCount; ++i) {
            Vector3 pos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            if(Vector3.SqrMagnitude(pos - AnimalManager.instance.Controller.transform.position) < noSpawnRange * noSpawnRange)
                continue;
            Instantiate(obstacles[Random.Range(0, obstacles.Count)], pos, Quaternion.identity, transform);
        }
    }

}
