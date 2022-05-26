using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    
    [SerializeField] private UIQuestPanelUpdator questPanelUpdator;
    [SerializeField] private List<SOAnimal> questAnimals;

    private List<AbstractQuest> quests = new List<AbstractQuest>();
    public List<AbstractQuest> Quests { get => quests; }

    private void Start() {

        GenerateRandomAvoidQuest();
        GenerateRandomPossessQuest();
        GenerateRandomPossessQuest();
        GenerateRandomPossessQuest();
        GenerateRandomPossessQuest();

        questPanelUpdator.UpdatePanel();
    }

    private void GenerateRandomAvoidQuest() {
        Quests.Add(new AvoidQuest(RandomAnimal()));
    }

    private void GenerateRandomPossessQuest() {
        Quests.Add(new PossessQuest(RandomAnimal(), Random.Range(1, 5)));
    }

    private SOAnimal RandomAnimal() {
        int id = Random.Range(0, questAnimals.Count);
        var animal = questAnimals[id];
        questAnimals.RemoveAt(id);
        return animal;
    }

}
