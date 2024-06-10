using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI")]
    private int currentcharacterindex = 0;
    public GameObject[] Characters;
    public Text NameText;
    public Text SpeedText;
    public Text JumpForceText;
    public Text BufsText;
    public Text MoneyText;
    public Button Buybutton;
    public CharacterData[] CharacterStats;
    [Header("References")]
    public CharacterSelector characterSelector;
    public CharacterShop[] characterShop;
    private int characterscount = 1;
    private void Start()
    {
        currentcharacterindex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in Characters)
            character.SetActive(false);
        Characters[currentcharacterindex].SetActive(true);
        characterSelector.SelectCharacter();
        foreach (CharacterShop character in characterShop)
        {
            if(character.price == 0 || PlayerPrefs.HasKey("unlockallcharacters"))
                character.IsUnlocked = true;
            else character.IsUnlocked = PlayerPrefs.GetInt(character.Name, 0) == 0 ? false : true;
            if (characterscount == characterShop.Length)
                AchievementManager.instance.Complete(3, 0);
        }
        UpdateUI();
    }
    private void Next()
    {
        Characters[currentcharacterindex].SetActive(false);
        currentcharacterindex++;
        if(currentcharacterindex == Characters.Length) currentcharacterindex = 0;
        Characters[currentcharacterindex].SetActive(true);
        UpdateUI();
        CharacterShop ch = characterShop[currentcharacterindex];
        if (!ch.IsUnlocked) return;
        PlayerPrefs.SetInt("SelectedCharacter", currentcharacterindex);
        characterSelector.SelectCharacter();
    }
    private void Previos()
    {
        Characters[currentcharacterindex].SetActive(false);
        currentcharacterindex--;
        if (currentcharacterindex == -1) currentcharacterindex = Characters.Length - 1;
        Characters[currentcharacterindex].SetActive(true);
        UpdateUI();
        CharacterShop ch = characterShop[currentcharacterindex];
        if (!ch.IsUnlocked) return;
        PlayerPrefs.SetInt("SelectedCharacter", currentcharacterindex);
        characterSelector.SelectCharacter();
    }
    public void UpdateUI()
    {
        NameText.text = CharacterStats[currentcharacterindex].Name;
        SpeedText.text = "Speed:" + CharacterStats[currentcharacterindex].speed.ToString();
        JumpForceText.text = "JumpForce:" +CharacterStats[currentcharacterindex].JumpForce.ToString();
        BufsText.text = "Buff:" + CharacterStats[currentcharacterindex].buff.ToString();
        CharacterShop ch = characterShop[currentcharacterindex];
        if (ch.IsUnlocked)
        {
            Buybutton.gameObject.SetActive(false);
        }
        else
        {
            Buybutton.gameObject.SetActive(true);
            Buybutton.GetComponentInChildren<Text>().text = "Buy:" + ch.price;
            if(ch.price <= PlayerPrefs.GetInt("Money", 0))
            {
                Buybutton.interactable = true;
            }
            else
            {
                Buybutton.interactable = false;
            }
        }
    }
    public void Buy()
    {
        CharacterShop ch = characterShop[currentcharacterindex];
        PlayerPrefs.SetInt(ch.Name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", currentcharacterindex);
        ch.IsUnlocked = true;
        characterscount++;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - ch.price);
        MoneyText.text = PlayerPrefs.GetInt("Money",0).ToString();
        characterSelector.SelectCharacter();
        UpdateUI();
    }
}
