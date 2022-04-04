using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour
{
    public Text timeTM, timetakentext;
    public int[] timeToCompleteLevels;// = new int[]{60,210,240,300,360,180,210,240,300,360,350,300,20};
    public float timeToCompleteLevel, timetaken;
    public static bool starttimer = false;
    public GameObject timer;
    
    void Start()
    {
        timeToCompleteLevel = timeToCompleteLevels[PlayerPrefs.GetInt("selectedlevel") - 1];

    }



    void Update()
    {
        if (starttimer == true)
        {


            int Minutes = 0;
            int Seconds = 0;
            Minutes = (int)Math.Abs(timeToCompleteLevel / 60);
            Seconds = (int)timeToCompleteLevel % 60;

            if (timeToCompleteLevel > 0)
            {// && !isGamePaused)
                timeToCompleteLevel -= Time.deltaTime;

            }

            if (timeToCompleteLevel <= 0.0F)
            {// && !TimeController.isTimeOver)


                levelmanager.instance.failcall();
                starttimer = false;

            }

            if (Minutes > 0)
            {
                timeTM.text = "0" + Minutes.ToString() + ":";
                if (Seconds < 10)
                {

                    timeTM.text = timeTM.text + "0" + Seconds.ToString();

                }
                else
                {
                
                    timeTM.text = timeTM.text + Seconds.ToString();
                }

            }
            else
            {

                timeTM.text = null;
       
                if (!(timer.GetComponent<AudioSource>().enabled))
                {
                    timer.GetComponent<AudioSource>().enabled = true;
                }

                if (Seconds < 10)
                {

                    timeTM.text = timeTM.text + "<color=#ff0000>0" + Seconds.ToString() + "</color>";

                }
                else
                {
                    
                    timeTM.text = timeTM.text + "<color=#ff0000>" + Seconds.ToString() + "</color>";
                }
            }
            

           


            //extra feature to check time taken by player

            //timetaken = timeToCompleteLevels[PlayerPrefs.GetInt("selectedlevel") - 1] - timeToCompleteLevel;

            //int minutestaken = 0;
            //int secondstaken = 0;

            //minutestaken = (int)Math.Abs(timetaken / 60);
            //secondstaken = (int)timetaken % 60;

            //timetakentext.text = "0" + minutestaken.ToString() + ":";

            //if (secondstaken < 10)
            //{

            //    timetakentext.text = timetakentext.text + "0" + secondstaken.ToString();

            //}
            //else
            //{
            //        timetakentext.text = timetakentext.text + secondstaken.ToString();
            //}

        }
    }
}
