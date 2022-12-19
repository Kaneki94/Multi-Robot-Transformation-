using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class objopener2 : MonoBehaviour
{
    public GameObject player;
    public GameObject[] cars, paths;
    float dist;

    private void Start()
    {
        if (SystemInfo.systemMemorySize > 1024)
        {
            Invoke("trafficchecker", 2f);
        }
    }


    void trafficchecker()
    {

        player = GetComponent<levelmanager>().currentplayer.gameObject;
        for (int i = 0; i < paths.Length; i++)
        {
            if (cars[i] != null)
            {
                dist = Vector3.Distance(player.transform.position, paths[i].transform.position);
                if (dist < 100 && cars[i].GetComponent<DamageManager>().trafficspawner == false)
                {
                    cars[i].SetActive(true);

                    cars[i].GetComponent<splineMove>().Resume();
                }
                else
                {
                    if (Vector3.Distance(player.transform.position, cars[i].transform.position) > 120)
                    {
                        cars[i].SetActive(false);
                        cars[i].GetComponent<splineMove>().Pause();
                    }
                }
            }
        }

        Invoke("trafficchecker", 5f);
    }





}
