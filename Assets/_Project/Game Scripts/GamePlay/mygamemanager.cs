using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mygamemanager : MonoBehaviour
{
    MediationHandler mediation;
    public GameObject mapcanvas , successpanel, failpanel, pausedpanel, missionpanel, gameplaycontrols, timeuppanel, loading, interstitialPanel;
    public AudioClip buttonSound;
    public bool soundchk = true;
    void Start()
    {
        Time.timeScale = 1f;
        mediation = FindObjectOfType<MediationHandler>();
    }

    public void Success()
    {
        ShowInterstialAd();
        if (PlayerPrefs.GetInt("unlockedlevels") < 45 && PlayerPrefs.GetInt("selectedlevel") == PlayerPrefs.GetInt("unlockedlevels"))
        {
            PlayerPrefs.SetInt("unlockedlevels", PlayerPrefs.GetInt("unlockedlevels") + 1);
            //Debug.Log(" Selected Level is " + PlayerPrefs.GetInt("selectedlevel"));
            //Debug.Log(" Unlocked Levels is " + PlayerPrefs.GetInt("unlockedlevels"));
            //Debug.Log("Unlocked level!!!");
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Success_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));

        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 1000);
        successpanel.SetActive(true);
        Debug.Log("Success_Mission_Mode_Before_Next_Level_" + PlayerPrefs.GetInt("selectedlevel"));
        Invoke("delays", 4f);
    }

    public void Failed()
    {
        ShowInterstialAd();
        failpanel.SetActive(true);
        Invoke("delays", 4f);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Fail_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));
    }

    void delays()
    {
        Time.timeScale = 0f;
    }

    public void Timeup()
    {
        Time.timeScale = 0f;
        timeuppanel.SetActive(true);
    }

    public void Paused()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        Time.timeScale = 0f;

        pausedpanel.SetActive(true);
        if(levelmanager.instance.currentplayer)
        {
            if(levelmanager.instance.currentplayer.GetComponent<WeaponController>())
            {
                foreach(var x in levelmanager.instance.currentplayer.GetComponent<WeaponController>().WeaponLists)
                {
                    x.GetComponent<WeaponLauncher>().Seeker = false;
                    x.GetComponent<WeaponLauncher>().ShowCrosshair = false;
                }
            }
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Pause_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));

    }

    public void Mission()
    {
        Time.timeScale = 0f;
        missionpanel.SetActive(true);
    }


    public void Next()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        MainMenu.gameplayback = true;
        SceneManager.LoadScene("mainmenu");
    }

    public void Next1()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        MainMenu.gameplayback = true;

        if (PlayerPrefs.GetInt("selectedlevel") < 45)
            PlayerPrefs.SetInt("selectedlevel", PlayerPrefs.GetInt("selectedlevel") + 1);

        loading.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Success_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));

    }

    public void Restart()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        loading.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Restart_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));

    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("gameplay");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {

                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void Home()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        MainMenu.gameplayback = false;
        StartCoroutine(LoadYourAsyncScene1());
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Back_To_Home_From_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));
    }

    IEnumerator LoadYourAsyncScene1()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("mainmenu");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {

                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void Resume()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        pausedpanel.SetActive(false);
        Time.timeScale = 1f;
        if (levelmanager.instance.currentplayer)
        {
            if (levelmanager.instance.currentplayer.GetComponent<WeaponController>())
            {
                foreach (var x in levelmanager.instance.currentplayer.GetComponent<WeaponController>().WeaponLists)
                {
                    x.GetComponent<WeaponLauncher>().Seeker = true;
                    x.GetComponent<WeaponLauncher>().ShowCrosshair = true;
                }
            }
        }
    }

    public void Startmission()
    {
        levelmanager.instance.currentlevelz = Instantiate(levelmanager.instance.levels[levelmanager.instance.selectedlevel - 1].gameObject, transform.position, transform.rotation);

        soundchk = false;
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        Time.timeScale = 1f;
        TimeController.starttimer = true;
        GetComponent<levelmanager>().currentlevelz.SetActive(true);

        GetComponent<levelmanager>().colormenustart();

        if (levelmanager.instance)
            levelmanager.instance.powerupchk();

        missionpanel.SetActive(false);
        gameplaycontrols.SetActive(true);

    }

    public void statusgameplaycanvas(int val)
    {
        mapcanvas.GetComponent<CanvasGroup>().alpha = val;
    }
    public void ShowInterstialAd()
    {
        if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1))
        {
            interstitialPanel.SetActive(true);
        }
    }
}
