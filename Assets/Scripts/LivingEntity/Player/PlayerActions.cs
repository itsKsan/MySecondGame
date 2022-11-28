using UnityEngine;

namespace LivingEntity.Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private Transform weapon;

        private PolygonCollider2D _weaponCollider;
    
        private void Awake()
        {
            _weaponCollider = weapon.GetComponent<PolygonCollider2D>();
        }


        private void Update()
        {
            _weaponCollider.isTrigger = Input.GetMouseButton(0);
        }
    }
}
