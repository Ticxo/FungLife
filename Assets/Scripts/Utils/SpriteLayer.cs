using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update() {
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 10);
    }

}
