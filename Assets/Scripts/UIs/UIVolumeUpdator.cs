using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeUpdator : MonoBehaviour {
    
    [SerializeField] private Slider slider;

    private void Start() {
        slider.value = MusicPlayer.instance.MusicVolume;
    }

    public void OnSliderChange(float val) {
        MusicPlayer.instance.MusicVolume = val;
    }

}
