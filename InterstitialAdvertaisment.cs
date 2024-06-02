using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdvertaisment : MonoBehaviour,IUnityAdsLoadListener,IUnityAdsShowListener
{
    public static InterstitialAdvertaisment instance;
    public string AndroidAdvretaismentId = "Interstitial_Android";
    public string IOSAdvretaismentId = "Interstitial_iOS";
    public string AdvertaismentId;
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

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new System.NotImplementedException();
        LoadAdvertaisment();
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
    void Awake()
    {
        instance = this;
        AdvertaismentId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSAdvretaismentId
            : AndroidAdvretaismentId;
    }
    public void LoadAdvertaisment()
    {
        Advertisement.Load(AdvertaismentId, this);
    }
    public void ShowAdvertaisment()
    {
        Advertisement.Show(AdvertaismentId, this);
    }
}
