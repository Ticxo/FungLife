using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    
    private int score;
    public int Score { get => score; }

    private void Start() {
        Animal.animalDeathEvent += OnAnimalDeath;
        Animal.animalPossessEvent += OnAnimalPossess;
    }

    private void OnDestroy() {
        Animal.animalDeathEvent -= OnAnimalDeath;
        Animal.animalPossessEvent -= OnAnimalPossess;
    }

    private void OnAnimalDeath(Animal target, Animal killer) {
        if(killer == AnimalManager.instance.Controller.Possessed)
            score += 20;
    }

    private void OnAnimalPossess(Animal possessed) {
        score += 100;
    }

}
