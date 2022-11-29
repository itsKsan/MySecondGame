using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LivingEntity
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : LivingEntity
    {
        protected enum State
        {
            Idle,
            Walking,
            Chasing,
            Attacking,
            SpecialAttack
        }
        protected State CurrentState; // TODO : REMOVE SerializeField
        private SpriteRenderer[] gfx;
        
        [SerializeField] protected float timeBetweenMovements = 5f;
        [SerializeField] protected float patrollingRadius = 5f;
        
        
        
        [Header("On Hit Settings")]
        [SerializeField] private float flashTime = 0.5f;
        [SerializeField] private float flashSpeed = 0.1f;

        protected NavMeshAgent Pathfinder;
        protected Transform Target;
        
        protected float NextAttackTime;
        private float nextMoveTime;
        private Vector2 newPosition;

        protected virtual void Awake()
        {
            Pathfinder = GetComponent<NavMeshAgent>();
            gfx = GetComponentsInChildren<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            CurrentState = State.Idle;

            Pathfinder.updateRotation = false;
            Pathfinder.updateUpAxis = false;

            Pathfinder.speed = movementSpeed;
            timeBetweenMovements = Random.Range(4, 7);
        }

        public override void TakeHit(float damage)
        {
            base.TakeHit(damage);

            CurrentState = State.Chasing;
            StartCoroutine(nameof(FlashOnHit));
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }
        
        protected  void RandomMovement()
        {
            if (Time.time > nextMoveTime)
            {
                Vector2 localPos = transform.localPosition;
                
                nextMoveTime = Time.time + timeBetweenMovements;
                newPosition = Random.insideUnitCircle * patrollingRadius + localPos;
                Pathfinder.SetDestination(newPosition);
                
            }
        }
        
        private IEnumerator FlashOnHit()
        {
            var flashingFor = 0f;
            var newColor = Color.red;
            while (flashingFor < flashTime)
            {
                foreach (var sprite in gfx)
                    sprite.color = newColor;
                
                flashingFor += Time.deltaTime;
                yield return new WaitForSeconds(flashSpeed);
                flashingFor += flashSpeed;
                newColor = newColor == Color.red ? Color.white : Color.red;
            }
        }
    }
}
