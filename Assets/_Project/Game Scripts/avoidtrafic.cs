using UnityEngine;
using System.Collections;
namespace SWS
{
    public class avoidtrafic : MonoBehaviour
    {
        public GameObject horn;

        public GameObject car;
        public bool walk;
        // Use this for initialization


        //private void OnEnable()
        //{
        //    car = this.transform.parent.parent.gameObject;
        //}


        void Start()
        {
            walk = true;
        }

        
        public void stop1()
        {
            walk = false;
            car.GetComponent<splineMove>().Pause();

 
        }
        public void play1()
        {
           
            car.GetComponent<splineMove>().Resume();
           

        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag=="Player")
            {
                //horn = GameObject.Find("horn");
                horn.gameObject.GetComponent<playhorn>().playhorn1();
                stop1();

            }
        }


        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
               // horn = GameObject.Find("horn");
               if (walk == true)
                horn.gameObject.GetComponent<playhorn>().playhorn1();

                stop1();

            }
        }
        void OnTriggerExit(Collider other)
        {
           if(other.tag== "Player")
           {
               walk = true;
                Invoke("play1",1f);
           }

        }

    }
}
