using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string Name;
        public int index;
        public bool IsCompleted;
        public Text completeText;
    }
    public Achievement[] Achievements;
    [SerializeField] private Text MoneyText;
    public static AchievementManager instance;
    private void Start()
    {
        instance = this;
        UpdateUI();
    }
    public void Complete(int index, int reward)
    {
        Achievements[index].IsCompleted = PlayerPrefs.GetInt(Achievements[index].Name, 0) == 0 ? false : true;
        if (Achievements[index].IsCompleted) return;
        PlayerPrefs.SetInt(Achievements[index].Name, 1);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + reward);
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        UpdateUI();
    }
    private void UpdateUI()
    {
        foreach (Achievement achievement in Achievements)
        {
            achievement.IsCompleted = PlayerPrefs.GetInt(achievement.Name, 0) == 0 ? false : true;
            achievement.completeText.gameObject.SetActive(achievement.IsCompleted);
        }
    }
}
