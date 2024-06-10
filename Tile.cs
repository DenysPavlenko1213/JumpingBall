using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private GameObject[] Money;
    [SerializeField] private GameObject[] PowerUps;
    private void Start()
    {
        foreach (GameObject obstacle in Obstacles)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1) obstacle.SetActive(true);
            else if (rand == 0) obstacle.SetActive(false);
        }
        foreach (GameObject money in Money)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1) money.SetActive(true);
            else if (rand == 0) money.SetActive(false);
        }
        foreach (GameObject PowerUp in PowerUps)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1) PowerUp.SetActive(true);
            else if (rand == 0) PowerUp.SetActive(false);
        }
    }
}
