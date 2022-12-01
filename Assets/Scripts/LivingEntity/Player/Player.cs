using UnityEngine;

namespace LivingEntity.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : LivingEntity
    {
        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        protected override void Start()
        {
            base.Start();
            playerMovement.SetSpeed(movementSpeed);
        }
        
    }
}