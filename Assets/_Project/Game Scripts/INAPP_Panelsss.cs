using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INAPP_Panelsss : MonoBehaviour
{
    MediationHandler mediation;
    public enum BannerPosition
    {
        TopSmartBanner, BottomSmartBanner,
        TopCenter, TopRight, TopLeft,
        BottomCenter, BottomRight, BottomLeft,
        HideBanner

    };
    [Space]
    [Space]
    [Header("Set Banner Position")]
    public BannerPosition _BannerPosition = BannerPosition.TopSmartBanner;
    [Space]
    [Space]
    [Header(" Hide Banner Ads ")]
    public bool HideAds;


    private void OnEnable()
    {

        if (AdmobAdsManager.Instance && HideAds)
            AdmobAdsManager.Instance.hideSmallBanner();
    }
    private void OnDisable()
    {

        // Show Banner ads.............................
        //if (AdmobAdsManager.Instance)
        //    AdmobAdsManager.Instance.ShowBannerAds(_BannerPosition.ToString());
        if (AdmobAdsManager.Instance && HideAds)
            AdmobAdsManager.Instance.ShowSmallBanner(GoogleMobileAds.Api.AdPosition.TopRight);
    }
}
