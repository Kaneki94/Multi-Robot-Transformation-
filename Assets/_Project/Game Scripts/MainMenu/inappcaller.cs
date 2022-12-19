using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inappcaller : MonoBehaviour
{
    MediationHandler mediation;
    string _RewardType;


    private void Start()
    {
        mediation = FindObjectOfType<MediationHandler>();
    }

    public void Buyall()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            InApp_Manager.instance.Buy_UnlockAll_All();
        }
        Debug.Log("Buy All");
    }

    public void Unlockplayers()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            InApp_Manager.instance.Buy_UnlockAll_Players();

        }
    }

    public void Unlocklevels()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            InApp_Manager.instance.Buy_UnlockAll_Levels();
        }
    }

    public void removeads()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            InApp_Manager.instance.Buy_UnlockAll_Removeads();
        }
    }

    public void rewardedinterstitialcoins(string rewardtype)
    {
        //if (AdmobAdsManager.Instance)
        //    AdmobAdsManager.Instance.ShowRewardedInterstitialAd(rewardtype, 1000);

        if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable)
        {
            if(rewardtype!= null)
            {
                _RewardType = rewardtype;
                mediation.ShowRewardedVideo(GiveCoins);
            }

        }
    }

    void GiveCoins()
    {
        if (_RewardType == "coins")
        {
            if (MainMenu.instance)
            {
                MainMenu.instance.UpdateCash();
            }
        }
        else if (_RewardType == "player")
        {
            if (gunselection.ins)
            {
                gunselection.ins.freecar();
            }
        }
        else if (_RewardType == "monster")
        {

            if (levelmanager.instance)
            {
                levelmanager.instance.AnimalChanger();
            }
        }
        else if (_RewardType == "toy")
        {

            if (levelmanager.instance)
            {

                levelmanager.instance.RobotoToy();
            }
        }
        else if (_RewardType == "HighwayCar")
        {

            if (HR_GamePlayHandler.ins)
            {

                HR_GamePlayHandler.ins.SaveButton();
            }
        }
    }

}
