using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Admob_InterstitialAd : MonoBehaviour
{
    [Space]
    [Space]
    [Header(" Destroy Interstitial Ads whan player go to Next Scene")]
    public bool DestroyAds;
    private void OnEnable()
    {
        // Show  Admob Interstitial ads.............................
        if (AdsManager.Instance)
            AdsManager.Instance.ShowInterstitialAd();
    }
    private void OnDisable()
    {
        if (AdsManager.Instance && DestroyAds)
        {
            AdsManager.Instance.DestroyInterstitialAd();
        }
       
    }
}
