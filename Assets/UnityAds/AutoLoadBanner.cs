using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AutoLoadBanner : MonoBehaviour
{
    public string androidAdUnitId;
    public string iosAdUnitId;

    string adUnitId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Start()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnitId;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnitId;
        #endif

        Advertisement.Banner.SetPosition(bannerPosition);

        // Load and show banner ad automatically when the script starts
        LoadAndShowBanner();
    }

    // Method to load and show the banner ad
    void LoadAndShowBanner()
    {
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadError
        };

        Advertisement.Banner.Load(adUnitId, loadOptions);
    }

    void OnBannerLoaded()
    {
        print("Banner Loaded!!");
        // Automatically show the banner ad when loaded
        ShowBannerAd();
    }

    void OnBannerLoadError(string error)
    {
        print("Banner failed to load " + error);
    }

    void ShowBannerAd()
    {
        BannerOptions showOptions = new BannerOptions
        {
            showCallback = OnBannerShown,
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden
        };

        Advertisement.Banner.Show(adUnitId, showOptions);
    }

    void OnBannerShown()
    {
        print("Banner Shown!!");
    }

    void OnBannerClicked()
    {
        print("Banner Clicked!!");
    }

    void OnBannerHidden()
    {
        print("Banner Hidden!!");
        // Optionally, load and show a new banner when the current one is hidden
        LoadAndShowBanner();
    }

    // Method to hide the banner ad
    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
