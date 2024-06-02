using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertaismentsManager : MonoBehaviour,IUnityAdsInitializationListener
{
    public string AndroidGameId = "5278325";
    public string IOSGameId = "5278324";
    public bool testMode;
    public string gameId;
    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {
        InitializeAdvertaisments();
    }
    public void InitializeAdvertaisments()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSGameId
            : AndroidGameId;
        Advertisement.Initialize(gameId, testMode);
    }
}
