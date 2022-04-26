using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class levelselection : MonoBehaviour
{
    public GameObject[] levels;
    //public Sprite/* levelImg,*/ selectImg;
    public int selectedlevel;
    public AudioClip buttonSound;
    public GameObject lvlselection, seasonselection/*, mainmode*/;
    public GameObject[] modes, modelocks;
    int n;

    void OnEnable()
    {
        Time.timeScale = 1f;
       // mainmode.SetActive(true);
        //PlayerPrefs.SetInt("unlockedlevels", 20);
        CheckLevels();
    }

    private void OnDisable()
    {
       // mainmode.SetActive(false);
        seasonselection.SetActive(false);
        lvlselection.SetActive(false);
    }

    public void CheckLevels()
    {
        if (PlayerPrefs.GetInt("unlockedlevels") < 1)
        {
            PlayerPrefs.SetInt("unlockedlevels", 1);
        }
        if (PlayerPrefs.GetInt("unlockedlevels") > levels.Length)
        {
            PlayerPrefs.SetInt("unlockedlevels", levels.Length);
        }

        modes[0].GetComponent<TweenScale>().enabled = true;

        if (PlayerPrefs.GetInt("unlockedlevels") > 10)
        {
            modes[1].GetComponent<Button>().interactable = true;
            modelocks[1].SetActive(false);
            modes[1].GetComponent<TweenScale>().enabled = true;
        }
        if (PlayerPrefs.GetInt("unlockedlevels") > 20)
        {
            modes[2].GetComponent<Button>().interactable = true;
            modelocks[2].SetActive(false);
            modes[2].GetComponent<TweenScale>().enabled = true;
        }
        if (PlayerPrefs.GetInt("unlockedlevels") > 30)
        {
            modes[3].GetComponent<Button>().interactable = true;
            modelocks[3].SetActive(false);
            modes[3].GetComponent<TweenScale>().enabled = true;
        }

        selectedlevel = PlayerPrefs.GetInt("unlockedlevels");

        n = selectedlevel - 1;

        afterseason();
        selected_level(PlayerPrefs.GetInt("unlockedlevels"));
    }

    public void modeselected()
    {
        seasonselection.SetActive(false);

        afterseason();
        selected_level(PlayerPrefs.GetInt("unlockedlevels"));
    }

    public void afterseason()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i < selectedlevel)
            {
                levels[i].GetComponent<Button>().interactable = true;
                //levels[i].GetComponent<Image>().sprite = levelImg;
                levels[i].transform.GetChild(0).gameObject.SetActive(false);
            }
           else if (i == selectedlevel)
            {
               // levels[i].GetComponent<Image>().sprite = selectImg;
            }
            else
            {
                levels[i].GetComponent<Button>().interactable = false;
                //levels[i].GetComponent<Image>().sprite = levelImg;
                levels[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            levels[i].gameObject.GetComponent<TweenScale>().enabled = false;
           // levels[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        levels[n].gameObject.GetComponent<TweenScale>().enabled = true;
     //  levels[n].GetComponent<Image>().sprite = selectImg;
        levels[n].transform.GetChild(0).gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("unlockedlevels") >= 10)
        {
            levels[9].gameObject.GetComponent<TweenScale>().enabled = true;
          //  levels[9].GetComponent<Image>().sprite = selectImg;
            levels[9].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("unlockedlevels") >= 20)
        {
            levels[19].gameObject.GetComponent<TweenScale>().enabled = true;
         //   levels[19].GetComponent<Image>().sprite = selectImg;
            levels[19].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("unlockedlevels") >= 30)
        {
            levels[29].gameObject.GetComponent<TweenScale>().enabled = true;
           // levels[29].GetComponent<Image>().sprite = selectImg;
            levels[29].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void selected_level(int lvl)
    {
        StopAllCoroutines();

        if (GetComponent<AudioSource>().enabled)
        {
            GetComponent<AudioSource>().PlayOneShot(buttonSound);
        }
        selectedlevel = lvl;

        PlayerPrefs.SetInt("selectedlevel", lvl);

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.GetComponent<TweenScale>().enabled = false;
        }

        levels[selectedlevel - 1].gameObject.GetComponent<TweenScale>().enabled = true;
        n = selectedlevel - 1;

    }


}
