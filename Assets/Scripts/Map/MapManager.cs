using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private NavMeshManager navMeshManager;
        private MapGenerator mapGenerator;

        private void Awake()
        {
            mapGenerator = GetComponentInChildren<MapGenerator>();
            navMeshManager = GetComponentInChildren<NavMeshManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateMap();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ClearMap();
            }
        }

        private void GenerateMap()
        {
            ClearMap();

            mapGenerator.GenerateMap();
            navMeshManager.BakeNavMesh();
        }

        private void ClearMap()
        {
            mapGenerator.ClearMap();
            navMeshManager.UpdateNavMesh();
        }
    }
}