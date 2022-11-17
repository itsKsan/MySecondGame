using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Skeleton : LivingEntity
    {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        [SerializeField] private float speed = 2f;
        [SerializeField] private float timeBetweenMovements = 5f;
        [SerializeField] private float radius = 5f;

        private Animator _animator;
        
        private Vector2 _newPosition;
        
        [SerializeField] private State currentState; // TODO : REMOVE SerializeField
        

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            currentState = State.Idle;
            StartCoroutine(nameof(RandomPosition));
        }

        private void Update()
        {
            switch (currentState)
            {
                case State.Idle:
                    _animator.SetBool(IsWalking, false);
                    break;
                case State.Walking:
                    transform.position = Vector2.Lerp(transform.position, _newPosition, speed * Time.deltaTime);
                    _animator.SetBool(IsWalking, true);
                    break;
                case State.Chasing:
                    // DO SOMETHING
                    break;
                case State.Attacking:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private IEnumerator RandomPosition()
        {
            while (enabled)
            {
                _newPosition = Random.insideUnitCircle * radius;
                yield return new WaitForSeconds(timeBetweenMovements);
            }
        }
    }
}
