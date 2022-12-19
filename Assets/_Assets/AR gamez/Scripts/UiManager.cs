
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    MediationHandler mediation;
    public GameObject red, green, starcam;
    public GameObject ingamepanel, interstitialPanel;

    public GameObject startpanel, gameplaypanel, losepanel, winpanel, maincam;
    public GameObject wineffet;
    public Text timershow;
    public float t = 65;
    public bool startcount;
    public GameObject bloodeefect;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        mediation = FindObjectOfType<MediationHandler>();
        if (PlayerPrefs.GetInt("SuicideModeChap") == 0)
        {
            t = 120;
        }
        else if (PlayerPrefs.GetInt("SuicideModeChap") == 1)
        {
            t = 95;
        }
        else
        {
            t = 80;
        }
        SoundManager.instance.Play("start");
        if (AdmobAdsManager.Instance)
            AdmobAdsManager.Instance.ShowSmallBanner(GoogleMobileAds.Api.AdPosition.TopRight);
    }

    private void Update()
    {
        if (startcount)
        {
            t -= Time.deltaTime;
            int a = (int)t;
            timershow.text = a.ToString();
            if(t<=0)
            {
                t = 0;
            }
        }
    }

    public void btn_retry()
    {
        SoundManager.instance.stop("enem");
        SoundManager.instance.stop("search");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Restart_Suicide_Mode_" );


    }


    public void btnstart()
    {
        SoundManager.instance.stop("start");
        SoundManager.instance.Play("click");
        FindObjectOfType<PlayerController1>().GmRun = true;
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    public void btnhome()
    {
        MainMenu.gameplayback = false;
        SceneManager.LoadScene("mainmenu");
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Back_to_home_Suicide_Mode_");

    }
    public void ShowInterstialAd()
    {
        if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1))
        {
            interstitialPanel.SetActive(true);
        }
    }
}
