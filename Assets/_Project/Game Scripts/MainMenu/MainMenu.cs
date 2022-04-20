using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool gameplayback = false;
    Camera maincamera;
    [Header("UI Panels")]
    public GameObject settingp;
    public GameObject quitp, mainmenu, gunselection,modeselection,chapterselection ,levelselection/*, selectionbar*//*, back*/, exit, loadingpanel;
    [Header("SoundFX")]
    public GameObject MainMenuSFX;
    public AudioClip buttonSound;

    [Header("Cash Counter")]
    public int totalcash;
    public Text cashtext;

    [Header("Players Animations")]
    public GameObject[] Animals;
    public GameObject dino;
    public Animator animaltrans;
    int counter = 0;

    [Header("LInks")]
    public string rateUsLink;
    public string moreAppsLink;
    public string privacyLink;

    private AsyncOperation async;



    void Start()
    {
        Time.timeScale = 1f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        maincamera = this.gameObject.GetComponent<Camera>();

        if (PlayerPrefs.GetInt("Cash") < 0)
        {
            PlayerPrefs.SetInt("Cash", 0);
        }

        if (gameplayback == false)
        {
            mainmenu.SetActive(true);
            //back.SetActive(false);
           // exit.SetActive(true);
        }
        else if (gameplayback == true)
        {
            levelselection.SetActive(true);
            //back.SetActive(true);
           // exit.SetActive(false);
        }

        //Invoke("forwardanim", 3f);

        //............Screen Resolution......................
        if (SystemInfo.systemMemorySize < 2048 || SystemInfo.graphicsMemorySize < 512)
        {
            Screen.SetResolution(800, 480, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, true);
        }
    }

    void Update()
    {
        totalcash = PlayerPrefs.GetInt("Cash");
        cashtext.text = totalcash.ToString();
    }

    public void UpdateCash()
    {
        totalcash = PlayerPrefs.GetInt("Cash");
        cashtext.text = totalcash.ToString();
    }


    public void Play()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        mainmenu.SetActive(false);
        gunselection.SetActive(true);
        //back.SetActive(true);
        exit.SetActive(false);
        if (dino)
        {
            dino.SetActive(false);
        }
    }

    public void OnView()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }

    public void Settings()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        settingp.SetActive(true);

    }

    public void Quit()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        if (dino)
        {
            dino.SetActive(false);
        }
        quitp.SetActive(true);

    }

    public void Yes()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        Application.Quit();
    }

    public void forwardanim()
    {
        Animals[counter].SetActive(true);
        animaltrans.Play("forward");
        Invoke("reverseanim", 5f);
    }

    public void reverseanim()
    {
        animaltrans.Play("reverse");
        Invoke("delayrev", 3f);
    }

    void delayrev()
    {
        Animals[counter].SetActive(false);
        if (counter < Animals.Length - 1)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }
        Invoke("forwardanim", 3f);
    }

    public void No()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        quitp.SetActive(false);
        if (dino)
        {
            dino.SetActive(true);
        }
    }

    public void Back()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        if (gunselection.activeInHierarchy)
        {
            gunselection.SetActive(false);

            maincamera.enabled = true;
           // back.SetActive(false);
            exit.SetActive(true);
            mainmenu.SetActive(true);
            if (dino)
            {
                dino.SetActive(true);
            }
        }
        else if (levelselection.activeInHierarchy)
        {
            gunselection.SetActive(true);

            levelselection.SetActive(false);

            maincamera.enabled = true;
            //back.SetActive(true);
        }
        else if (modeselection.activeInHierarchy)
        {
            gunselection.SetActive(true);

            modeselection.SetActive(false);

            maincamera.enabled = true;
           //wi back.SetActive(true);
        }
    }


    public void Next()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        gunselection.SetActive(false);

        maincamera.enabled = true;

        modeselection.SetActive(true);
    }

    public void startmission()
    {
        levelselection.SetActive(false);

        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        loadingpanel.SetActive(true);
        StartCoroutine("LoadYourAsyncScene", 3f);
    }

    public void bonusarea()
    {
        levelselection.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        loadingpanel.SetActive(true);
        //SceneManager.LoadScene("LifeGame");
        StartCoroutine("LoadLifeScene", 3f);
    }


    public void SelectScene(int levelIndex)
    {

        async = SceneManager.LoadSceneAsync(levelIndex+3);

    }

    public void SelectMode(int _modeIndex)
    {

        PlayerPrefs.SetInt("SelectedModeIndex", _modeIndex);
        

    }

    IEnumerator LoadLifeScene()
    {
         async = SceneManager.LoadSceneAsync("LifeGame");

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {

                async.allowSceneActivation = true;
            }

            yield return null;
        }
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

    void gameplay()
    {
        SceneManager.LoadScene("gameplay");
    }

    //....................................Links..........................................

    public void Rateus()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            Application.OpenURL(rateUsLink);
        }
    }

    public void MoreApps()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            Application.OpenURL(moreAppsLink);
        }
    }

    public void Privacy()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            Application.OpenURL(privacyLink);
        }

    }

}

