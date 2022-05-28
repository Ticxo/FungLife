using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    private Vector2 moveInput;
    [SerializeField] private Animal respawnPrefab;
    [SerializeField] private SOAnimal respawnType;
    [SerializeField] private Animal possessed;
    [Header("Suicide")]
    [SerializeField] private float suicideCooldown;
    [SerializeField] private float teleportBorder;

    private float suicideCooldownTimer;
    private bool awaitSuicide;

    [SerializeField] private Animator cameraAnimator;
    private bool gameStarted;

    public Animal Possessed { get => possessed; }

    private void Start() {
        AnimalManager.instance.Controller = this;
        PossessNoTrigger(possessed);
    }

    private void Update() {
        if(suicideCooldownTimer > 0)
            suicideCooldownTimer = Mathf.Max(suicideCooldownTimer - Time.deltaTime, 0);
        
        if(possessed == null) {
            transform.position = Vector3.zero;
            return;
        }
        transform.position = possessed.transform.position;
    }

    private void FixedUpdate() {

        if(awaitSuicide) {
            if(possessed.GetAnimalType() != respawnType) {
                Vector3 pos = possessed.transform.position;
                possessed.Damage(possessed.GetAnimalType().MaxHealth);
                var fungus = Instantiate(respawnPrefab, pos, Quaternion.identity);
                fungus.Initialize(respawnType);
                Possess(fungus);
            }else {
                var pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                pos.x = Mathf.Clamp(pos.x, -teleportBorder, teleportBorder);
                pos.y = Mathf.Clamp(pos.y, -teleportBorder, teleportBorder);
                pos.z = 0;
                possessed.transform.position = pos;
                possessed.PossessParticle.Play();
            }
            awaitSuicide = false;
        }
        possessed?.HandleControl(moveInput);
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
        if(!ctx.performed || suicideCooldownTimer > 0 || !gameStarted)
            return;
        awaitSuicide = true;
        suicideCooldownTimer = suicideCooldown;
    }

    public void OnReload(InputAction.CallbackContext ctx) {
        if(!ctx.performed) 
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Possess(Animal target) {
        if(!gameStarted) {
            cameraAnimator.SetBool("isStarted", true);
            gameStarted = true;
        }
        PossessNoTrigger(target);
    }

    public void PossessNoTrigger(Animal target) {
        if(possessed != null)
            possessed.IsPossessed = false;
        possessed = target;
        target.IsPossessed = true;
    }

    public float GetSuicideCooldownProgress() {
        return 1 - suicideCooldownTimer / suicideCooldown;
    }

}
