using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    
    public bool isPossessed = false;

    private AnimalManager manager;

    [SerializeField] private bool isActive = true;
    [SerializeField] private SOAnimal animalType;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float health;
    private float hunger;
    private Animal target;

    public void Initialize(AnimalManager manager, SOAnimal type) {

        this.manager = manager;

        animalType = type;
        health = type.MaxHealth;
        hunger = type.MaxHunger;
        if(type.Sprite != null)
            spriteRenderer.sprite = type.Sprite;

        StartCoroutine(DetectNearbyTargets());
    }

    private void Start() {
        if(animalType != null) {
            AnimalManager manager = FindObjectOfType<AnimalManager>();
            Initialize(manager, animalType);
        }
    }

    private void FixedUpdate() {
        if(isPossessed)
            return;
        if(target == null) {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, animalType.MovementSpeed * Time.fixedDeltaTime));
    }

    private void OnDestroy() {
        manager.RemoveAnimal(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.TryGetComponent<Animal>(out Animal animal)) {
            if(animalType.Prey.Contains(animal.GetAnimalType()))
                Destroy(other.gameObject);
        }
    }

    private IEnumerator DetectNearbyTargets() {
        float rSq = animalType.DetectionRadius * animalType.DetectionRadius;
        while(isActive) {
            target = null;
            float minDisSqr = rSq;
            foreach(var targetType in animalType.Prey) {
                var targetSet = manager.GetAnimals(targetType);
                if(targetSet == null)
                    continue;
                foreach(Animal potentialTarget in targetSet) {
                    float d = Vector3.SqrMagnitude(potentialTarget.transform.position - transform.position);
                    if(d < minDisSqr) {
                        d = minDisSqr;
                        target = potentialTarget;
                    }
                }
            }

            yield return new WaitForSeconds(animalType.DetectionInterval);
        }
    }

    public void HandleControl(Vector2 control) {
        Vector3 pos = transform.position;
        pos += Time.fixedDeltaTime * animalType.MovementSpeed * (Vector3) control;
        _rigidbody.MovePosition(pos);
    }

    public SOAnimal GetAnimalType() {
        return animalType;
    }

}
