using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour {

    public static AnimalManager instance;

    [SerializeField] private PlayerController controller;
    public PlayerController Controller { get => controller; set => controller = value; }

    private Dictionary<SOAnimal, HashSet<Animal>> animals = new Dictionary<SOAnimal, HashSet<Animal>>();

    private int spawnCount;
    public int SpawnCount { get => spawnCount; }

    private void Awake() {
        if(instance) {
            Destroy(gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RegisterAnimal(Animal animal) {
        var type = animal.GetAnimalType();
        if(!animals.ContainsKey(type))
            animals[type] = new HashSet<Animal>();
        animals[type].Add(animal);
        spawnCount += type.SpawnCost;
    }

    public void RemoveAnimal(Animal animal) {
        var type = animal.GetAnimalType();
        if(animals.ContainsKey(type)) {
            animals[type].Remove(animal);
            spawnCount -= type.SpawnCost;
        }
    }

    public HashSet<Animal> GetAnimals(SOAnimal animalType) {
        if(!animals.ContainsKey(animalType))
            return null;
        return animals[animalType];
    }

    public Animal GetClosestTarget(Animal hunter, float radius) {
        Animal target = null;
        SOAnimal type = hunter.GetAnimalType();

        float minDisSqr = radius * radius;
        foreach(var targetType in type.Prey) {
            var targetSet = GetAnimals(targetType);
            if(targetSet == null)
                continue;
            foreach(Animal potentialTarget in targetSet) {
                if(potentialTarget == hunter)
                    continue;
                float d = Vector3.SqrMagnitude(potentialTarget.transform.position - hunter.transform.position);
                if(d < minDisSqr) {
                    minDisSqr = d;
                    target = potentialTarget;
                }
            }
        }
        return target;
    }

}
