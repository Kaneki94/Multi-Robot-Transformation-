using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popuppanel : MonoBehaviour
{
    public GameObject inapppanel, cross;
    public GameObject titlesound;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("AllInApp") != 1)
        {
            inapppanel.SetActive(true);
            if (titlesound)
                titlesound.GetComponent<AudioSource>().enabled = false;
            Invoke("crosson", 3f);
        }
        
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
