using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidQuest : AbstractQuest {

    private SOAnimal avoid;

    public SOAnimal Avoid { get => avoid; }

    public AvoidQuest(SOAnimal avoid) {
        this.avoid = avoid;
        Animal.animalPossessEvent += OnAnimalPossess;
        IsComplete = true;
    }

    ~AvoidQuest() {
        Animal.animalPossessEvent -= OnAnimalPossess;
    }

    protected override void OnAnimalPossess(Animal possessed) {
        if(IsComplete && possessed.GetAnimalType() == avoid) {
            IsComplete = false;
        }
    }

    public override string ToString() {
        return $"Don't become a {avoid.Name}!";
    }

}
