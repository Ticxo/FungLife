using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    [Header("Timer")]
    [SerializeField] private float timeLimit;
    private float startTime;
    private float timeLeft;
    public float TimeLeft { get => timeLeft; }

    [Header("Score")]
    [SerializeField] private QuestManager questManager;
    private int score;
    public int Score { get => score; }
    private float questBonus;
    public float QuestBonus { get => questBonus; }

    [Header("End Screen")]
    [SerializeField] private float endScreenLoadTime;

    private bool gameEnded;
    public bool GameEnded { get => gameEnded; }
    public delegate void GameEnd();
    public event GameEnd gameEndEvent; 

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Animal.animalDeathEvent += OnAnimalDeath;
        Animal.animalPossessEvent += OnAnimalPossess;

        startTime = Time.time;
    }

    private void OnDestroy() {
        Animal.animalDeathEvent -= OnAnimalDeath;
        Animal.animalPossessEvent -= OnAnimalPossess;
    }

    private void Update() {
        if(gameEnded) return;

        timeLeft = timeLimit - Time.time + startTime;
        if(timeLeft <= 0) {
            gameEnded = true;
            if(gameEndEvent != null)
                gameEndEvent();

            questBonus = questManager.GetQuestBonus();

            StartCoroutine(LoadEndScreen());
            return;
        }
    }

    private void OnAnimalDeath(Animal target, Animal killer) {
        if(!gameEnded && killer == AnimalManager.instance.Controller.Possessed)
            score += 20;
    }

    private void OnAnimalPossess(Animal possessed) {
        if(!gameEnded)
            score += 100;
    }

    private IEnumerator LoadEndScreen() {
        yield return new WaitForSeconds(endScreenLoadTime);
        SceneManager.LoadScene("End Screen");
    }

}
