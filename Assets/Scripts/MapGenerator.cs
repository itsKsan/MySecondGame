using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int smoothIterations = 5;
    [SerializeField] private int waterPercent;

    [Header("Tilemaps")] 
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap waterTilemap;

    
    [Header("Tiles")]
    [SerializeField] private TileBase groundTile;
    [SerializeField] private RuleTile waterTile;
    
    
    private int[,] _map;


    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            groundTilemap.ClearAllTiles();
            waterTilemap.ClearAllTiles();
            GenerateMap();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            waterTilemap.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }
    private void GenerateMap()
    {
        _map = new int[mapWidth, mapHeight];
        RandomFillMap();
        SmoothMap();
        SetTiles();
    }

    private void RandomFillMap() 
    {
        for (int x = 0; x < mapWidth; x ++)
            for (int y = 0; y < mapHeight; y ++) 
            {
                if (x == 0 || x == mapWidth-1 || y == 0 || y == mapHeight -1) 
                {
                    _map[x,y] = 1;
                }
                else 
                {
                    _map[x,y] = (Random.Range(0,100) < waterPercent)? 1: 0;
                }
            }
    }

    private void SmoothMap() 
    {
        for (int i = 0; i < smoothIterations; i++)
        for (int x = 0; x < mapWidth; x ++) 
            for (int y = 0; y < mapHeight; y ++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x,y);

                _map[x, y] = neighbourWallTiles switch
                {
                    > 4 => 1,
                    < 4 => 0,
                    _ => _map[x, y]
                };
            }
    }
    
    private int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) 
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) 
            {
                if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight) 
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += _map[neighbourX,neighbourY];
                    }
                }
                else 
                {
                    wallCount ++;
                }
            }

        return wallCount;
    }

    private void SetTiles()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (_map[x,y] == 1)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else
                {
                    waterTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                }
            }
        }
    }
}
