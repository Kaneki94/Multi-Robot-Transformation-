using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertriggerhandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
       // this.gameObject.transform.SetParent(null);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            //this.gameObject.transform.SetParent(other.gameObject.transform.root.transform);
            PlayersHandler._instance.switchplayer(other.gameObject.transform.root.gameObject);
           
        }
    }
}
