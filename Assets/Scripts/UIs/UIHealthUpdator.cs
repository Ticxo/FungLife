using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthUpdator : MonoBehaviour {
    
    [SerializeField] private Slider healthSlider;

    private void Update() {
        var possessed = AnimalManager.instance.Controller.Possessed;
        healthSlider.value = possessed.Health / possessed.GetAnimalType().MaxHealth;
    }

}
