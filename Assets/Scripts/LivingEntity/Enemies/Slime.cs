using System;
using System.Collections;
using UnityEngine;

namespace LivingEntity.Enemies
{
    public class Slime : Enemy
    {
        [Header("Attack Settings")] 
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private float maxChaseDistance = 10f;
        [SerializeField] private float timeBetweenAttacks = 1;
    
    
        private Vector2 _newPosition;
        private float _targetDistance;

        private  float _myCollisionRadius;

        protected override void Awake()
        {
            base.Awake();
            _myCollisionRadius = GetComponentInChildren<CircleCollider2D>().radius;
        }

        private void Update()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    RandomMovement();
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
                case State.Attacking:
                    break;
                case State.Walking:
                    // DO SOMETHING
                    break;
                case State.SpecialAttack:
                    // DO SOMETHING
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void ChaseTarget()
        {
            _targetDistance = Vector2.Distance(transform.position, Target.transform.position);
        
            if (_targetDistance < attackDistance && Time.time > NextAttackTime)     // TODO : May cause bugs (change if statements)
            {
                NextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(nameof(AttackTarget));
            }
            else if (_targetDistance < maxChaseDistance && _targetDistance > attackDistance)
            {
                Pathfinder.SetDestination(Target.position);
            }
            else if (_targetDistance > maxChaseDistance)
            {
                CurrentState = State.Idle;
            }
        }
    
        private IEnumerator AttackTarget()
        {
            CurrentState = State.Attacking;
        
            Vector3 originalPosition = transform.position;
            Vector3 dirToTarget = (Target.position - transform.position).normalized;
        
            Vector3 attackPosition = Target.position - dirToTarget * (_myCollisionRadius);
        
            float attackSpeed = 3;
            float percent = 0;
        
            while (percent <= 1) {

                percent += Time.deltaTime * attackSpeed;
                float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

                yield return null;
            }

            CurrentState = State.Chasing;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
        }
    }
}