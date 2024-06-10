using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effect;
    void Start()
    {
        instance = this;
        music.Play();
    }
    public void PlayEffect(AudioClip clip) => effect.PlayOneShot(clip);
}
