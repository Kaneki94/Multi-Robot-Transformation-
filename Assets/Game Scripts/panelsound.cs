using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelsound : MonoBehaviour
{
    public GameObject gamesound;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(gamesound)
        gamesound.SetActive(false);

        if (GetComponent<AudioSource>())
        GetComponent<AudioSource>().enabled = true;
        
    }


}
