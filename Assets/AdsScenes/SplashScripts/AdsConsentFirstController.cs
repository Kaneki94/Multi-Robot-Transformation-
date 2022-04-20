using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsConsentFirstController : MonoBehaviour
{


    public GameObject privacypolicy, UserSplash;
    public string PrivacyLink;
    public static int firstcount = 0;
    string imagePath1 = "";
    string imagePath2 = "";
    void Start()
    {

        if (PlayerPrefs.GetInt("ConsentAd", 0) == 0)
        {
            privacypolicy.SetActive(true);
            UserSplash.SetActive(false);
           
        }
        else
        {
            privacypolicy.SetActive(false);
            UserSplash.SetActive(true);
            StartCoroutine(WaitForMainMenu());
        }
        Time.timeScale = 1f;
    }
  
   
   

    

    

    public void Okbutton()
    {
        UserSplash.SetActive(true);
        privacypolicy.SetActive(false);
        PlayerPrefs.SetInt("ConsentAd", 1);
        StartCoroutine(WaitForMainMenu());
    }
    public void On_withdraw()
    {
        PlayerPrefs.SetInt("ConsentAd", 0);

    }

    public void On_consentokMainbutton()
    {

        PlayerPrefs.SetInt("ConsentAd", 1);

    }
    IEnumerator WaitForMainMenu()
    {
        //yield return new WaitForSeconds(3f);
        //if (AdsManager.Instance)
        //    AdsManager.Instance.ShowInterstitialAd();
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(1);

    }
    public void OnMoreButtonClicked()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6443581777790265108");

    }

    public void PrivacyOpen()
    {
        Application.OpenURL(PrivacyLink);
    }
}
