using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private int currentCharacterIndex = 0;
    public GameObject[] Characters;
    private PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        SelectCharacter();
    }
    public void SelectCharacter()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject skin in Characters)
            skin.SetActive(false);
        Characters[currentCharacterIndex].SetActive(true);
        player.UpdateCharacterStats();
    }
}
