using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objstopass : MonoBehaviour
{
    public GameObject manager;  

    void Awake()
    {
        manager = GameObject.FindWithTag("manager");
    }

}
