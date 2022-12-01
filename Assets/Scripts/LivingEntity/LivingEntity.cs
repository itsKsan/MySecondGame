using System;
using UnityEngine;

namespace LivingEntity
{
    public class LivingEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] protected float movementSpeed = 2f;
        
        private float _health;
        private bool _dead;

        public event Action OnDeath;

        protected virtual void Start()
        {
            _health = maxHealth;
        }

        public virtual void TakeHit(float damage)
        {
            _health -= damage;

            if (_health <= 0 && !_dead)
                Die();
        }

        private void Die()
        {
            _dead = true;
            OnDeath?.Invoke();

            Destroy(gameObject); // TODO : SWITCH TO OBJECT PULL
        }
    }
}