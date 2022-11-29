using LivingEntity;
using Unity.Mathematics;
using UnityEngine;

namespace Map
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private int enemiesCount = 0;
        
        
        
        public void SetPlayerPosition(Vector3Int position)
        {
            player.transform.position = position;
        }
        
        public void SpawnEnemies(Vector3Int position)
        {
            var randomEnemy = enemies[0];
            var newEnemy = Instantiate(randomEnemy, position, quaternion.identity);
            newEnemy.transform.parent = transform;
        }
    }
}