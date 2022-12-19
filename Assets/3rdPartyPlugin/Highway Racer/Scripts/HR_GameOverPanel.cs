//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2021 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HR_GameOverPanel : MonoBehaviour {
	public static HR_GameOverPanel ins;
	public GameObject content,interstitialPanel;
	MediationHandler mediation;
	[Header("UI Texts On Scoreboard")]
	public Text totalScore;
	public Text subTotalMoney;
	public Text totalMoney;

	public Text totalDistance;
	public Text totalNearMiss;
	public Text totalOverspeed;
	public Text totalOppositeDirection;

	public Text totalDistanceMoney;
	public Text totalNearMissMoney;
	public Text totalOverspeedMoney;
	public Text totalOppositeDirectionMoney;
	public Text HighScore;
	public int sbs;
	public GameObject RespawnAd;
	public int totalDistanceMoneyMP
	{
		get
		{
			return HR_HighwayRacerProperties.Instance._totalDistanceMoneyMP;
		}
	}
	public int totalNearMissMoneyMP{
		get
		{
			return HR_HighwayRacerProperties.Instance._totalNearMissMoneyMP;
		}
	}
	public int totalOverspeedMoneyMP{
		get
		{
			return HR_HighwayRacerProperties.Instance._totalOverspeedMoneyMP;
		}
	}
	public int totalOppositeDirectionMP{
		get
		{
			return HR_HighwayRacerProperties.Instance._totalOppositeDirectionMP;
		}
	}

    private void Start()
    {
		mediation = FindObjectOfType<MediationHandler>();
    }
    void OnEnable(){

		HR_PlayerHandler.OnPlayerDied += HR_PlayerHandler_OnPlayerDied;
		Debug.Log("workingggggggggggggg00000000000");
	}

	void HR_PlayerHandler_OnPlayerDied (HR_PlayerHandler player){

		StartCoroutine (DisplayResults(player));
		
	}

	public IEnumerator DisplayResults(HR_PlayerHandler player){

		yield return new WaitForSecondsRealtime (1f);
		ShowInterstialAd();
		content.SetActive (true);

		totalScore.text = Mathf.Floor(player.score).ToString("F0");
		totalDistance.text = (player.distance).ToString("F2") + " KM";
		totalNearMiss.text = (player.nearMisses).ToString("F0");
		totalOverspeed.text = (player.highSpeedTotal).ToString("F1");
		totalOppositeDirection.text = (player.opposideDirectionTotal).ToString("F1");
		//	Debug.Log(totalDistanceMoney.text = Mathf.Floor(player.distance * totalDistanceMoneyMP).ToString("F0"));
		Debug.Log("totalDistanceMoneyMP " + totalDistanceMoneyMP);
		Debug.Log("player.distance " + player.distance);

		totalDistanceMoney.text = Mathf.Floor(player.distance * totalDistanceMoneyMP).ToString("F0");          // this one
		totalNearMissMoney.text = Mathf.Floor(player.nearMisses * totalNearMissMoneyMP).ToString("F0");
		totalOverspeedMoney.text = Mathf.Floor(player.highSpeedTotal * totalOverspeedMoneyMP).ToString("F0");
		totalOppositeDirectionMoney.text = Mathf.Floor(player.opposideDirectionTotal * totalOppositeDirectionMP).ToString("F0");
		totalMoney.text = (Mathf.Floor(player.distance * totalDistanceMoneyMP) + (player.nearMisses * totalNearMissMoneyMP) + Mathf.Floor(player.highSpeedTotal * totalOverspeedMoneyMP) + Mathf.Floor(player.opposideDirectionTotal * totalOppositeDirectionMP)).ToString("F0");
		sbs = (int)Mathf.Round(Mathf.Floor(player.distance * totalDistanceMoneyMP) + (player.nearMisses * totalNearMissMoneyMP) + Mathf.Floor(player.highSpeedTotal * totalOverspeedMoneyMP) + Mathf.Floor(player.opposideDirectionTotal * totalOppositeDirectionMP));
		PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + Mathf.FloorToInt(Mathf.Floor(player.distance * totalDistanceMoneyMP) + (player.nearMisses * totalNearMissMoneyMP) + Mathf.Floor(player.highSpeedTotal * totalOverspeedMoneyMP)));
		//PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + sbs);
		gameObject.BroadcastMessage("Animate");
		gameObject.BroadcastMessage("GetNumber");
		if(player.score > PlayerPrefs.GetInt("HighwayScore"))
        {
			PlayerPrefs.SetInt("HighwayScore", ((int)Mathf.Floor(player.score)));
		}
		else
        {
			PlayerPrefs.SetInt("HighwayScore", PlayerPrefs.GetInt("HighwayScore"));
		}
		HighScore.text = PlayerPrefs.GetInt("HighwayScore").ToString();
		if(mediation && Application.internetReachability != NetworkReachability.NotReachable)
        {
			RespawnAd.SetActive(true);
        }
		else
        {
			RespawnAd.SetActive(false);
		}
		Debug.Log("workingggggggggggggg");
		//HR_PlayerHandler.OnPlayerDied -= HR_PlayerHandler_OnPlayerDied;
		StopAllCoroutines();

	}

	void OnDisable(){

		HR_PlayerHandler.OnPlayerDied -= HR_PlayerHandler_OnPlayerDied;
		PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + sbs);
		Debug.Log("workingggggggggggggg222222222222");
	}
	public void ShowInterstialAd()
	{
		if (mediation != null && Application.internetReachability != NetworkReachability.NotReachable && (PlayerPrefs.GetInt("RemoveAds") != 1))
		{
			interstitialPanel.SetActive(true);
		}
	}
}
