using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Map
{
    public class WorldMapBuilder : MonoBehaviour
    {
        [Header("Map Settings")]
        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        [SerializeField] private int waterPercent;

        [Header("Tilemaps")] 
        [SerializeField] private Tilemap islandTilemap;
        [SerializeField] private Tilemap waterTilemap;

    
        [Header("Tiles")]
        [SerializeField] private TileBase islandTile;
        [SerializeField] private TileBase waterTile;

        private int[,] _map;
        private bool _enableSmoothing;

        private void Start()
        {
            GenerateMap();
        }

        private void GenerateMap()
        {
            _map = new int[mapWidth, mapHeight];
            RandomFillMap();
            
            SetTiles();
        }

        public void ClearMap()
        {
            islandTilemap.ClearAllTiles();
            waterTilemap.ClearAllTiles();
        }
        
        private void RandomFillMap() 
        {
            for (int x = 0; x < mapWidth; x ++)
            for (int y = 0; y < mapHeight; y ++) 
            {
                if (x == 0 || x == mapWidth-1 || y == 0 || y == mapHeight -1) 
                    _map[x,y] = 1;
                else 
                    _map[x,y] = (Random.Range(0,100) < waterPercent)? 1: 0;
            }
        }


        private void SetTiles()
        {
            for (int x = 0; x < mapWidth; x++)
                for (int y = 0; y < mapHeight; y++)
                {
                    if (_map[x,y] == 1)
                    {
                        waterTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                    }
                    else
                    {
                        var tilePosition = new Vector3Int(x, y, 0);
                        islandTilemap.SetTile(tilePosition, islandTile);
                    }
                }
        }
    }
}