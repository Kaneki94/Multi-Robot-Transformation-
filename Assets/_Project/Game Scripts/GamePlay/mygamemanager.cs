using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mygamemanager : MonoBehaviour
{
    public GameObject mapcanvas , successpanel, failpanel, pausedpanel, missionpanel, gameplaycontrols, timeuppanel, loading;
    public AudioClip buttonSound;
    public bool soundchk = true;
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Success()
    {
        if (PlayerPrefs.GetInt("unlockedlevels") < 40 && PlayerPrefs.GetInt("selectedlevel") == PlayerPrefs.GetInt("unlockedlevels"))
        {
            PlayerPrefs.SetInt("unlockedlevels", PlayerPrefs.GetInt("unlockedlevels") + 1);
            //Debug.Log(" Selected Level is " + PlayerPrefs.GetInt("selectedlevel"));
            //Debug.Log(" Unlocked Levels is " + PlayerPrefs.GetInt("unlockedlevels"));
            //Debug.Log("Unlocked level!!!");
        }
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 1000);
        successpanel.SetActive(true);
        Invoke("delays", 4f);
    }

    public void Failed()
    {
        failpanel.SetActive(true);
        Invoke("delays", 4f);
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

        if (PlayerPrefs.GetInt("selectedlevel") < 40)
            PlayerPrefs.SetInt("selectedlevel", PlayerPrefs.GetInt("selectedlevel") + 1);

        loading.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
    }

    public void Restart()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        loading.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
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
    }

    public void Startmission()
    {
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
}
