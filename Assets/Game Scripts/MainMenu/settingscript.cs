using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingscript : MonoBehaviour
{
    public Slider Musicslider, Soundslider;
    public AudioSource Sound;
    public AudioSource[] Music;


    void Start()
    {
        for (int i = 0; i < Music.Length; i++)
        {
            Music[i].volume = PlayerPrefs.GetFloat("music", 1f);
        }


        if (Musicslider != null)
        {
            Musicslider.value = Music[0].volume;

        }

        Sound.volume = PlayerPrefs.GetFloat("sound", 1f);

        if (Soundslider != null)
            Soundslider.value = Sound.volume;
    }

    public void MusicSelector()
    {
        if (Musicslider != null)
        {
            for (int i = 0; i < Music.Length; i++)
            {
                Music[i].volume = Musicslider.value;
            }
        }
        PlayerPrefs.SetFloat("music", Musicslider.value);
    }

    public void volumeSelector()
    {
        if (Soundslider != null)
            Sound.volume = Soundslider.value;
        PlayerPrefs.SetFloat("sound", Soundslider.value);
    }

}
