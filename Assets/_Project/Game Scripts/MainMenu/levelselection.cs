using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class levelselection : MonoBehaviour
{
    public GameObject[] levels;
    public Sprite selectImage;
    public int selectedlevel;
    public AudioClip buttonSound;
    public GameObject lvlselection, seasonselection/*, mainmode*/;
    public GameObject[] modes, modelocks;
    int n;

    void OnEnable()
    {
        Time.timeScale = 1f;
       // mainmode.SetActive(true);
      //  PlayerPrefs.SetInt("unlockedlevels", 30);
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
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Mission_Mode_Chapter_1_Level_" + PlayerPrefs.GetInt("unlockedlevels"));
        if (PlayerPrefs.GetInt("unlockedlevels") > 15)
        {
            modes[1].GetComponent<Button>().interactable = true;
            modelocks[1].SetActive(false);
            modes[1].GetComponent<TweenScale>().enabled = true;
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Mission_Mode_Chapter_2_Level_" + (PlayerPrefs.GetInt("unlockedlevels")-15));

        }
        if (PlayerPrefs.GetInt("unlockedlevels") > 25)
        {
            modes[2].GetComponent<Button>().interactable = true;
            modelocks[2].SetActive(false);
            modes[2].GetComponent<TweenScale>().enabled = true;
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Mission_Mode_Chapter_3_Level_" + (PlayerPrefs.GetInt("unlockedlevels") - 25));
        }
        if (PlayerPrefs.GetInt("unlockedlevels") > 35)
        {
            modes[3].GetComponent<Button>().interactable = true;
            modelocks[3].SetActive(false);
            modes[3].GetComponent<TweenScale>().enabled = true;
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Mission_Mode_Chapter_4_Level_" + (PlayerPrefs.GetInt("unlockedlevels") - 35));
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
                levels[i].transform.GetChild(1).gameObject.SetActive(false);
            }
           else if (i == selectedlevel)
            {
                levels[i].GetComponent<Image>().sprite = selectImage;
            }
            else
            {
                levels[i].GetComponent<Button>().interactable = false;
                //levels[i].GetComponent<Image>().sprite = levelImg;
                levels[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            levels[i].gameObject.GetComponent<TweenScale>().enabled = false;
           // levels[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        levels[n].gameObject.GetComponent<TweenScale>().enabled = true;
       levels[n].GetComponent<Image>().sprite = selectImage;
        levels[n].transform.GetChild(1).gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("unlockedlevels") >= 15)
        {
            levels[14].gameObject.GetComponent<TweenScale>().enabled = true;
            levels[14].GetComponent<Image>().sprite = selectImage;
            levels[14].transform.GetChild(1).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("unlockedlevels") >= 25)
        {
            levels[24].gameObject.GetComponent<TweenScale>().enabled = true;
            levels[24].GetComponent<Image>().sprite = selectImage;
            levels[24].transform.GetChild(1).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("unlockedlevels") >= 35)
        {
            levels[34].gameObject.GetComponent<TweenScale>().enabled = true;
            levels[34].GetComponent<Image>().sprite = selectImage;
            levels[34].transform.GetChild(1).gameObject.SetActive(false);
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
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Selected_Mission_Mode_Level_" + PlayerPrefs.GetInt("selectedlevel"));

    }


}
