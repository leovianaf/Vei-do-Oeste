using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public Tilemap groundTilemap;      // Refer�ncia ao Tilemap de Ground
    public GameObject healItemPrefab;
    public float spawnInterval = 10f;
    private float timeSinceLastSpawn = 0f;

    void Update()
    {
        // Contador de tempo para spawn de itens a cada 10 segundos
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnItems();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnItems()
    {
        // Spawn do item de cura
        Vector3Int healPosition = GetRandomGroundTilePosition();
        if (healPosition != Vector3Int.zero)
        {
            Instantiate(healItemPrefab, groundTilemap.CellToWorld(healPosition), Quaternion.identity);
        }
    }

    Vector3Int GetRandomGroundTilePosition()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        for (int i = 0; i < 100; i++) // Tenta 100 vezes para evitar spawn em �reas inv�lidas
        {
            // Gera uma posi��o aleat�ria dentro do Tilemap
            Vector3Int randomPos = new Vector3Int(
                Random.Range(bounds.xMin, bounds.xMax),
                Random.Range(bounds.yMin, bounds.yMax),
                0
            );

            // Verifica se o tile nessa posi��o � do tipo Ground
            if (groundTilemap.HasTile(randomPos))
            {
                return randomPos;
            }
        }
        return Vector3Int.zero; // Retorna zero caso n�o encontre uma posi��o v�lida
    }
}
