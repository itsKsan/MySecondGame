using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    private float _horizontalMovement;
    private float _verticalMovement;

    public Vector2 MovementVector { get; private set; }

    [SerializeField]private float speed = 100f;
    
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");
        MovementVector = new Vector2(_horizontalMovement, _verticalMovement).normalized;
        _rigidbody2D.velocity = MovementVector * (speed * Time.deltaTime);
    }
}
