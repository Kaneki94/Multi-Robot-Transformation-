using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunselection : MonoBehaviour
{
    public GameObject playerParent;

    public GameObject[] Guns;
    public int[] cost;
    public Text guncost;
    public GameObject selectedgun;
    public GameObject buybutton,selectbutton, nocash;
    public AudioClip buttonSound;
    int i;


    // Use this for initialization
    void Start()
    {
        i = 0;
        selectedgun = Guns[i].gameObject;
        if (cost[i] != 0)
        {
            guncost.text = "$" + cost[i].ToString();
        }
        else
        {
            guncost.text = "";
        }

        PlayerPrefs.SetInt("Gun", i);

        selectedgun.SetActive(true);

        //...............Edit By Umar....................................
        if (PlayerPrefs.GetInt("firsttime") < 0)
        {
            PlayerPrefs.SetInt("firsttime", 0);
        }

        if (PlayerPrefs.GetInt("firsttime") == 0)
        {
            for (int x = 0; x < cost.Length; x++)
            {
                if (PlayerPrefs.GetInt("ArmyModes") == 0)
                {
                    PlayerPrefs.SetInt("costgun" + x.ToString(), cost[x]);
                    Debug.Log(PlayerPrefs.GetInt("costgun" + x.ToString()));
                }
            }
            PlayerPrefs.SetInt("firsttime", 1);
        }
        //................................................................

        for (int y = 0; y < cost.Length; y++)
        {
            cost[y] = PlayerPrefs.GetInt("costgun" + y.ToString());
        }
        buybutton.SetActive(false);
        selectbutton.SetActive(true);
    }

    void OnEnable()
    {
        playerParent.SetActive(true);
        Time.timeScale = 1f;
        gunselected(0);
    }

    private void OnDisable()
    {
        if (playerParent)
        {
            playerParent.SetActive(false);
        }
    }

    public void left()
    {
        i--;
        if (i < 0)
            i = Guns.Length - 1;
        gunselected(i);

    }

    public void right()
    {
        i++;
        if (i >= Guns.Length)
            i = 0;
        gunselected(i);

    }

    public void gunselected(int index)
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);


        if (PlayerPrefs.GetInt("costgun" + index.ToString()) == 0)
        {
            PlayerPrefs.SetInt("Gun", index);
            guncost.text = "";
            buybutton.SetActive(false);
            selectbutton.SetActive(true);
        }
        else
        {
            guncost.text = "" + cost[index].ToString();
            buybutton.SetActive(true);
            selectbutton.SetActive(false);

        }
        i = index;
        selectedgun = Guns[index].gameObject;
        for (int j = 0; j < Guns.Length; j++)
        {

            Guns[j].SetActive(false);
        }

        selectedgun.SetActive(true);
    }

    public void buygun()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);

        if (PlayerPrefs.GetInt("Cash") >= PlayerPrefs.GetInt("costgun" + i.ToString()))
        {
            PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") - PlayerPrefs.GetInt("costgun" + i.ToString()));
            PlayerPrefs.SetInt("costgun" + i.ToString(), 0);
            gunselected(i);
        }
        else
        {
            nocash.SetActive(true);
            Invoke("DisableCashPanel", 1f);
            //Debug.Log("Don't have enough money!!!");
        }
    }

    public void DisableCashPanel()
    {
        nocash.SetActive(false);
    }

}
