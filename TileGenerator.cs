using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    private int spawnPositions = 0;
    [SerializeField] private Transform player;
    private List<GameObject> activeTiles = new List<GameObject>();
    [Header("Constantes")]
    private const int START_TILES_COUNT = 10;
    private const int TILE_LENGTH = 100;
    private void Start()
    {
        Spawn(0);
        for (int i = 0; i < START_TILES_COUNT - 1; i++)
            Spawn(Random.Range(0, tilePrefabs.Length));
    }
    void FixedUpdate()
    {
        if (player.position.z - 10 > spawnPositions - (START_TILES_COUNT * TILE_LENGTH))
        {
            Spawn(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    private void Spawn(int tileIndex)
    {
        GameObject newtile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPositions, Quaternion.identity);
        activeTiles.Add(newtile);
        spawnPositions += TILE_LENGTH;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
