﻿//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2021 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(RCC_CarControllerV3))]
public class HR_PlayerHandler : MonoBehaviour {

	private RCC_CarControllerV3 carController;
	private HR_RoadPooling roadPooling;
	private Rigidbody rigid;

	private bool gameOver = false;
	private bool gameStarted{get{return HR_GamePlayHandler.Instance.gameStarted;}}

	public float score;
	internal float timeLeft = 100f;
	internal int combo;
	internal int maxCombo;
	public GameObject PowerupParticle;
	public float speed = 0f;
	internal float distance = 0f;
	internal float highSpeedCurrent = 0f;
	internal float highSpeedTotal = 0f;
	internal float opposideDirectionCurrent = 0f;
	internal float opposideDirectionTotal = 0f;
 


    private int minimumSpeedForGainScore
	{
		get
		{
			return HR_HighwayRacerProperties.Instance._minimumSpeedForGainScore;
		}
	}
	private int minimumSpeedForHighSpeed
	{
		get
		{
			return HR_HighwayRacerProperties.Instance._minimumSpeedForHighSpeed;
		}
	}

	private Vector3 previousPosition;

	private string currentTrafficCarNameLeft;
	private string currentTrafficCarNameRight;

	internal int nearMisses;
	private float comboTime;

	internal bool bombTriggered = false;
	internal float bombHealth = 100f;

	private AudioSource hornSource;

	public delegate void onPlayerSpawned(HR_PlayerHandler player);
	public static event onPlayerSpawned OnPlayerSpawned;

	public delegate void onNearMiss(HR_PlayerHandler player, int score, HR_DynamicScoreDisplayer.Side side);
	public static event onNearMiss OnNearMiss;

	public delegate void onPlayerDied(HR_PlayerHandler player);
	public static event onPlayerDied OnPlayerDied;
	void Awake () {

		if(!GameObject.FindObjectOfType<HR_UIDashboardDisplay>()){

			enabled = false;
			return;

		}
		

		carController = GetComponentInParent<RCC_CarControllerV3>();
		roadPooling = GameObject.FindObjectOfType<HR_RoadPooling>();
		rigid = GetComponent<Rigidbody>();
		rigid.drag = 0f;
	
	}

	void OnEnable(){

		if(OnPlayerSpawned != null)
			OnPlayerSpawned (this);

        if (!carController.engineRunning)
            carController.StartEngine();

        hornSource = HR_CreateAudioSource.NewAudioSource(gameObject, "Horn", 10f, 100f, 1f, HR_HighwayRacerProperties.Instance.hornClip, true, false, false);
		//Invoke(nameof(set_StatusPlayerswitcher),10f);
	}

