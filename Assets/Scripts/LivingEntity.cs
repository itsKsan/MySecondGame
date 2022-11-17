using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    protected enum State
    {
        Idle,
        Walking,
        Chasing,
        Attacking
    }
    
    [SerializeField] private float maxHealth;
    private float _health;
    private bool _dead;

    public event Action OnDeath;

    protected virtual void Start()
    {
        _health = maxHealth;
    }


    protected virtual void TakeHit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _dead = true;
        OnDeath?.Invoke();

        Destroy(gameObject); // TODO : SWITCH TO OBJECT PULL
    }
}