using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popuppanel1 : MonoBehaviour
{
    public GameObject loading, mainpanel;
    public GameObject inapppanel, cross;
    public GameObject titlesound;


    void OnEnable()
    {
        loading.SetActive(true);
        if (PlayerPrefs.GetInt("PlayersInApp") != 1 && inapppanel.name == "InApp_Players Panel")
        {
            mainpanel.SetActive(false);
            if (titlesound)
                titlesound.GetComponent<AudioSource>().enabled = false;
            Invoke("laodwaiting", 5f);
        }
        else
        if (PlayerPrefs.GetInt("LevelsUnlock") != 1 && inapppanel.name == "InApp_Levels Panel")
        {
            mainpanel.SetActive(false);
            if (titlesound)
                titlesound.GetComponent<AudioSource>().enabled = false;
            Invoke("laodwaiting", 5f);
        }
        else
        {
            Invoke("laoding", 5f);
        }
    }

    public void OnDisable()
    {
        mainpanel.SetActive(false);
    }

    public void laodwaiting()
    {
        loading.SetActive(false);
        mainpanel.SetActive(true);

        inapppanel.SetActive(true);
        Invoke("crosson", 3f);
    }

    public void laoding()
    {
        loading.SetActive(false);
        mainpanel.SetActive(true);
    }

    void crosson()
    {
        if (cross.GetComponent<Animator>() && cross.GetComponent<Animator>().enabled)
            cross.GetComponent<Animator>().Play("cross");
    }

    public void crossoff()
    {

        if (titlesound)
            titlesound.GetComponent<AudioSource>().enabled = true;

        cross.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

}
