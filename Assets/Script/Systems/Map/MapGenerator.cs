using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public TileBase groundTile;
    public TileBase[] decorTiles;

    [Header("Chunk Settings")]
    private int chunkSizeX = 30;
    private int chunkSizeY = 30;
    public int ChunkSizeX => chunkSizeX;
    public int ChunkSizeY => chunkSizeY;

    public const int chunkCountX = 3;
    public const int chunkCountY = 3;

    [Range(0f, 1f)]
    public float decorChance = 0.05f;

    // runtime
    private Grid grid;
    public List<Chunk> chunks = new List<Chunk>();
    public List<Chunk> Chunks => chunks;

    [System.Serializable]
    public class Chunk
    {
        public GameObject root;
        public Tilemap ground;
        public Tilemap decor;

        public Vector2Int coord; // logical coord
    }

    void Start()
    {
        CreateChunkStructure();
        GenerateMap();
    }

    // =============================
    // CREATE STRUCTURE
    // =============================
    void CreateChunkStructure()
    {
        GameObject gridGO = new GameObject("MapGrid");
        grid = gridGO.AddComponent<Grid>();

        for (int cx = 0; cx < chunkCountX; cx++)
        {
            for (int cy = 0; cy < chunkCountY; cy++)
            {
                Chunk chunk = CreateChunk(cx, cy);
                chunks.Add(chunk);
            }
        }
    }

    Chunk CreateChunk(int cx, int cy)
    {
        Chunk chunk = new Chunk();

        chunk.coord = new Vector2Int(cx, cy);

        // ===== ROOT =====
        GameObject root = new GameObject($"Chunk_{cx}_{cy}");
        root.transform.SetParent(grid.transform, false);

        float worldX = cx * chunkSizeX;
        float worldY = cy * chunkSizeY;

        root.transform.position = new Vector3(worldX, worldY, 0);
        root.transform.localScale = Vector3.one;

        chunk.root = root;

        // ===== Ground =====
        GameObject groundGO = new GameObject("Ground");
        groundGO.transform.SetParent(root.transform, false);
        groundGO.transform.localPosition = Vector3.zero;

        chunk.ground = groundGO.AddComponent<Tilemap>();
        var groundRenderer = groundGO.AddComponent<TilemapRenderer>();
        groundRenderer.sortingOrder = 0;

        // ===== Decor =====
        GameObject decorGO = new GameObject("Decor");
        decorGO.transform.SetParent(root.transform, false);
        decorGO.transform.localPosition = Vector3.zero;

        chunk.decor = decorGO.AddComponent<Tilemap>();
        var decorRenderer = decorGO.AddComponent<TilemapRenderer>();
        decorRenderer.sortingOrder = 1;

        return chunk;
    }

    // =============================
    // GENERATE MAP
    // =============================
    void GenerateMap()
    {
        foreach (var chunk in chunks)
        {
            GenerateChunkTiles(chunk);
        }
    }

    void GenerateChunkTiles(Chunk chunk)
    {
        chunk.ground.ClearAllTiles();
        chunk.decor.ClearAllTiles();

        // local tile positions!
        for (int x = 0; x < chunkSizeX; x++)
        {
            for (int y = 0; y < chunkSizeY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                // Ground FULL
                chunk.ground.SetTile(pos, groundTile);

                // Decor random
                if (Random.value < decorChance)
                {
                    TileBase randomDecor =
                        decorTiles[Random.Range(0, decorTiles.Length)];

                    chunk.decor.SetTile(pos, randomDecor);
                }
            }
        }
    }
}