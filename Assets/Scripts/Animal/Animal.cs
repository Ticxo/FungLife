using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    private bool isInitialized = false;
    [SerializeField] private SOAnimal animalType;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float waddleAngle;
    [SerializeField] private float waddleSpeed;
    
    private float health;
    public float Health { get => health; }

    private bool isPossessed = false;
    public bool IsPossessed { get => isPossessed; set => isPossessed = value; }

    private Animal target;
    public Animal Target { get => target; set => target = value; }

    private Animal hunter;
    public Animal Hunter { get => hunter; set => hunter = value; }

    private float spawnTime;
    private bool isWalking;

    public void Initialize(SOAnimal type) {
        animalType = type;
        health = type.MaxHealth;
        if(type.Sprite != null)
            spriteRenderer.sprite = type.Sprite;

        spawnTime = Time.time;
        gameObject.transform.localScale = type.Scale * Vector3.one;

        AnimalManager.instance.RegisterAnimal(this);
        isInitialized = true;
    }

    private void Start() {
        if(animalType != null && !isInitialized) {
            Initialize(animalType);
        }
    }

    private void FixedUpdate() {
        if(isWalking)
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, waddleAngle * Mathf.Sin((Time.time - spawnTime) * waddleSpeed));
        else
            spriteRenderer.transform.rotation = Quaternion.identity;
    }

    private void OnDestroy() {
        AnimalManager.instance.RemoveAnimal(this);
    }

    public void HandleControl(Vector2 control) {
        if(animalType.MovementSpeed > 0 && control != Vector2.zero) {
            Vector3 pos = transform.position;
            pos += Time.fixedDeltaTime * animalType.MovementSpeed * (Vector3) control;
            _rigidbody.MovePosition(pos);
            isWalking = true;
            spriteRenderer.flipX = control.x < 0;
        }else {
            isWalking = false;
        }
    }

    public void StopHandleControl() {
        isWalking = false;
    }

    public void Attack(Animal target) {
        if(health <= 0) return;
        if(target.Damage(animalType.Damage)) {
            if(target.IsPossessed)
                AnimalManager.instance.Controller.Possess(this);
            Heal(target.GetAnimalType().HealthFillRate);
            target = null;
        }
    }

    public void AttackNearby() {
        var closest = AnimalManager.instance.GetClosestTarget(this, animalType.AttackRange);
        if(closest) {
            Attack(closest);
        }
    }

    public bool Damage(float damage) {
        health -= damage;
        float f = health / animalType.MaxHealth;
        spriteRenderer.color = new Color(f, f, f, 1);
        if(health <= 0)
            Destroy(gameObject);
        return health <= 0;
    }

    public void Heal(float heal) {
        health = Mathf.Min(health + target.GetAnimalType().HealthFillRate, animalType.MaxHealth);
        float f = health / animalType.MaxHealth;
        spriteRenderer.color = new Color(f, f, f, 1);
    }

    public SOAnimal GetAnimalType() {
        return animalType;
    }

}
