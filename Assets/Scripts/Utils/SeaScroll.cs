using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaScroll : MonoBehaviour {

    [SerializeField] private float scrollSpeed;

    private void Update() {
        float x = transform.position.x;
        x = (x + scrollSpeed * Time.deltaTime) % 1;
        transform.position = new Vector3(x, x, 0);
    }

}
