using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoSingleton<AdManager>
{
    public int count = 0, maxCount = 3;
    public BannerView bannerView;
    public InterstitialAd interstitial;
    public float intertstitialAdTimer = 5;   
    public void InitializeAds()
    {

        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestInterstitial();
    }

    void Update()
    {
        if (intertstitialAdTimer > 0)
        {
            intertstitialAdTimer -= Time.deltaTime;
        }

    }


    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2964581050083559/6146769664";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2964581050083559/1378438964";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public bool IsReadyInterstitialAd()
    {
        if (intertstitialAdTimer < 0 && interstitial.IsLoaded())
        {
            return true;
        }
        return false;
    }
    private void HandleOnAdOpening(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<AudioListener>().enabled = false;
    }
    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        intertstitialAdTimer = 5;
        Time.timeScale = 1;
        Camera.main.GetComponent<AudioListener>().enabled = true;
        RequestInterstitial();
    }
    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestInterstitial();
    }

}
