using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    private Vector2 moveInput;
    private Animal possessed;

    private void FixedUpdate() {
        if(possessed == null) {
            transform.position = Vector3.zero;
            return;
        }
        possessed.HandleControl(moveInput);
        transform.position = possessed.transform.position;
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        if(ctx.started) 
            return;
        moveInput = ctx.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnSelect(InputAction.CallbackContext ctx) {
        if(!ctx.performed) 
            return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit && hit.transform.gameObject.TryGetComponent<Animal>(out Animal animal)) {
            Possess(animal);
        }
    }

    public void Possess(Animal target) {
        if(possessed != null)
            possessed.isPossessed = false;
        target.isPossessed = true;
        possessed = target;
    }

}
