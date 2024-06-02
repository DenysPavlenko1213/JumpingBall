using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float tilelength = 100;
    private float spawnPos = 0;
    public Transform player;
    List<GameObject> activetiles = new List<GameObject>();
    public int starttiles = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < starttiles; i++)
        {
            if(i == 0)
            {    
                Spawn(0);
            }
            Spawn(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.position.z - 10> spawnPos - (starttiles * tilelength))
        {
            Spawn(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    private void Spawn(int tileindex)
    {
        GameObject newtile = Instantiate(tilePrefabs[tileindex], transform.forward * spawnPos, Quaternion.identity);
        activetiles.Add(newtile);
        spawnPos += tilelength;
    }
    void DeleteTile()
    {
        Destroy(activetiles[0]);
        activetiles.RemoveAt(0);
    }
}
