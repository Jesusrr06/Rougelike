using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic ;
public class BoardManager : MonoBehaviour
{
    public class CellData
    {   public GameObject ContainedObject;

        public bool Passable;
    }
    private CellData[,] m_BoardData;
    private Grid m_Grid;
    private Tilemap m_Tilemap;
    public int Width;
    public int Height;
    public Tile[] GroundTiles;


    public Tile[] WallTiles;
public GameObject FoodPrefab;

void GenerateFood()
{
   int foodCount = 5;
   for (int i = 0; i < foodCount; ++i)
   {
       int randomX = Random.Range(1, Width-1);
       int randomY = Random.Range(1, Height-1);
       CellData data = m_BoardData[randomX, randomY];
       if (data.Passable && data.ContainedObject == null)
       {
           GameObject newFood = Instantiate(FoodPrefab);
           newFood.transform.position = CellToWorld(new Vector2Int(randomX, randomY));
           data.ContainedObject = newFood;
       }
   }
}
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public PlayerController Player;
public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_Grid = GetComponentInChildren<Grid>();

        m_BoardData = new CellData[Width, Height];

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        GenerateFood();
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width
            || cellIndex.y < 0 || cellIndex.y >= Height)
        {
            return null;
        }

        return m_BoardData[cellIndex.x, cellIndex.y];
    }

    // Update is called once per frame
    void Update()
    {

    }

}
