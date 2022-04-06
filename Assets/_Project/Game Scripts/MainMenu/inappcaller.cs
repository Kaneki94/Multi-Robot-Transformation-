using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inappcaller : MonoBehaviour
{

    public void Buyall()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            InApp_Manager.instance.Buy_UnlockAll_All();
        }
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
        if (AdsManager.Instance)
            AdsManager.Instance.ShowRewardedInterstitialAd(rewardtype, 1000);
    }

}
