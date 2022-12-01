using System;
using Map.Island;
using UnityEngine;

namespace LivingEntity.Player
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private IslandsManager islandsManager;
        [SerializeField] private PlayerMovement input;

        private Vector2 cursorPositionCorrection;

        private void Start()
        {
            UnityEngine.Cursor.visible = false;
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, 1));
            transform.position = input.MousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                var pos = new Vector3Int((int) input.MousePosition.x, (int) input.MousePosition.y, 0);
                islandsManager.EnterIsland(pos);
            }
            
        }
    }
}