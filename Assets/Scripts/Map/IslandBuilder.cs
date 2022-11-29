using UnityEngine;
using System.Collections.Generic;

namespace Map
{
    public class IslandBuilder : MonoBehaviour
    {
        [SerializeField] private int enemyCount = 20;
        
        [SerializeField]private NavMeshManager navMeshManager;
        [SerializeField]private SpawnerManager spawnerManager;
        [SerializeField]private TilemapBuilder tilemapBuilder;

        private List<int> occupiedTiles;
        
        private void Awake()
        {
            tilemapBuilder = GetComponentInChildren<TilemapBuilder>();
            navMeshManager = GetComponentInChildren<NavMeshManager>();
            spawnerManager = GetComponentInChildren<SpawnerManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                GenerateMap();

            if (Input.GetKeyDown(KeyCode.C))
                ClearMap();
        }

        private void GenerateMap()
        {
            tilemapBuilder.GenerateMap();
            navMeshManager.BakeNavMesh();
            SetLivingEntities();
        }

        private void ClearMap()
        {
            tilemapBuilder.ClearMap();
            navMeshManager.UpdateNavMesh();
        }

        private void SetLivingEntities()
        {
            var groundTilePos = tilemapBuilder.GroundTilesDictionary;
            spawnerManager.SetPlayerPosition(groundTilePos[0]);
            
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnerManager.SpawnEnemies(groundTilePos[Random.Range(1, groundTilePos.Count)]);
            }
        }
    }
}