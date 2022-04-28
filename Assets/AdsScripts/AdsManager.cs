using System;
using UnityEngine;
using GoogleMobileAds.Common;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;




public class AdsManager : MonoBehaviour
{
  public  enum  AdsPosition
    {
      
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
        
    }

    #region Variables
    
    [Header("Plz Set Native Ads Position")]
    [Space]
    [Space]
    [Space]
    [Header("GoogleMobileAds-v6.1.1")]
    public AdsPosition NativeAdsPosition = AdsPosition.BottomLeft;
    [Space]
    [Space]
    [Header("Show Test Ads")]
    [Space]
    [Space]
    public bool isTestAds;
    [Space]
    [Space]
  
    [Header(" Show Default Banner  ")]
    [Space]
    [Space]
    public bool ShowDefaultBanner;
    string TestBannerID = "ca-app-pub-3940256099942544/6300978111";
    string TestMediumRectangleID = "ca-app-pub-3940256099942544/6300978111";
    string TestInterstitalID = "ca-app-pub-3940256099942544/1033173712";
    string TestRewardedInterstitialID = "ca-app-pub-3940256099942544/5354046379";
    string TestUnityID = "12345678";
    [Space]
    [Space]
    [Header("Original IDs")]

    public string BannerID, MediumRectangleID, InterstitalID, RewardedInterstitialID, UnityAdID;
    [Space]
    [Space]
    [Header("Links")]
    public string MoreGamesLink;
    public string RateUsLink;
    public string PrivacyLink;

    private bool isInitialized;
    private string RewardType;
    private int Reward;
    private BannerView bannerView;
    private BannerView bannerViewMediumRectangle;
    private InterstitialAd interstitialAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private string placementIdRewarded = "rewardedVideo";

    public static AdsManager Instance;

    #endregion

    // %%%%%%%%%%%%%%%%%%%%%%%%%%%  Initializing %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region Initializing


