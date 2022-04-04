using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelmove : MonoBehaviour
{
    public Animator player;

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {

            this.GetComponent<TweenRotation>().enabled = true;
        }
        else {

            this.GetComponent<TweenRotation>().enabled = false;
        }
    }
}
