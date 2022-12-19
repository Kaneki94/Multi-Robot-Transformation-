using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycounter : MonoBehaviour
{
    public static enemycounter instance;
    public int counter,extracounter;
    public GameObject[] enemies,extras;
    void Start()
    {
        instance = this;
        //for(int i =0; i<enemies.Length;i++)
        //{
        //    enemies[i].SetActive(true);
        //}
        if (enemies[counter])
            enemies[counter].SetActive(true);

        if (extras.Length!=0 && extras[extracounter]!=null)
            extras[extracounter].SetActive(true);
    }

    public void deadcounter() {


        if (counter < enemies.Length-1)
        {
          
            counter++;
            enemies[counter].SetActive(true);
        }
        
    }

    public void deadcounterextra()
    {


        if (extracounter < extras.Length-1)
        {

            extracounter++;
            extras[extracounter].SetActive(true);
        }

    }

}
