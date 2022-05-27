using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal", menuName = "FungLife/New Animal", order = 0)]
public class SOAnimal : ScriptableObject {
    
    [Header("General")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _possessedSprite;
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private int _spawnCost;
    [SerializeField] private bool _playEatenEffect;
    [Header("Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healthFillRate;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _scale;
    [Header("Behavior")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _detectionInterval;
    [SerializeField] private List<SOAnimal> _prey;

    public Sprite Sprite { get => _sprite; }
    public Sprite PossessedSprite { get => _possessedSprite; }
    public string Id { get => _id; }
    public string Name { get => _name; }
    public int SpawnCost { get => _spawnCost; }
    public bool PlayEatenEffect { get => _playEatenEffect; }
    public float MaxHealth { get => _maxHealth; }
    public float HealthFillRate { get => _healthFillRate; }
    public float Damage { get => _damage; }
    public float AttackRange { get => _attackRange; }
    public float Scale { get => _scale; }
    public float MovementSpeed { get => _movementSpeed; }
    public float DetectionRadius { get => _detectionRadius; }
    public float DetectionInterval { get => _detectionInterval; }
    public List<SOAnimal> Prey { get => _prey; }
}