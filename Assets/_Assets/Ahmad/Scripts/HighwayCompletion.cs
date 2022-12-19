using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwayCompletion : MonoBehaviour
{
    public static HighwayCompletion ins;

    public GameObject CompleteTrigger, GameplayCanvas, Content;
    // Start is called before the first frame update
    void Start()
    {

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        Success();
    //    }
    //}
    public void Success()
    {
        GameplayCanvas.SetActive(false);
        HR_GamePlayHandler._instance.OnGameOver(2f);
        //PlayersHandler._instance.levelno++;
        Content.SetActive(true);
    }

}
