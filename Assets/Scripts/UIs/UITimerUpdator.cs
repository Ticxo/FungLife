using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimerUpdator : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Animator timerAnimator;

    private void Start() {
        GameManager.instance.gameEndEvent += OnGameEnd;
    }

    private void Update() {
        if(!GameManager.instance.GameEnded)
            timerText.text = GetTimeString(GameManager.instance.TimeLeft);
    }

    private void OnGameEnd() {
        timerText.text = GetTimeString(0);
        timerAnimator.SetTrigger("timerSpin");
    }

    private string GetTimeString(float time) {
        int minute = Mathf.FloorToInt(time / 60f);
        int second = Mathf.FloorToInt(time % 60f);
        return $"{minute}:{second.ToString("00")}";
    }

}
