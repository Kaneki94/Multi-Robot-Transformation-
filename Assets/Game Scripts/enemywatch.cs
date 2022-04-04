using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class enemywatch : MonoBehaviour
{
    public GameObject[] target;


    void Start()
    {
        target = GetComponent<objstopass>().manager.GetComponent<levelmanager>().players;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, target[0].transform.position) < 60f && target[0].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[0].transform.position.x, transform.position.y, target[0].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[1].transform.position) < 60f && target[1].activeInHierarchy)
        {

            Vector3 boundlook = new Vector3(target[1].transform.position.x, transform.position.y, target[1].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }

        else if (Vector3.Distance(transform.position, target[2].transform.position) < 60f && target[2].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[2].transform.position.x, transform.position.y, target[2].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[3].transform.position) < 60f && target[3].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[3].transform.position.x, transform.position.y, target[3].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[4].transform.position) < 60f && target[4].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[4].transform.position.x, transform.position.y, target[4].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[5].transform.position) < 60f && target[5].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[5].transform.position.x, transform.position.y, target[5].transform.position.z);
            this.gameObject.GetComponent<splineMove>().Pause();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("idle");
            transform.LookAt(boundlook);
            Invoke("Weapon", 1f);


        }
        else
        {

            this.gameObject.GetComponent<splineMove>().Resume();
            if (GetComponent<Animator>())
                this.gameObject.GetComponent<Animator>().Play("run");

        }

        //else if (Vector3.Distance(transform.position, target[3].transform.position) < 60f && target[3].activeInHierarchy)
        //{
        //    Vector3 boundlook = new Vector3(target[3].transform.position.x, transform.position.y, target[3].transform.position.z);
        //    transform.LookAt(boundlook);

        //    Debug.Log("shoooot");
        //    Invoke("Weapon", 1f);


        //}

    }


    public void Weapon()
    {

        GetComponent<WeaponController>().LaunchWeapon();


    }
}
