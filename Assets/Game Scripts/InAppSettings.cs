using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppSettings : MonoBehaviour
{
    public static InAppSettings instance;

    public GameObject[] adsInApp, LevelsInApp, playersInApp, AllInApp;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        SetupInApp();
    }

    public void SetupInApp()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            foreach (var r in adsInApp)
            {
                r.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("LevelsUnlock") == 1)
        {
            foreach (var lvl in LevelsInApp)
            {
                lvl.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("PlayersInApp") == 1)
        {
            foreach (var p in playersInApp)
            {
                p.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("AllInApp") == 1)
        {
            foreach (var lvl in LevelsInApp)
            {
                lvl.SetActive(false);
            }

            foreach (var r in adsInApp)
            {
                r.SetActive(false);
            }

            foreach (var all in AllInApp)
            {
                all.SetActive(false);
            }

            foreach (var p in playersInApp)
            {
                p.SetActive(false);
            }
        }
    }

}
