using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject[] Money;
    public GameObject[] PowerUps;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obstacle in Obstacles)
        {
            int rand = Random.Range(0, 2);
            if(rand == 1)
            {
                obstacle.SetActive(true);
            }
            else if (rand == 0)
            {
                obstacle.SetActive(false);
            }
        }
        foreach (GameObject money in Money)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                money.SetActive(true);
            }
            else if (rand == 0)
            {
                money.SetActive(false);
            }
        }
        foreach (GameObject PowerUp in PowerUps)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                PowerUp.SetActive(true);
            }
            else if (rand == 0)
            {
                PowerUp.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
