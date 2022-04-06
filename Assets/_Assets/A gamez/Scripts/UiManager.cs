
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;


    public GameObject red, green;
    public GameObject ingamepanel;

    public GameObject startpanel, gameplaypanel, losepanel, winpanel;
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
        SoundManager.instance.Play("start");
    }

    private void Update()
    {
        if (startcount)
        {
            t -= Time.deltaTime;
            int a = (int)t;
            timershow.text = a.ToString();
        }
    }

    public void btn_retry()
    {
        SoundManager.instance.stop("enem");
        SoundManager.instance.stop("search");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }
}
