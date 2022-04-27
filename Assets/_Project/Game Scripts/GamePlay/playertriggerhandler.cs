using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertriggerhandler : MonoBehaviour
{
    public GameObject[] TransformPlayer;


    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach (GameObject g in TransformPlayer)
            g.SetActive(false);
        TransformPlayer[PlayersHandler.currentplayerindex].SetActive(true);
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
