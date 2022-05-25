using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    private Vector2 moveInput;
    private bool awaitSuicide;
    [SerializeField] private Animal respawnPrefab;
    [SerializeField] private SOAnimal respawnType;
    [SerializeField] private Animal possessed;

    public Animal Possessed { get => possessed; }

    private void Start() {
        Possess(possessed);
    }

    private void FixedUpdate() {

        if(awaitSuicide) {
            var pos = possessed.transform.position;
            possessed.Damage(possessed.GetAnimalType().MaxHealth);

            var fungus = Instantiate(respawnPrefab, pos, Quaternion.identity);
            fungus.Initialize(respawnType);
            Possess(fungus);
            awaitSuicide = false;
        }

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

    public void OnSuicide(InputAction.CallbackContext ctx) {
        if(!ctx.performed) 
            return;
        awaitSuicide = true;
    }

    public void Possess(Animal target) {
        if(possessed != null)
            possessed.IsPossessed = false;
        target.IsPossessed = true;
        possessed = target;
    }

}
