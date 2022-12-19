using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyLoading : MonoBehaviour
{
    public Text loadingTxt;

    MediationHandler mediation;

    private void OnEnable()
    {
        mediation = FindObjectOfType<MediationHandler>();

        // Show  Admob Interstitial ads.............................
        if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1))
        {
            mediation.LoadInterstitial();
        }


        StartCoroutine(Loading());

    }

    IEnumerator Loading()
    {
        for (int i = 0; i < 12; i++)
        {
            loadingTxt.text = "AD";
            yield return new WaitForSecondsRealtime(0.25f);
            loadingTxt.text = "AD .";
            yield return new WaitForSecondsRealtime(0.25f);
            loadingTxt.text = "AD . .";
            yield return new WaitForSecondsRealtime(0.25f);
            loadingTxt.text = "AD . . .";
            yield return new WaitForSecondsRealtime(0.25f);
            i++;
        }

        // Show  Admob Interstitial ads.............................
        if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1))
        {
            mediation.ShowInterstitial();
        }

        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
