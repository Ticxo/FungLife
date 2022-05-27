using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    
    public static MusicPlayer instance;

    [SerializeField] private AudioSource source;
    public float MusicVolume { get => source.volume; set => source.volume = value; }

    private void Awake() {
        if(instance) {
            Destroy(gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
