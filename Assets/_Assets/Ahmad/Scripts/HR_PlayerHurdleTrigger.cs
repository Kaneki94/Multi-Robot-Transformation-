using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HR_PlayerHurdleTrigger : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        { 
            explosion.SetActive(true);
            Invoke("Explosionoff", 3f);
        }
    }
    public void Explosionoff()
    {
        explosion.SetActive(false);
    }
}
