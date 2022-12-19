using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BugsnagUnity;

public class AdsConsentFirstController : MonoBehaviour
{


    public GameObject ConsentPanel, UserSplash;
    public string PrivacyLink;
    public static int firstcount = 0;
    string imagePath1 = "";
    string imagePath2 = "";
    void Start()
    {

        if (PlayerPrefs.GetInt("ConsentAd") == 0)
        {
            ConsentPanel.SetActive(true);
            UserSplash.SetActive(false);
        }
        else
        {
            ConsentPanel.SetActive(false);
            UserSplash.SetActive(true);
            StartCoroutine(WaitForMainMenu());
        }
        Time.timeScale = 1f;
    }

    public void MoreApps()
    {

        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            Application.OpenURL("https://play.google.com/store/apps/developer?id=MegaGamez");
        }

    }



    public void Okbutton()
    {
        UserSplash.SetActive(true);
        ConsentPanel.SetActive(false);
        PlayerPrefs.SetInt("ConsentAd", 1);
        StartCoroutine(WaitForMainMenu());


    }

    IEnumerator WaitForMainMenu()
    {

        yield return new WaitForSeconds(3f);
        //if (AdmobAdsManager.Instance)
        //    AdmobAdsManager.Instance.ShowInterstitialAd();
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(1);

    }


    public void PrivacyOpen()
    {
        Application.OpenURL(PrivacyLink);
    }
}
