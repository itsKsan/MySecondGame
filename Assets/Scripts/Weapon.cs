using UnityEngine;
using LivingEntity;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    [SerializeField] private Transform owner;

    private void Start()
    {
        owner = transform.root;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<IDamageable>().TakeHit(damage);
            col.GetComponent<Enemy>().SetTarget(owner);
        }
    }
}
