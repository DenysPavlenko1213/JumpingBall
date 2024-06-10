using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdvertaisment : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAdvertaisment instance;
    public const string ANDROID_ADVRETAISMENT_ID = "Interstitial_Android";
    public const string IOS_ADVRETAISMENT_ID = "Interstitial_iOS";
    private string AdvertaismentId;
    public void OnUnityAdsAdLoaded(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) => LoadAdvertaisment();

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
    private void Awake()
    {
        instance = this;
        AdvertaismentId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOS_ADVRETAISMENT_ID
            : ANDROID_ADVRETAISMENT_ID;
    }
    public void LoadAdvertaisment() => Advertisement.Load(AdvertaismentId, this);
    public void ShowAdvertaisment() => Advertisement.Show(AdvertaismentId, this);
}
