using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject Achievements;
    public GameObject Settings;
    public GameObject Character;
    public GameObject MenuPanel;
    public GameObject Player;
    public GameObject Cursor;
    public GameObject[] Modificators;
    // Start is called before the first frame update
    void Start()
    {
        CloseAll();
        foreach (GameObject Modificator in Modificators)
        {
            Modificator.SetActive(false);
        }
    }
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
        Achievements.SetActive(false);
        Settings.SetActive(false);
        Player.SetActive(false);
        Cursor.SetActive(false);
        Character.SetActive(false);
    }
    public void OpenSkin()
    {
        ShopPanel.SetActive(false);
        Achievements.SetActive(false);
        Settings.SetActive(false);
        Player.SetActive(false);
        Cursor.SetActive(false);
        Character.SetActive(true);
    }
    public void OpenAchivments()
    {
        Player.SetActive(false);
        ShopPanel.SetActive(false);
        Achievements.SetActive(true);
        Settings.SetActive(false);
        Cursor.SetActive(false);
        Character.SetActive(false);
    }
    public void OpenSetthings()
    {
        Player.SetActive(false);
        ShopPanel.SetActive(false);
        Achievements.SetActive(false);
        Settings.SetActive(true);
        Cursor.SetActive(false);
        Character.SetActive(false);
    }
    public void CloseAll()
    {
        Player.SetActive(true);
        ShopPanel.SetActive(false);
        Achievements.SetActive(false);
        Settings.SetActive(false);
        Cursor.SetActive(true);
        Character.SetActive(false);
        MenuPanel.SetActive(false);
    }
    public void Restart()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Resume()
    {
        PlayerPrefs.Save();
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MenuOpen()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void MenuPanelOpen()
    {
        PlayerPrefs.Save();
        MenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
