using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Start() {
        var inst = GameManager.instance;
        scoreText.text = $"Score: {inst.Score}\nQuest Bonus: x{inst.QuestBonus}";
        finalScoreText.text = $"Total: {Mathf.RoundToInt(inst.Score * inst.QuestBonus)}";
    }

    public void OnRestart() {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenu() {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Main Menu");
    }

}
