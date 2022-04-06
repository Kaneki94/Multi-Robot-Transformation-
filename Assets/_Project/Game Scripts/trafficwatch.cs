using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficwatch : MonoBehaviour
{
    public GameObject[] target;
    public GameObject selectedgun;


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target[0].transform.position) < 60f && target[0].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[0].transform.position.x, transform.position.y + 5f, target[0].transform.position.z);
            selectedgun.transform.LookAt(boundlook);

            //Debug.Log ("shoooot");
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[1].transform.position) < 60f && target[1].activeInHierarchy)
        {

            Vector3 boundlook = new Vector3(target[1].transform.position.x, transform.position.y + 3f, target[1].transform.position.z);
            selectedgun.transform.LookAt(boundlook);


            //Debug.Log ("shoooot");
            Invoke("Weapon", 1f);


        }

        else if (Vector3.Distance(transform.position, target[2].transform.position) < 60f && target[2].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[2].transform.position.x, transform.position.y + 3f, target[2].transform.position.z);
            selectedgun.transform.LookAt(boundlook);


            //    Debug.Log("shoooot");
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[3].transform.position) < 60f && target[3].activeInHierarchy)
        {

            Vector3 boundlook = new Vector3(target[3].transform.position.x, transform.position.y + 5f, target[3].transform.position.z);
            selectedgun.transform.LookAt(boundlook);


            //Debug.Log ("shoooot");
            Invoke("Weapon", 1f);


        }
        else if (Vector3.Distance(transform.position, target[4].transform.position) < 60f && target[4].activeInHierarchy)
        {
            Vector3 boundlook = new Vector3(target[4].transform.position.x, transform.position.y, target[4].transform.position.z);
            selectedgun.transform.LookAt(boundlook);

            //Debug.Log("shoooot");
            Invoke("Weapon", 1f);


        }

    }


    public void Weapon() {

        GetComponent<WeaponController>().LaunchWeapon();


    }
}
