using UnityEngine;
using UnityEngine.AI;

namespace Map
{
    public class NavMeshManager : MonoBehaviour
    {
        private NavMeshSurface _meshSurface;

        private void Awake()
        {
            _meshSurface = GetComponent<NavMeshSurface>();
        }

        public void BakeNavMesh()
        {
            _meshSurface.BuildNavMesh();
        }

        public void UpdateNavMesh()
        {
            if (_meshSurface.navMeshData != null)
                _meshSurface.UpdateNavMesh(_meshSurface.navMeshData);
        }
    }
}