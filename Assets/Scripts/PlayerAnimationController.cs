using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerActions))]
public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    
    [SerializeField] private Transform GFX;
    
    private Animator _animator;
    private PlayerMovement _playerMovement;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        SetAnimations();
        FlipCharacter();
    }

    private void FlipCharacter()
    {
        switch (_playerMovement.MovementVector.x)
        {
            case < 0:
                GFX.localScale = new Vector3(-1, 1, 1);
                break;
            case > 0:
                GFX.transform.localScale = Vector3.one;
                break;
        }
    }
    private void SetAnimations()
    {
        if (_playerMovement.MovementVector.x != 0 || _playerMovement.MovementVector.y != 0)
        {
            _animator.SetBool(IsWalking, true);
        }
        else if (Input.GetMouseButton(0))
        {
            _animator.SetBool(IsAttacking, true);
        }
        else
        {
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsAttacking, false);
        }
    }
}
