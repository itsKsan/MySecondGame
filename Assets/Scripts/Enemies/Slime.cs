using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

internal enum State
{
    Idle,
    Chasing,
    Attacking
}
public class Slime : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer;
    
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeBetweenMovements = 5f;
    [SerializeField] private float radius = 5f;

    [Header("On Hit Settings")]
    [SerializeField] private float flashTime = 0.5f;
    [SerializeField] private float flashSpeed = 0.1f;

    [Header("Attack Settings")] 
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float maxChaseDistance = 10f;
    [SerializeField] private float timeBetweenAttacks = 1;
    
    private float _nextAttackTime;
    
    private Vector2 _newPosition;
    private Transform _target;
    private float _targetDistance;

    [SerializeField] private State currentState;
    private  float _myCollisionRadius;

    private void Awake()
    {
        _myCollisionRadius = GetComponentInChildren<CircleCollider2D>().radius;
        
        currentState = State.Idle;
        health = maxHealth;
        
    }

    private void Start()
    {
        StartCoroutine(nameof(RandomPosition));
        
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // TODO : get target from hit info
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                transform.position = Vector2.Lerp(transform.position, _newPosition, speed * Time.deltaTime);
                break;
            case State.Chasing:
                ChaseTarget();
                break;
            case State.Attacking:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void ChaseTarget()
    {
        _targetDistance = Vector2.Distance(transform.position, _target.position);
        
        if (_targetDistance < attackDistance && Time.time > _nextAttackTime)     // TODO : May cause bugs (change if statements)
        {
            _nextAttackTime = Time.time + timeBetweenAttacks;
            StartCoroutine(nameof(AttackTarget));
        }
        else if (_targetDistance < maxChaseDistance && _targetDistance > attackDistance)
        {
            transform.position = Vector2.Lerp(transform.position, _target.position, speed * Time.deltaTime);
        }
        else if (_targetDistance > maxChaseDistance)
        {
            currentState = State.Idle;
        }
    }
    
    private IEnumerator AttackTarget()
    {
        currentState = State.Attacking;
        
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (_target.position - transform.position).normalized;
        
        Vector3 attackPosition = _target.position - dirToTarget * (_myCollisionRadius);
        
        float attackSpeed = 3;
        float percent = 0;
        
        while (percent <= 1) {

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
    }

    private IEnumerator RandomPosition()
    {
        while (enabled)
        {
            _newPosition = Random.insideUnitCircle * radius;
            yield return new WaitForSeconds(timeBetweenMovements);
        }
    }

    public void TakeHit(float damage)
    {
        health -= damage;

        currentState = State.Chasing;
        
        StartCoroutine(nameof(FlashOnHit));
        
        if (health <= 0)
            Destroy(gameObject);
    }

    private IEnumerator FlashOnHit()
    {
        var flashingFor = 0f;
        var newColor = Color.red;
        while (flashingFor < flashTime)
        {
            renderer.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            newColor = newColor == Color.red ? Color.white : Color.red;
        }
    }
}