	void Update () {

		if(gameOver || !gameStarted)
			return;
		
		speed = carController.speed;
		distance += Vector3.Distance(previousPosition, transform.position) / 1000f;
		previousPosition = transform.position;

		if(speed >= minimumSpeedForGainScore){
			score += carController.speed * (Time.deltaTime * .05f);
		}

		if(speed >= minimumSpeedForHighSpeed){
			highSpeedCurrent += Time.deltaTime;
			highSpeedTotal += Time.deltaTime;
		}else{
			highSpeedCurrent = 0f;
		}

		if(speed >= (minimumSpeedForHighSpeed / 2f) && transform.position.x <= 0f && HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.TwoWay){
			opposideDirectionCurrent += Time.deltaTime;
			opposideDirectionTotal += Time.deltaTime;
		}else{
			opposideDirectionCurrent = 0f;
		}

		if(HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.TimeAttack){
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0){
				timeLeft = 0;
				OnGameOver(0.2f);
			}
		}
		if(this.gameObject.transform.rotation.y > 15)
        {
			this.gameObject.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, 15, this.transform.localRotation.z);
        }
		else if (this.gameObject.transform.rotation.y < -15)
		{
			this.gameObject.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, -15, this.transform.localRotation.z);
		}


		comboTime += Time.deltaTime;

		if(HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.Bomb){

			if(speed > 80f){
				if(!bombTriggered)
					bombTriggered = true;
				else
					bombHealth += Time.deltaTime * 5f;
			}else if(bombTriggered){
				bombHealth -= Time.deltaTime * 10f;
			}

			bombHealth = Mathf.Clamp(bombHealth, 0f, 100f);

			if(bombHealth <= 0f){
				GameObject explosion = (GameObject)Instantiate(HR_HighwayRacerProperties.Instance.explosionEffect, transform.position, transform.rotation);
				explosion.transform.SetParent(null);
				rigid.isKinematic = true;
				OnGameOver(2f);
			}

		}

		if(comboTime >= 2)
			combo = 0;

		CheckStatus ();

	}
	
	void FixedUpdate(){

		if (!gameOver && gameStarted) {
			CheckNearMiss ();
		}
			
	}
	void CheckNearMiss(){
		
		RaycastHit hit;

		Debug.DrawRay(carController.COM.position, (-transform.right * 2f), Color.white);
		Debug.DrawRay(carController.COM.position, (transform.right * 2f), Color.white);
		
		if(Physics.Raycast(carController.COM.position, (-transform.right), out hit, 2f, HR_HighwayRacerProperties.Instance.trafficCarsLayer) && !hit.collider.isTrigger){

			currentTrafficCarNameLeft = hit.transform.name;

		}else{
			
			if(currentTrafficCarNameLeft != null && speed > HR_HighwayRacerProperties.Instance._minimumSpeedForGainScore){
				
				nearMisses ++;
				combo ++;
				comboTime = 0;
				if(maxCombo <= combo)
					maxCombo = combo;

				score += 100f * Mathf.Clamp(combo / 1.5f, 1f, 20f);
				OnNearMiss (this, (int)(100f * Mathf.Clamp(combo / 1.5f, 1f, 20f)), HR_DynamicScoreDisplayer.Side.Left);
				
				currentTrafficCarNameLeft = null;
				
			}else{
				
				currentTrafficCarNameLeft = null;
				
			}
			
		}
		
		if(Physics.Raycast(carController.COM.position, (transform.right), out hit, 2f, HR_HighwayRacerProperties.Instance.trafficCarsLayer) && !hit.collider.isTrigger){
			currentTrafficCarNameRight = hit.transform.name;
		}else{
			
			if(currentTrafficCarNameRight != null && speed > HR_HighwayRacerProperties.Instance._minimumSpeedForGainScore){
				
				nearMisses ++;
				combo ++;
				comboTime = 0;
				if(maxCombo <= combo)
					maxCombo = combo;

				score += 100f * Mathf.Clamp(combo / 1.5f, 1f, 20f);
				OnNearMiss (this, (int)(100f * Mathf.Clamp(combo / 1.5f, 1f, 20f)), HR_DynamicScoreDisplayer.Side.Right);
				
				currentTrafficCarNameRight = null;
				
			}else{
				
				currentTrafficCarNameRight = null;
				
			}
			
		}

		if(Physics.Raycast(carController.COM.position, (transform.forward), out hit, 40f, HR_HighwayRacerProperties.Instance.trafficCarsLayer) && !hit.collider.isTrigger){

			Debug.DrawRay(carController.COM.position, (transform.forward * 20f), Color.red);

			if (carController.highBeamHeadLightsOn)
				hit.transform.SendMessage("ChangeLines");

		}

		if (hornSource) {

			if (carController.highBeamHeadLightsOn) {

				if (GetComponent<HR_ModApplier>().isSirenAttached)
					hornSource.clip = HR_HighwayRacerProperties.Instance.sirenAudioClip;

				if (!hornSource.isPlaying)
					hornSource.Play();

			} else {

				hornSource.Stop();

			}

		}

	}

	void OnCollisionEnter(Collision col){

		Vector3 colRelVel = col.relativeVelocity;
		colRelVel *= 1f - (Mathf.Abs (Vector3.Dot (transform.up, col.contacts [col.contacts.Length - 1].normal)) + Mathf.Abs (Vector3.Dot (transform.up, col.contacts [0].normal)));

		float cos = Mathf.Abs (Vector3.Dot (col.contacts [col.contacts.Length - 1].normal, colRelVel.normalized));
		float cos2 = Mathf.Abs (Vector3.Dot (col.contacts [0].normal, colRelVel.normalized));


		//if (col.transform.root.gameObject.GetComponent<Rigidbody>() /*&& this.gameObject.GetComponent<RCC_CarControllerV3>().speed>= 80f*/)
		//{
		//	col.transform.root.gameObject.GetComponent<HR_TrafficCar>().enabled = false;
		//	col.transform.root.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		//	col.transform.root.gameObject.GetComponent<Rigidbody>().mass = 10f;
		//	col.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500*rigid.velocity.magnitude,ForceMode.Acceleration);
		//	col.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 500 * rigid.velocity.magnitude, ForceMode.Acceleration);

		//	print("Name :"+ col.transform.root.gameObject.name);
		//}

		if (colRelVel.magnitude * cos < HR_HighwayRacerProperties.Instance._minimumCollisionForGameOver && colRelVel.magnitude * cos2 < HR_HighwayRacerProperties.Instance._minimumCollisionForGameOver)
			return;

		combo = 0;

		if(col.relativeVelocity.magnitude < HR_HighwayRacerProperties.Instance._minimumCollisionForGameOver || (1 << col.gameObject.layer) != HR_HighwayRacerProperties.Instance.trafficCarsLayer.value)
			return;

		if(HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.Bomb){
			bombHealth -= col.relativeVelocity.magnitude / 2f;
			return;
		}

		//rigid.isKinematic = true;
	   //OnGameOver(1f);

	}
	void OnTriggerEnter(Collider col)
	{

		
		if (col.transform.root.gameObject.GetComponent<Rigidbody>() && col.CompareTag("TrafficCar")) /*&& this.gameObject.GetComponent<RCC_CarControllerV3>().speed>= 80f*/
		{
			col.transform.root.gameObject.GetComponent<HR_TrafficCar>().enabled = false;
			col.transform.root.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			col.transform.root.gameObject.GetComponent<Rigidbody>().mass = 10f;
			col.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100 * rigid.velocity.magnitude, ForceMode.Acceleration);
			col.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100 * rigid.velocity.magnitude, ForceMode.Acceleration);
			col.transform.root.gameObject.GetComponent<AudioSource>().Play();

			print("Name :" + col.transform.root.gameObject.name);
		}
		//if (col.tag == "CompleteTrigger")
		//{
		//	rigid.isKinematic = true;
		//	OnGameOver(1f);
		//}
		else if(col.tag == "health")
        {
			if(PowerupParticle)
            {
				PowerupParticle.SetActive(true);
				Invoke("PowerupOff", 4f);
			}
			
        }
		else if(col.tag == "Hurdle")
        {
			if(PowerupParticle!=null && !PowerupParticle.activeInHierarchy)
            {
				this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				OnGameOver(0.2f);
			}
			
		}

	}
	void PowerupOff()
    {
		if(PowerupParticle)
			PowerupParticle.SetActive(false);
    }
	void CheckStatus(){

		if(!roadPooling || rigid.isKinematic)
			return;

		if(HR_GamePlayHandler.Instance && !gameStarted)
			return;

		if(speed < 5f || Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f){

			transform.position = new Vector3(0f, 2f, transform.position.z + 10f);
			transform.rotation = Quaternion.identity;
			rigid.angularVelocity = Vector3.zero;
			rigid.velocity = new Vector3(0f, 0f, 20f);

		}

	}

	void OnGameOver(float delayTime){

		OnPlayerDied (this);
		gameOver = true;
		carController.canControl = false;
		carController.engineRunning = false;
		PlayerPrefs.SetInt("PlayerCarOne", 0);
	}
	public void OnGameSave(float delayTime)
	{

		this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		gameOver = false;
		carController.canControl = true;
		carController.engineRunning = true;
		PlayerPrefs.SetInt("PlayerCarOne", 0);
	}
	//  private void set_StatusPlayerswitcher()
	//   {
	//	PlayerswitchTrigger.SetActive(true);
	//	CancelInvoke(nameof(set_StatusPlayerswitcher));
	////	Invoke(nameof(set_StatusPlayerswitcher), 10f);
	//}
}
