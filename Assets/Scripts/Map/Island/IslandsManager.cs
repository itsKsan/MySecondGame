using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Map.Island
{
    public class IslandsManager : MonoBehaviour
    {
        private Dictionary<int, Vector3Int> islands = new Dictionary<int, Vector3Int>();

        [SerializeField] private GameObject debugSprite;

        public void AddIslandToDictionary(int id, Vector3Int position)
        {
            islands.Add(id,position);
            Instantiate(debugSprite, position, quaternion.identity);    //TODO : REMOVE
        }

        public void EnterIsland(Vector3Int position)
        {
            foreach (var island in islands)
            {
                if (island.Value == position)
                {
                    print("Entered island : " + island.Key);
                }
                else
                {
                    print("There`is no island!");
                }
            }
        }
        
        public void PrintIslandsInfo()
        {
            foreach (var island in islands)  
            {
                print("Island : " + island.Key + "| Position :" + island.Value);
            }
            
            print(islands.Count);
        }
    }
}