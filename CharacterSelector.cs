using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private int currentcharacterindex = 0;
    public GameObject[] Characters;
    public static CharacterSelector instance;
    private PlayerController player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        SelectCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCharacter()
    {
        currentcharacterindex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject skin in Characters)
        {
            skin.SetActive(false);
        }
        Characters[currentcharacterindex].SetActive(true);
        player.UpdateCharacterStats();
    }
}
