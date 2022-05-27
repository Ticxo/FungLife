using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreUpdator : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update() {
        scoreText.text = "Score : " + GameManager.instance.Score.ToString();
    }

}
