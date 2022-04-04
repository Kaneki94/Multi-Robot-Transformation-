using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefManager : MonoBehaviour
{

    public static PlayerPrefManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static PlayerPrefManager Instance
    {

        get
        {

            if (_instance == null)
            {

                try
                {
                    _instance = GameObject.FindObjectOfType<PlayerPrefManager>();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    _instance = new PlayerPrefManager();
                }

            }

            return _instance;

        }

    }


    void Start()
    {
        Debug.Log("initialized PlayerPrefManager");

    }

    public void RemoveAds()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBannerAd();
            AdsManager.Instance.HideMediumRectangleAd();
        }
        PlayerPrefs.SetInt("RemoveAds", 1);

        //..........................................
        if(InAppSettings.instance)
        {
            InAppSettings.instance.SetupInApp();
        }
    }

    public void unlocklevels()
    {
        PlayerPrefs.SetInt("unlockedlevels", 40);

        //....................................................
        PlayerPrefs.SetInt("LevelsUnlock", 1);

        if (InAppSettings.instance)
        {
            InAppSettings.instance.SetupInApp();
        }
    }

    public void unlockplayers()
    {
        for (int i = 0; i <= 9; i++)
        {
            PlayerPrefs.SetInt("costgun" + i.ToString(), 0);
        }

        PlayerPrefs.SetInt("ArmyModes", 1);

        //....................................................
        PlayerPrefs.SetInt("PlayersInApp", 1);

        if (InAppSettings.instance)
        {
            InAppSettings.instance.SetupInApp();
        }

    }

    public void MegaPack()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBannerAd();
            AdsManager.Instance.HideMediumRectangleAd();
        }
        PlayerPrefs.SetInt("RemoveAds", 1);

        //............................................
        PlayerPrefs.SetInt("unlockedlevels", 40);
        PlayerPrefs.SetInt("LevelsUnlock", 1);

        for (int i = 0; i <= 9; i++)
        {
            PlayerPrefs.SetInt("costgun" + i.ToString(), 0);
        }

        PlayerPrefs.SetInt("ArmyModes", 1);
        PlayerPrefs.SetInt("PlayersInApp", 1);


        //.................................................
        PlayerPrefs.SetInt("AllInApp", 1);
        if (InAppSettings.instance)
        {
            InAppSettings.instance.SetupInApp();
        }
    }

    public bool IsAdsRemoved()
    {

        if (PlayerPrefs.GetInt("RemoveAds") == 0)
            return false;
        else
            return true;
    }



}
