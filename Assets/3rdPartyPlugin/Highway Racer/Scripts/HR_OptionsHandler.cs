//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2021 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class HR_OptionsHandler : MonoBehaviour {
	public static HR_OptionsHandler ins;
	public GameObject pausedMenu;
	public GameObject pausedButtons;
	public GameObject optionsMenu;
	public GameObject optionsMenu_PP;

	void OnEnable(){

		HR_GamePlayHandler.OnPaused += OnPaused;
		HR_GamePlayHandler.OnResumed += OnResumed;
		ins = this;
	}

	public void ResumeGame () {
		
		HR_GamePlayHandler.Instance.Paused();
		
	}

	public void RestartGame () {

		HR_GamePlayHandler.Instance.RestartGame();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Restart_Mode_Highway_" + PlayerPrefs.GetInt("SelectedModeIndex") + "_Scene_" + PlayerPrefs.GetInt("HighwayScene"));

	}

	public void MainMenu () {
		
		HR_GamePlayHandler.Instance.MainMenu();
		AudioListener.volume = 1f;
		Firebase.Analytics.FirebaseAnalytics.LogEvent("Back_to_Home_From_Highway_Scene_" + PlayerPrefs.GetInt("HighwayScene") + "_Mode_" + PlayerPrefs.GetInt("SelectedModeIndex"));

	}

	public void OptionsMenu (bool open) {

		optionsMenu.SetActive (open);

		if (open)
			pausedButtons.SetActive (false);
		else
			pausedButtons.SetActive (true);

	}

	void OnPaused () {

		pausedMenu.SetActive(true);
		pausedButtons.SetActive(true);

		//AudioListener.pause = true;
		Time.timeScale = 0;
		Firebase.Analytics.FirebaseAnalytics.LogEvent("Pause_Highway_Scene_" + PlayerPrefs.GetInt("HighwayScene") + "_Mode_" + PlayerPrefs.GetInt("SelectedModeIndex") );


	}

	public void OnResumed () {

		pausedMenu.SetActive(false);
		pausedButtons.SetActive(false);

		//AudioListener.pause = false;
		Time.timeScale = 1;

	}

	public void ChangeCamera(){

		if (GameObject.FindObjectOfType<HR_CarCamera> ())
			GameObject.FindObjectOfType<HR_CarCamera> ().ChangeCamera ();

	}

	void OnDisable(){

		HR_GamePlayHandler.OnPaused -= OnPaused;
		HR_GamePlayHandler.OnResumed -= OnResumed;

	}

}
