using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wings : MonoBehaviour
{
    public GameObject[] wing;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < wing.Length; i++)
        {
            wing[i].GetComponent<Animator>().enabled = true;
        }
        
    }

    void OnDisable()
    {
        for (int i = 0; i < wing.Length; i++)
        {
            wing[i].GetComponent<Animator>().enabled = false;
        }
    }
}
