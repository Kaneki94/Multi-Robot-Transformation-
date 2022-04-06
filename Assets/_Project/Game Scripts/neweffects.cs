using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neweffects : MonoBehaviour
{

    public GameObject effect1;
    Animator robo;
    int totalbtns;
    // Start is called before the first frame update
    void Start()
    {
        effect1.SetActive(false);
        robo = GetComponent<Animator>();
        totalbtns = GetComponent<weapons>().firebuttons.Length;
    }


    public void Smash() {

        robo.Play("attack");
        for (int i = 0; i < totalbtns; i++)
        {
            GetComponent<weapons>().firebuttons[i].SetActive(false);

        }
       // effect1btn.SetActive(false);
    }


    public void GetBodyEffect() {

        effect1.SetActive(true);
        Invoke("waitoff", 2f);
    }

   
    void waitoff() {

       
        effect1.SetActive(false);
        for (int i = 0; i < totalbtns; i++)
        {
            GetComponent<weapons>().firebuttons[i].SetActive(true);

        }
        //effect1btn.SetActive(true);
    }
}
