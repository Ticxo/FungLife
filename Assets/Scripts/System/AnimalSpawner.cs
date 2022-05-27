using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {
    
    [SerializeField] private bool isActive = true;
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private List<SOAnimal> spawnTypes;
    [SerializeField] private List<int> spawnWeights;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnRange;
    [SerializeField] private float noSpawnRange;
    [SerializeField] private int maxSpawnCount;

    private List<int> presumSpawnWeights = new List<int>();

    private void Start() {
        if(spawnTypes.Count != spawnWeights.Count) {
            Debug.LogError("Error: Spawn types do not have the same amount of spawn weights. Disabling game object.");
            gameObject.SetActive(false);
            return;
        }
        PopulatePresumSpawnWeights();
        StartCoroutine(SpawnAnimals());
    }

    private IEnumerator SpawnAnimals() {
        while(isActive && !GameManager.instance.GameEnded) {
            
            yield return new WaitForSeconds(spawnInterval);

            int chance = Random.Range(0, GetTotalWeight()) + 1;
            SOAnimal type = GetAnimalType(chance);
            
            if(AnimalManager.instance.SpawnCount + type.SpawnCost > maxSpawnCount)
                continue;

            Vector3 pos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            if(Vector3.SqrMagnitude(pos - AnimalManager.instance.Controller.transform.position) < noSpawnRange * noSpawnRange)
                continue;

            Animal animalObject = Instantiate<Animal>(animalPrefab, pos, Quaternion.identity);
            animalObject.gameObject.name = type.Name;
            animalObject.Initialize(type);
        }
    }

    // Helpers
    private void PopulatePresumSpawnWeights() {
        int total = 0;
        presumSpawnWeights.Add(total);
        foreach(int w in spawnWeights) {
            total += w;
            presumSpawnWeights.Add(total);
        }
    }

    private int GetTotalWeight() {
        return presumSpawnWeights[presumSpawnWeights.Count - 1];
    }

    private SOAnimal GetAnimalType(int chance) {
        for(int i = 1; i < presumSpawnWeights.Count; ++i) {
            if(presumSpawnWeights[i] >= chance) {
                return spawnTypes[i - 1];
            }
        }
        return null;
    }

}
