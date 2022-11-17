using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Slime>().TakeHit(damage);
        }
    }
}
