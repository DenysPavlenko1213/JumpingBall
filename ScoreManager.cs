using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Text ScoreText;
    public int ScoreMultipleire = 0;
    void FixedUpdate()
    {
        int score = (int)(player.position.z / 2);
        score += ScoreMultipleire;
        ScoreText.text = score.ToString();
    }
}
