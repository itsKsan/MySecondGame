using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LivingEntity.Enemies
{
    public class Skeleton : Enemy
    {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        
        private Animator _animator;
        
        private Vector2 _newPosition;


        protected override void Awake()
        {
            base.Awake();
            
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void Update()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    _animator.SetBool(IsWalking, false);
                    break;
                case State.Walking:
                    RandomMovement();
                    _animator.SetBool(IsWalking, true);
                    break;
                case State.Chasing:
                    // DO SOMETHING
                    break;
                case State.Attacking:
                    break;
                case State.SpecialAttack:
                    // DO SOMETHING
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
        
    }
}
