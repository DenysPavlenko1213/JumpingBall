using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Variebles")]
    private CharacterController controller;
    public Vector3 dir;
    public GameObject GUI;
    public GameObject MenuPanel;
    public GameObject GameOverPanel;
    bool GameStarted;
    [SerializeField]private float speed = 2;
    private const int maxSpeed = 100;
    public FixedJoystick joystick;
    public int money;
    public Score scoremanger;
    public Text recordText;
    public AchievementManager achievementManager;
    int tryCount = 0;
    private int currentcharacterindex;
    [Header("Gravity")]
    public float jumpForce;
    private float gravity = -9.8f;
    public CharacterData[] CharacterStats;
    bool IsGrounded = false;
    public LayerMask ground;
    private Vector3 velocity = Vector3.zero;
    public float grounddistance;
    bool FireResist;
    [Header("GUI")]
    public Text MoneyText;
    public GameObject[] Modificators;
    [Header("Music")]
    public AudioClip moneyeffect;
    public AudioClip jump;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        currentcharacterindex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        UpdateCharacterStats();
        GUI.SetActive(false);
        GameOverPanel.SetActive(false);
        MenuPanel.SetActive(true);
        scoremanger.ScoreMultipleire = 0;
        InterstitialAdvertaisment.instance.LoadAdvertaisment();
        //RewardedAdveraisment.instance.LoadAdvertaisment();
        money = PlayerPrefs.GetInt("Money");
        MoneyText.text = money.ToString();
        StartCoroutine(SpeedIncrease());
        foreach (GameObject Modificator in Modificators)
        {
            Modificator.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(0, 1, transform.position.z);
            GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Gravity();
        StartGame();
        Jump();
        if (GameStarted)
        {
            dir.x = joystick.Horizontal;
            dir.z = speed;
            GUI.SetActive(true);
            MenuPanel.SetActive(false);
            transform.Rotate(50 * Time.deltaTime, 0, 0);
            controller.Move(dir * Time.deltaTime);
        }
    }
    bool StartGame()
    {
        if (SwipeController.swipeLeft)
        {
            GameStarted = true;
        }
        if (SwipeController.swipeRight)
        {
            GameStarted = true;
        }
        if (SwipeController.swipeUp)
        {
            GameStarted = true;
        }
        return GameStarted;
    }
    void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(transform.position, grounddistance, ground);
    }
    void Gravity()
    {
        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Jump()
    {
        if (IsGrounded && SwipeController.swipeUp)
        {
            SoundManager.instance.PlayEffect(jump);
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    void GameOver()
    {
        int lastscore = PlayerPrefs.GetInt("lastscore");
        int recordscore = PlayerPrefs.GetInt("recordscore");
        if (lastscore > recordscore)
        {
            recordscore = lastscore;
            PlayerPrefs.SetInt("recordscore",recordscore);
            recordText.text = "Record:" + recordscore.ToString();
        }
        else
        {
            recordText.text = "Record:" + recordscore.ToString();
        }
        if (lastscore >= 100)
        {
            achievementManager.Complete(0, 50);
        }
        if(lastscore >= 1000)
        {
            achievementManager.Complete(1, 100);
        }
        if (lastscore >= 10000)
        {
            achievementManager.Complete(5, 50);
        }
        if (lastscore >= 100000)
        {
            achievementManager.Complete(6, 50);
        }
        if (!FireResist || CharacterStats[currentcharacterindex].buff == CharacterData.Buff.Protection)
        {
            GUI.SetActive(false);
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstecle"))
        {
            int lastscore = int.Parse(scoremanger.ScoreText.text.ToString());
            PlayerPrefs.SetInt("lastscore", lastscore);
            GameOver();
            int rand = Random.Range(0, 3);
            if (rand == 1 && !FireResist && PlayerPrefs.GetInt("removeadvertaisments")==0)
            {
                //Show
                InterstitialAdvertaisment.instance.ShowAdvertaisment();
            }
            if (FireResist)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Money"))
        {
            if (CharacterStats[currentcharacterindex].buff == CharacterData.Buff.DoubleMoney)
            {
                money += 2;
                PlayerPrefs.SetInt("Money", money);
                MoneyText.text = money.ToString();
                Destroy(other.gameObject);
                SoundManager.instance.PlayEffect(moneyeffect);
            }
            else
            {
                money++;
                PlayerPrefs.SetInt("Money", money);
                MoneyText.text = money.ToString();
                Destroy(other.gameObject);
                SoundManager.instance.PlayEffect(moneyeffect);
            }
        }
        else if (other.CompareTag("Protection"))
        {
            StartCoroutine(Fireresist());
            Destroy(other.gameObject);
            SoundManager.instance.PlayEffect(moneyeffect);
        }
        else if (other.CompareTag("Score"))
        {
            StartCoroutine(ScoreIncrease());
            Destroy(other.gameObject);
            SoundManager.instance.PlayEffect(moneyeffect);
        }
        else if (other.CompareTag("JumpUp"))
        {
            StartCoroutine(JumpForceIncrease());
            Destroy(other.gameObject);
            SoundManager.instance.PlayEffect(moneyeffect);
        }
    }
    public void Rise()
    {
        Time.timeScale = 1;
        transform.position = new Vector3(0,1,transform.position.z);
        StopAllCoroutines();
        SoundManager.instance.PlayEffect(jump);
        velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        foreach (GameObject Modificator in Modificators)
        {
            Modificator.SetActive(false);
        }
        GUI.SetActive(true);
        GameOverPanel.SetActive(false);
        StartCoroutine(Fireresist());
    }
    public void UpdateCharacterStats()
    {
        jumpForce = CharacterStats[currentcharacterindex].JumpForce;
        speed = CharacterStats[currentcharacterindex].speed;
        if (CharacterStats[currentcharacterindex].buff == CharacterData.Buff.DoubleScore)
        {
            scoremanger.ScoreMultipleire = 2;
        }
        else
        {
            scoremanger.ScoreMultipleire = 0;
        }
    }
    IEnumerator Fireresist()
    {
        FireResist = true;
        Modificators[1].SetActive(true);
        yield return new WaitForSeconds(10);
        Modificators[1].SetActive(false);
        FireResist = false;
    }
    IEnumerator ScoreIncrease()
    {
        scoremanger.ScoreMultipleire = 2;
        Modificators[0].SetActive(true);
        yield return new WaitForSeconds(10);
        Modificators[0].SetActive(false);
        scoremanger.ScoreMultipleire = 0;
    }
    IEnumerator JumpForceIncrease()
    {
        jumpForce += 5;
        Modificators[2].SetActive(true);
        yield return new WaitForSeconds(10);
        Modificators[2].SetActive(false);
        jumpForce -= 5;
    }
    IEnumerator SpeedIncrease()
    {
        if(speed < maxSpeed)
        {
            yield return new WaitForSeconds(5f);
            speed++;
            StartCoroutine(SpeedIncrease());
        }
    }
}
