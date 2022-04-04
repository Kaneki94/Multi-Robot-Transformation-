using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Unity_InterstitialAd : MonoBehaviour
{
    private void OnEnable()
    {
        // Show  Unity Interstitial ads.............................
        if (AdsManager.Instance)
            AdsManager.Instance.ShowInterstitialUnityAds();
    }
}