    public void Start()
    {


        isInitialized = false;

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            isInitialized = true;
            RequestAndLoadInterstitialAd();
            RequestAndLoadRewardedInterstitialAd();
            MobileAds.SetiOSAppPauseOnBackground(true);
            MobileAds.Initialize(HandleInitCompleteAction);
            Advertisement.Initialize(UnityAdID, isTestAds);
            Advertisement.Load(UnityAdID);
            // Initialize the Mobile Ads SDK.
            MobileAds.Initialize((initStatus) =>
            {
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            // The adapter initialization did not complete.
                            MonoBehaviour.print("Adapter: " + className + " not ready.");
                            break;
                        case AdapterState.Ready:
                            // The adapter was successfully initialized.
                            MonoBehaviour.print("Adapter: " + className + " is initialized.");
                            break;
                    }
                }
            });
         //   LoadMediumRectangle();
            //if (ShowDefaultBanner)
            //    ShowBanner("Default");

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        }




    }
    #endregion

    #region HELPER METHODS
    // %%%%%%%%%%%%%%%%%%%%%%%%%%% Admob Rewarded Create AdRequest  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
           .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    #endregion


    // %%%%%%%%%%%%%%%%%%%%%%%%%%% Interstitial Banner %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region Banner ADS
    private void RequestBanner(AdSize adSize, AdPosition pos)
    {
        string adUnitId;

        if (isTestAds)
        {
            adUnitId = TestBannerID;
        }
        else
        {
            adUnitId = BannerID;
        }

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        this.bannerView = new BannerView(adUnitId, adSize, pos);


        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        //  this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //  this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    void LoadBannerAd(AdSize adSize, AdPosition pos)
    {
        RequestBanner(adSize, pos);
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void ShowBanner(string name)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1) && isInitialized)
        {
            // %%%%%%%%%%%%%%%%%%%%%%%%%%% Top Banner Ad Position %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            if (name == "TopLeft")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.TopLeft);
            }
            else if (name == "TopCenter")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.Top);
            }
            else if (name == "TopRight")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.TopRight);
            }
            // %%%%%%%%%%%%%%%%%%%%%%%%%%% Bottom Banner Ad Position %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            else if (name == "BottomLeft")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.BottomLeft);
            }
            else if (name == "BottomCenter")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.Bottom);
            }
            else if (name == "BottomRight")
            {
                LoadBannerAd(AdSize.Banner, AdPosition.BottomRight);
            }
            // %%%%%%%%%%%%%%%%%%%%%%%%%%% SmartBanner Ad Position %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            else if (name == "Default")
            {
                LoadBannerAd(AdSize.SmartBanner, AdPosition.Top);
            }

            else if (name == "BottomSmartBanner")
            {
                LoadBannerAd(AdSize.SmartBanner, AdPosition.Bottom);
            }


        }
    }

    public void HideBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }


    #endregion
    // %%%%%%%%%%%%%%%%%%%%%%%%%%% MediumRectangle Ad %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region MediumRectangle ADS
    public void ShowMediumRectangle() 
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1) && isInitialized)
        {
            if (AdsPosition.BottomLeft == NativeAdsPosition)
                ShowMediumBanner(AdSize.MediumRectangle, AdPosition.BottomLeft);

            else if (AdsPosition.BottomRight == NativeAdsPosition)
                ShowMediumBanner(AdSize.MediumRectangle, AdPosition.BottomRight);

            else if (AdsPosition.TopRight == NativeAdsPosition)
                ShowMediumBanner(AdSize.MediumRectangle, AdPosition.TopRight);

            else if (AdsPosition.TopLeft == NativeAdsPosition)
                ShowMediumBanner(AdSize.MediumRectangle, AdPosition.TopLeft);
        }
        
    }
  
    public void HideMediumRectangleAd()
    {

        if (this.bannerViewMediumRectangle != null)
        {

            this.bannerViewMediumRectangle.Hide();
        }
    }

     void LoadMediumRectangle()
    {
        
        if (AdsPosition.BottomLeft == NativeAdsPosition)
            LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.BottomLeft);

        else if (AdsPosition.BottomRight == NativeAdsPosition)
            LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.BottomRight);

        else if (AdsPosition.TopRight == NativeAdsPosition)
            LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.TopRight);

        else if (AdsPosition.TopLeft == NativeAdsPosition)
            LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.TopLeft);

    }

    void LoadMediumRectangleBanner(AdSize adSize, AdPosition adPos)
    {
        string adUnitId;
        if (isTestAds)
         adUnitId = TestMediumRectangleID;
         else
       adUnitId = MediumRectangleID;
      

        this.bannerViewMediumRectangle = new BannerView(adUnitId, adSize, adPos);
        BindMediumBannerEvents();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerViewMediumRectangle.LoadAd(request);
        this.bannerViewMediumRectangle.Hide();
    }
    void ShowMediumBanner(AdSize _size, AdPosition _pos)
    {
       if (this.bannerViewMediumRectangle != null)
        {
            this.bannerViewMediumRectangle.Show();

        }
      else
        {
            LoadMediumRectangle();
        }

    }

 
    private void BindMediumBannerEvents()
    {
        // Called when an ad request has successfully loaded.
        this.bannerViewMediumRectangle.OnAdLoaded += this.HandleOnMediumBannerAdLoaded;
        // Called when an ad request failed to load.
        this.bannerViewMediumRectangle.OnAdFailedToLoad += this.HandleOnMediumBannerAdFailedToLoad;

    }
    public void HandleOnMediumBannerAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("MediumBanner:Loaded");
    }

    public void HandleOnMediumBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("MediumBannerLoadError: "+ args);
    }
    #endregion
    // %%%%%%%%%%%%%%%%%%%%%%%%%%% Interstitial Ad %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region INTERSTITIAL ADS

    private void RequestAndLoadInterstitialAd()
    { 
        string adUnitId;

        if (isTestAds)
        {
            adUnitId = TestInterstitalID;
        }
        else
        {
            adUnitId = InterstitalID;
        }

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }


        interstitialAd = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
     //   this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
     //   this.interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Load an interstitial ad
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);
    }


    void LoadInterstitialAds()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitialAd.LoadAd(request);
        }
       
    }

    public void ShowInterstitialAd()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1) && isInitialized)
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
                LoadInterstitialAds();
                return;
            }
          else if (Advertisement.IsReady())
            {
                Advertisement.Show();
                LoadInterstitialAds();
                return;

            }
            else 
            {
               
                LoadInterstitialAds();
                Advertisement.Load(UnityAdID);

            }
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

 public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
 public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() => {


        });
    }

    // %%%%%%%%%%%%%%%%%%%%%%%%%%% Rewarded Interstitial Ad %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


    private void RequestAndLoadRewardedInterstitialAd()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && isInitialized)
        {
            string adUnitId;

            if (isTestAds)
            {
                adUnitId = TestRewardedInterstitialID;
            }
            else
            {
                adUnitId = RewardedInterstitialID;
            }




            // Create an interstitial.
            RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
            {

                if (error != null)
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        Debug.Log("RewardedInterstitialAd load failed, error: " + error);
                    });
                    return;
                }

                this.rewardedInterstitialAd = rewardedInterstitialAd;
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    Debug.Log("RewardedInterstitialAd loaded");
                });
                // Register for ad events.
                this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
                    {
                        MobileAdsEventExecutor.ExecuteInUpdate(() =>
                        {
                            Debug.Log("Rewarded Interstitial presented.");
                        });
                    };
                this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        Debug.Log("Rewarded Interstitial dismissed.");
                    });
                    this.rewardedInterstitialAd = null;
                };
                this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        Debug.Log("Rewarded Interstitial failed to present.");
                    });
                    this.rewardedInterstitialAd = null;
                };
            });
        }
    }

    public void ShowRewardedInterstitialAd(int Reward)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && isInitialized)
        {
            if (rewardedInterstitialAd != null)
            {
                rewardedInterstitialAd.Show((reward) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        Debug.LogError("User Rewarded: " + Reward);
                    });
                });
            }
            else
            {
                RequestAndLoadRewardedInterstitialAd();
            }
        }
    }
    public void ShowRewardedInterstitialAd(string _RewardType, int _Reward)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && isInitialized)
        {
            if (rewardedInterstitialAd != null)
            {
                rewardedInterstitialAd.Show((reward) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        if (_RewardType == "coins")
                        {
                            PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 1000);
                            Debug.Log("User Rewarded: " + _Reward);
                        }
                        else if (_RewardType == "monster")
                        {

                            if (levelmanager.instance)
                            {
                                levelmanager.instance.AnimalChanger();
                            }
                            Debug.Log("User Rewarded: " + _Reward);
                        }
                        else if (_RewardType == "toy")
                        {

                            if (levelmanager.instance)
                            {

                                levelmanager.instance.RobotoToy();
                            }
                            Debug.Log("User Rewarded: " + _Reward);
                        }
                    });
                });
            }
            else
            {
                RequestAndLoadRewardedInterstitialAd();
            }
        }
    }
    #endregion

    // %%%%%%%%%%%%%%%%%%%%%%%%%%% UNITY ADS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region UNITY ADS
    public void ShowInterstitialUnityAds()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1) && isInitialized)
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
                return;
            }
          else if  (interstitialAd.IsLoaded())
           {
            interstitialAd.Show();
            LoadInterstitialAds();
                return;
          }
        else
          {
                Advertisement.Load(UnityAdID);
                LoadInterstitialAds();
         }


    }
    public void ShowRewardedUnityAds()
    {
        if (Advertisement.IsReady(placementIdRewarded) && Application.internetReachability != NetworkReachability.NotReachable && isInitialized)
        {
            if (Advertisement.IsReady(placementIdRewarded))
            {
                var options = new ShowOptions { resultCallback = HandleSinglerewardsShowResult };
                Advertisement.Show(placementIdRewarded, options);
               
            }
        }
    }
    // --- Single Rewards Show Result ---
  private void HandleSinglerewardsShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
               
                    Debug.Log("The ad was successfully shown" + Reward);

                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end");
                break;

            case ShowResult.Failed:
                Debug.Log("The ad failed to be shown");
                break;
        }


    }

    public void ShowRewardedUnityAds(string _RewardType, int _Reward)
    {
        if (Advertisement.IsReady(placementIdRewarded) && Application.internetReachability != NetworkReachability.NotReachable && isInitialized)
        {
            if (Advertisement.IsReady(placementIdRewarded))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show(placementIdRewarded, options);
                RewardType = _RewardType;
                Reward = _Reward;
            }
        }
    }

    // --- UNITY ADS EVENTS ---
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (RewardType == "cash")
                    Debug.Log("The ad was successfully shown" + Reward);

                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end");
                break;

            case ShowResult.Failed:
                Debug.Log("The ad failed to be shown");
                break;
        }


    }
    #endregion

    // %%%%%%%%%%%%%%%%%%%%%%%%%%% Links %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    #region Links 
    public void RateUs()
    {
        Application.OpenURL(RateUsLink);
    }
    public void MoreGames()
    {
        Application.OpenURL(MoreGamesLink);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL(PrivacyLink);
    }
    public void ExitGame()
    {
       
        Application.Quit();
    }
    public void NextScene()
    {
        Application.LoadLevel(1);
     
    }
    #endregion
  
}