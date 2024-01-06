using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class loadRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Action OnRewardedAdSuccess;
    public string androidAdUnitId;
    public string iosAdUnitId;

    string adUnitId;

    void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        print("Loading Rewarded!!");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(adUnitId))
        {
            print("Rewarded loaded!!");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
         print($"Rewarded failed to load. Placement ID: {placementId}, Error: {error}, Message: {message}");
    }

    public void ShowAd(Action OnSuccess)
    {
        OnRewardedAdSuccess = OnSuccess;
        print("showing Rewarded ad!!");
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Rewarded clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            print("Rewarded show complete, Distribute the rewards");
            OnRewardedAdSuccess?.Invoke();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Rewarded show failure. Error: " + error.ToString() + ", Message: " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Rewarded show start");
    }

    public void OnUnityAdsShowReady(string placementId)
    {
        print("Rewarded show ready");
    }

    
}
