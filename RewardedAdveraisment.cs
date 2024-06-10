using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdveraisment : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public static RewardedAdveraisment instance;
    public const string AndroidAdvretaismentId = "Rewarded_Android";
    public const string IOSAdvretaismentId = "Rewarded_iOS";
    public Button[] RewardedButtons;
    public string AdvertaismentId;
    public Text MoneyText;
    public void OnUnityAdsAdLoaded(string placementId)
    {
        foreach (Button button in RewardedButtons)
            if (placementId.Equals(AdvertaismentId))
            {
                button.onClick.AddListener(ShowAdvertaisment);
                button.interactable = true;
            }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new System.NotImplementedException();
        if (placementId.Equals(AdvertaismentId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Advertisement.Load(AdvertaismentId, this);
        }
    }
    public void Add_Money(int amount)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + amount);
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        AdvertaismentId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSAdvretaismentId
            : AndroidAdvretaismentId;
        foreach (Button button in RewardedButtons)
            button.interactable = false;
    }
    private void Start() => StartCoroutine(AdvertaismentLoading());
    private IEnumerator AdvertaismentLoading()
    {
        yield return new WaitForSeconds(1);
        LoadAdvertaisment();
    }
    private void LoadAdvertaisment() => Advertisement.Load(AdvertaismentId, this);
    public void ShowAdvertaisment()
    {
        foreach (Button button in RewardedButtons)
            button.interactable = false;
        Advertisement.Show(AdvertaismentId, this);
    }
}
