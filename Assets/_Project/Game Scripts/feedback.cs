using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feedback : MonoBehaviour {

	public Image[] stars;
	public GameObject ok;

	// Use this for initialization
	void OnEnable() {
		
		for (int i = 0; i < stars.Length; i++) {

			stars [i].enabled = false;
		}
		ok.SetActive (false);
	}
		

	public void starfeedback(int starindex){

		for (int i = 0; i < stars.Length; i++) {

			stars [i].enabled = false;
		}
		for (int j = 0; j < starindex; j++) {

			stars [j].enabled = true;
		}

		if (starindex >= 4) {

			Debug.Log ("Review Given!!!");
           //mmm GaminatorAds.Instance.RateUs();
			this.gameObject.SetActive (false);
		} else {

			ok.SetActive (true);
		}
	}
}
