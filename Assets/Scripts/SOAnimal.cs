using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal", menuName = "Reapershroom/New Animal", order = 0)]
public class SOAnimal : ScriptableObject {
    
    [Header("General")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [Header("Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxHunger;
    [SerializeField] private float _hungerDecayRate;
    [SerializeField] private float _hungerFillRate;
    [Header("Behavior")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _detectionInterval;
    [SerializeField] private List<SOAnimal> _prey;

    public Sprite Sprite { get => _sprite; }
    public string Id { get => _id; }
    public string Name { get => _name; }
    public float MaxHealth { get => _maxHealth; }
    public float MaxHunger { get => _maxHunger; }
    public float HungerDecayRate { get => _hungerDecayRate; }
    public float HungerFillRate { get => _hungerFillRate; }
    public float MovementSpeed { get => _movementSpeed; }
    public float DetectionRadius { get => _detectionRadius; }
    public float DetectionInterval { get => _detectionInterval; }
    public List<SOAnimal> Prey { get => _prey; }
}