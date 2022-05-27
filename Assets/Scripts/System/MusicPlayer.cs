using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    
    public static MusicPlayer instance;

    private void Awake() {
        if(instance) {
            Destroy(gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
