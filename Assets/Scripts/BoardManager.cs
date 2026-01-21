using UnityEngine;
using UnityEngine.Tilemaps;
public class BoardManager : MonoBehaviour
{
    private Grid m_Grid;
    private Tilemap m_Tilemap;
    public int Width;
    public int Height;
    private CellData[,] m_BoardData;
    public Tile[] GroundTiles;


    public Tile[] WallTiles;
    public class CellData
    {
        public bool Passable;
    }

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public PlayerController Player;
    void Start()
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
        Player.Spawn(this, new Vector2Int(1, 1));

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
