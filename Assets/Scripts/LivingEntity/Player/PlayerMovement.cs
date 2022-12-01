using UnityEngine;

namespace LivingEntity.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform gfx;
        [SerializeField] private Transform boatGfx;
        
        public Vector2 MovementVector { get; private set; }
        public Vector2 MousePosition { get; private set; }

        private Camera mainCamera;
        private Rigidbody2D _rigidbody2D;
    
        private float _horizontalMovement;
        private float _verticalMovement;

        private Vector3 defaultScale;
        private Vector3 defaultBoatScale;

        private float _speed;
    
    
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;
            
            defaultScale = gfx.transform.localScale;
            defaultBoatScale = boatGfx.transform.localScale;
        }

        private void FixedUpdate()
        {
            Move();
            MouseInput();
            FlipCharacter();
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        private void Move()
        {
            _horizontalMovement = Input.GetAxisRaw("Horizontal");
            _verticalMovement = Input.GetAxisRaw("Vertical");
            MovementVector = new Vector2(_horizontalMovement, _verticalMovement).normalized;
            _rigidbody2D.velocity = MovementVector * (_speed * Time.deltaTime);
        }

        private void MouseInput()
        {
            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MousePosition = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        }
        
        /// <summary>
        /// Flip Sprites
        /// </summary>
        private void FlipCharacter()
        {
            var scale = defaultScale;
            var boatScale = defaultBoatScale;
            
            switch (MovementVector.x)
            {
                case < 0:
                    Flip(Mathf.Sign(MovementVector.x));
                    break;
                case > 0:
                    Flip(Mathf.Sign(MovementVector.x));
                    break;
            }

            void Flip(float direction)
            {
                scale.x = direction * defaultScale.x;   // direction - 
                gfx.transform.localScale = scale;
                boatScale.x = direction * defaultBoatScale.x;
                boatGfx.transform.localScale = boatScale;
            }
        }
    }
}
