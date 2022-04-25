using System.Collections.Generic;
using UnityEngine;

public class PlayersHandler : MonoBehaviour
{
    public static PlayersHandler _instance;
    #region Players 
    public List<GameObject> Players;
    public RCC_MobileButtons Mobilebutton;
    #endregion
    public int currentplayerindex = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    private void Start()
    {

    }
    public void switchplayer(GameObject currentplayer)
    {
        //if (this.gameObject.transform.root.gameObject.transform.GetComponent<BlinkerEffect>())
        //{
        //    this.gameObject.transform.root.gameObject.transform.GetComponent<BlinkerEffect>().set_StatusBlinker();
        //}
        //else
        if (currentplayer)
        {
            currentplayer.SetActive(false);
            Players[currentplayerindex].transform.position = currentplayer.transform.position;
            Players[currentplayerindex].transform.rotation = currentplayer.transform.rotation;
            Players[currentplayerindex].SetActive(true);

            Players[currentplayerindex].GetComponent<Rigidbody>().velocity = currentplayer.GetComponent<Rigidbody>().velocity;
            Players[currentplayerindex].GetComponent<Rigidbody>().angularVelocity = currentplayer.GetComponent<Rigidbody>().angularVelocity;
            Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().speed = currentplayer.GetComponent<RCC_CarControllerV3>().speed;

            Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().canControl =/* true*/currentplayer.GetComponent<RCC_CarControllerV3>().canControl;
            Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engineRunning = /*true*/currentplayer.GetComponent<RCC_CarControllerV3>().engineRunning;
          // sounds value copy 
            //Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engineSoundHigh = currentplayer.GetComponent<RCC_CarControllerV3>().engineSoundHigh;
            //Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engineSoundHighOff = currentplayer.GetComponent<RCC_CarControllerV3>().engineSoundHighOff;
            //Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engineSoundLowOff = currentplayer.GetComponent<RCC_CarControllerV3>().engineSoundLowOff;
            //Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engineSoundMedOff = currentplayer.GetComponent<RCC_CarControllerV3>().engineSoundMedOff;
            //Players[currentplayerindex].GetComponent<RCC_CarControllerV3>().engin = currentplayer.GetComponent<RCC_CarControllerV3>().engineSoundMedOff;


            currentplayer.GetComponent<RCC_CarControllerV3>().canControl = false;
            currentplayer.GetComponent<RCC_CarControllerV3>().engineRunning = false;
            if (currentplayer.GetComponent<WeaponController>())
                Mobilebutton.weaponcontroller = Players[currentplayerindex].GetComponent<WeaponController>();

            currentplayerindex++;
            if (currentplayerindex >= Players.Count)
                currentplayerindex = 0;
        }
    }
}
