using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFungalPowerUpdator : MonoBehaviour {
    
    [SerializeField] private Image image;

    private void Update() {
        image.fillAmount = AnimalManager.instance.Controller.GetSuicideCooldownProgress();
    }

}
