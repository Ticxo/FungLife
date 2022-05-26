using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessQuest : AbstractQuest {

    private SOAnimal possessType;
    private int requirement;
    private int count;

    public SOAnimal PossessType { get => possessType; }
    public int Requirement { get => requirement; }
    public int Count { get => count; }

    public PossessQuest(SOAnimal possessType, int requirement) {
        this.possessType = possessType;
        this.requirement = requirement;
        Animal.animalPossessEvent += OnAnimalPossess;
        IsComplete = false;
    }

    ~PossessQuest() {
        Animal.animalPossessEvent -= OnAnimalPossess;
    }

    protected override void OnAnimalPossess(Animal possessed) {
        if(!IsComplete && possessed.GetAnimalType() == possessType) {
            IsComplete = ++count >= requirement;
            CallStatusChangeEvent();
        }
    }

    public override string ToString() {
        return $"Get eaten by a {possessType.Name} {count}/{requirement} times.";
    }

}
