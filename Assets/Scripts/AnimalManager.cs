using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour {
    
    [SerializeField] private bool isActive = true;
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private List<SOAnimal> spawnTypes;
    [SerializeField] private List<int> spawnWeights;
    [SerializeField] private float spawnInterval;

    private List<int> presumSpawnWeights = new List<int>();
    private Dictionary<SOAnimal, HashSet<Animal>> animals = new Dictionary<SOAnimal, HashSet<Animal>>();

    private void Start() {
        if(spawnTypes.Count != spawnWeights.Count) {
            Debug.LogError("Error: Spawn types do not have the same ammount of spawn weights. Disabling game object.");
            gameObject.SetActive(false);
            return;
        }

        PopulatePresumSpawnWeights();
        StartCoroutine(SpawnAnimals());
    }

    private IEnumerator SpawnAnimals() {
        while(isActive) {
            int chance = Random.Range(0, GetTotalWeight()) + 1;
            SOAnimal type = GetAnimalType(chance);

            Vector3 pos = 5 * (Vector3) Random.insideUnitCircle + transform.position;
            Animal animalObject = Instantiate<Animal>(animalPrefab, pos, Quaternion.identity);
            animalObject.gameObject.name = type.Name;
            animalObject.Initialize(this, type);

            if(!animals.ContainsKey(type))
                animals[type] = new HashSet<Animal>();
            animals[type].Add(animalObject);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Getter Setters
    public void RemoveAnimal(Animal animal) {
        if(animals.ContainsKey(animal.GetAnimalType()))
            animals[animal.GetAnimalType()].Remove(animal);
    }

    public HashSet<Animal> GetAnimals(SOAnimal animalType) {
        if(!animals.ContainsKey(animalType))
            return null;
        return animals[animalType];
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
