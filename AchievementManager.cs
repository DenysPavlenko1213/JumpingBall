using System.Collections;
using System.Collections.Generic;
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
    public Text MoneyText;
    public static AchievementManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateUI();
    }
    public void Complete(int index, int reward)
    {
        Achievements[index].IsCompleted = PlayerPrefs.GetInt(Achievements[index].Name, 0) == 0 ? false : true;
        if (!Achievements[index].IsCompleted)
        {
            PlayerPrefs.SetInt(Achievements[index].Name, 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + reward);
            MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        foreach (Achievement achievement in Achievements)
        {
            achievement.IsCompleted = PlayerPrefs.GetInt(achievement.Name, 0) == 0 ? false : true;
            if (!achievement.IsCompleted) achievement.completeText.gameObject.SetActive(false);
            else achievement.completeText.gameObject.SetActive(true);
        }
    }
}
