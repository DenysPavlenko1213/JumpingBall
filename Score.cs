using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text ScoreText;
    public int ScoreMultipleire = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int score = ((int)(player.position.z / 2));
        score += ScoreMultipleire;
        ScoreText.text = score.ToString();
    }
}
