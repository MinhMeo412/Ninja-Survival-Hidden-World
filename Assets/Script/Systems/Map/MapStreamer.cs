using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapStreamer : MonoBehaviour
{
    public Transform player;

    private MapGenerator mapGen;

    public Vector2Int currentCenterCoord;

    private void Start()
    {
        mapGen = GetComponent<MapGenerator>();

        UpdateCenterChunk();
        RepositionChunks();
    }

    public void SetTarget(Transform target) => player = target;
    private void OnEnable() => PlayerSpawner.OnPlayerSpawned += SetTarget;
    private void OnDisable() => PlayerSpawner.OnPlayerSpawned -= SetTarget;

    private void LateUpdate()
    {
        if (player == null) return;
        UpdateCenterChunk();
    }

    void UpdateCenterChunk()
    {
        Vector3 playerPos = player.position;

        int cx = Mathf.FloorToInt(playerPos.x / mapGen.ChunkSizeX);
        int cy = Mathf.FloorToInt(playerPos.y / mapGen.ChunkSizeY);

        Vector2Int newCoord = new Vector2Int(cx, cy);

        if (newCoord != currentCenterCoord)
        {
            currentCenterCoord = newCoord;
            Debug.Log($"Player hiện đang ở Chunk: {currentCenterCoord}");

            RepositionChunks();
        }
    }

    void RepositionChunks()
    {
        if (mapGen == null || mapGen.Chunks == null) return;

        foreach (var chunk in mapGen.Chunks)
        {
            int newX = CalculateLoopingCoord(chunk.coord.x, currentCenterCoord.x, MapGenerator.chunkCountX);
            int newY = CalculateLoopingCoord(chunk.coord.y, currentCenterCoord.y, MapGenerator.chunkCountY);

            Vector2Int newCoord = new Vector2Int(newX, newY);

            if (newCoord != chunk.coord)
            {
                chunk.coord = newCoord;

                float worldX = chunk.coord.x * mapGen.ChunkSizeX;
                float worldY = chunk.coord.y * mapGen.ChunkSizeY;
                chunk.root.transform.position = new Vector3(worldX, worldY, 0);

                //Optional
                //UpdateChunkContent(chunk);
            }
        }
    }

    /// Hàm bổ trợ để tính toán vị trí vòng lặp của một điểm quanh một tâm
    int CalculateLoopingCoord(int currentCoord, int centerCoord, int range)
    {
        // Khoảng cách từ tâm đến biên (với range=3 thì half = 1)
        int half = range / 2;

        // Tính toán độ lệch
        int offset = currentCoord - centerCoord;

        // Nếu lệch quá xa về bên phải/trên
        while (offset > half) offset -= range;
        // Nếu lệch quá xa về bên trái/dưới
        while (offset < -half) offset += range;

        return centerCoord + offset;
    }
}
