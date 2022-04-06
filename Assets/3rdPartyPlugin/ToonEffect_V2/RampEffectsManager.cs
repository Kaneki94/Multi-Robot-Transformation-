using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampEffectsManager : MonoBehaviour
{
    public GameObject[] power_particles;
    int current = 0;
  
     public void OnTriggerEnter(Collider other)
     {
        if(other.gameObject.tag== "ramp")
        {
           if (current == power_particles.Length)
           {
                current = 0;
           }
           for(int i=0;i<power_particles.Length;i++)
            {
                power_particles[i].SetActive(false);
            }    
            power_particles[current].SetActive(true);
            current++;
        }
     }
  
}
