using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newpick : MonoBehaviour
{
    public GameObject healthimg, coinimg, coins;
    public Slider healthbar;
    public Transform spawnpos;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            coinimg.SetActive(true);
            //coins.transform.position = other.gameObject.transform.position;
            //coins.transform.rotation = other.gameObject.transform.rotation;
            coins.SetActive(true);
          //  PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 100);
            levelmanager.instance.coinscount(100);
            other.gameObject.SetActive(false);
            Invoke("waitoff", 3f);
          
                levelmanager.instance.picked = true;
        }
        else if (other.gameObject.tag == "health")
        {
            healthimg.SetActive(true);
            if (healthbar.value <= 2700)
            {
                DamageManager.playerHP += 300;
                healthbar.value += 300;

            }

            other.gameObject.SetActive(false);
            Invoke("waitoff", 3f);
           
                levelmanager.instance.picked = true;
        }
        else if (other.gameObject.tag == "monster")
        {

            if (levelmanager.instance)
                levelmanager.instance.AnimalChanger();

            other.gameObject.SetActive(false);
           
                levelmanager.instance.picked = true;

        }

      
    }


    void waitoff()
    {

        coinimg.SetActive(false);
        healthimg.SetActive(false);
        coins.SetActive(false);
    }

    public void coinseffect() {

        coins.SetActive(true);
        Invoke("waitoff", 3f);
    } 
}
