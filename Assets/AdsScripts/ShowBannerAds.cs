using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBannerAds : MonoBehaviour
{
   
    public enum BannerPosition
    {
        Default,TopLeft, TopCenter, TopRight, BottomCenter, BottomLeft,
        BottomRight, TopCenterLarge, TopRightLarge, TopLeftLarge,
        BottomSmartBanner, HideBanner

    };
    [Space]
    [Space]
    [Header("Set Banner Position")]
    public BannerPosition _BannerPosition = BannerPosition.Default;
    [Space]
    [Space]
    [Header(" Destroy Banner Ads whan player go to Next Scene")]
    public bool DestroyAds;

    private void OnEnable()
    {
        // Show Banner ads.............................
        if (AdsManager.Instance && _BannerPosition != BannerPosition.HideBanner)
            AdsManager.Instance.ShowBanner(_BannerPosition.ToString());
        //Hide Banner Ads...............................
        if (AdsManager.Instance && _BannerPosition == BannerPosition.HideBanner)
            AdsManager.Instance.HideBannerAd();

    }
    private void OnDisable()
    {
        if (AdsManager.Instance && DestroyAds)
            AdsManager.Instance.HideBannerAd();
       

    }


}


