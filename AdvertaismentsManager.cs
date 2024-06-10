using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertaismentsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public const string ANDROID_GAME_ID = "5278325";
    public const string IOS_GAME_ID = "5278324";
    [SerializeField] private bool testMode;
    private string gameId;
    public void OnInitializationComplete() => Debug.Log("Initialized");

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log(error);
    private void Start() => InitializeAdvertaisments();
    public void InitializeAdvertaisments()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOS_GAME_ID : ANDROID_GAME_ID;
        Advertisement.Initialize(gameId, testMode);
    }
}
