using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
	public Text killedtext,healthtext,Reloadingtext,ammotext;
	public int totalenemies;
	public Texture2D weaponicon;
	public GameObject weaponiconpos;
	public GUISkin skin;
	public Texture2D Logo;
	public int Mode;
	private GameManager game;
	private PlayerController play;
	private WeaponController weapon;
	public GameObject[] weaponicons;
	public AudioClip buttonsound;
	int i=1;
			
	void Start ()
	{
		game = (GameManager)GameObject.FindObjectOfType (typeof(GameManager));
		play = (PlayerController)GameObject.FindObjectOfType (typeof(PlayerController));
		weapon = play.GetComponent<WeaponController> ();
		//mmmmmm totalenemies= ObjectiveHandler.instance.Enemies;
		// define player
		Debug.Log(weapon.CurrentWeapon);

	}

	public void OnGUI ()
	{
		
		if (skin)
			GUI.skin = skin;
		
		
		switch (Mode) {
		case 0:
			if (Input.GetKeyDown (KeyCode.P)) {
//mm				Mode = 2;
	//mmmmmm			GameManager1.instance.OnTapPause();
			}
			
			if (play) {
				
				play.Active = true;
			
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUI.skin.label.fontSize = 30;
				//GUI.Label (new Rect (20, 20, 200, 50), "Kills " + game.Killed.ToString ());
			//	killedtext.text = game.Killed.ToString();
				if (game.Killed<=totalenemies){
				killedtext.text =  game.Killed.ToString() + "/" + totalenemies;
				}
//mm				GUI.Label (new Rect (20, 60, 200, 50), "Score " + game.Score.ToString ());
				
				GUI.skin.label.alignment = TextAnchor.UpperRight;
//mm				GUI.Label (new Rect (Screen.width - 220, 20, 200, 50), "ARMOR " + play.GetComponent<DamageManager> ().HP);
				//healthtext.text = play.GetComponent<DamageManager> ().HP.ToString();
				GUI.skin.label.fontSize = 16;
				
				// Draw Weapon system
				//if (weapon != null && weapon.WeaponLists.Length > 0 && weapon.WeaponLists.Length < weapon.CurrentWeapon && weapon.WeaponLists [weapon.CurrentWeapon] != null) {
				if (i == weapon.CurrentWeapon){
					if (i != 0) {
						weaponicons [i - 1].SetActive (false);
						Reloadingtext.enabled = false;
					}
					if (i == 0) {
						weaponicons [3].SetActive (false);
						Reloadingtext.enabled = false;

					}

					weaponicons [i].SetActive (true);
					GetComponent<AudioSource> ().PlayOneShot (buttonsound);
					i++;
					if (i >= 4){
						i = 0;

					}
				}
					
//mm					GUI.DrawTexture (new Rect (Screen.width - 188, Screen.height - 138, 80, 80), weapon.WeaponLists [weapon.CurrentWeapon].Icon);
					//GUI.DrawTexture (new Rect (Screen.width - 100, Screen.height - 100, 80, 80), weapon.WeaponLists [weapon.CurrentWeapon].Icon);
	//				weaponicon = weapon.WeaponLists [weapon.CurrentWeapon].Icon;
//				weaponiconpos.GetComponent<GUITexture>().texture



					GUI.skin.label.alignment = TextAnchor.UpperRight;
					if (weapon.WeaponLists [weapon.CurrentWeapon].Ammo <= 0 && weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess > 0) {
					if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo) {
						Reloadingtext.enabled = true;
						Reloadingtext.text = "Reloading " + Mathf.Floor ((1 - weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess) * 100) + "%";
						if (Mathf.Floor ((1 - weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess) * 100) >= 97) {
							Reloadingtext.enabled = false;
						}
					} 
					else {
						Reloadingtext.enabled = false;
					
					}
	//mm						GUI.Label (new Rect (Screen.width - 140, Screen.height - 85, 120, 30), "Reloading " + Mathf.Floor ((1 - weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess) * 100) + "%");
					} else {
					if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo) {
						ammotext.enabled = true;
						Reloadingtext.enabled = false;
						ammotext.text = weapon.WeaponLists [weapon.CurrentWeapon].Ammo.ToString ();
					} else {
						ammotext.enabled = false;
					}
	//mm						GUI.Label (new Rect (Screen.width - 230, Screen.height - 120, 200, 30), weapon.WeaponLists [weapon.CurrentWeapon].Ammo.ToString ());
					}
				//}else{
					//weapon = play.GetComponent<WeaponController> ();
				//}
				
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
//mm				GUI.Label (new Rect (20, Screen.height - 50, 250, 30), "R Mouse : Switch Guns C : Change Camera");
			
			}else{
				play = (PlayerController)GameObject.FindObjectOfType (typeof(PlayerController));
				weapon = play.GetComponent<WeaponController> ();
			}
			break;
		case 1:

			Invoke ("death",2f);
//			if (play)
//				play.Active = false;
//			
//			Screen.lockCursor = false;
//			
//			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
//			GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Game Over");
//		
//			GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
//		
//			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Restart")) {
//				Application.LoadLevel (Application.loadedLevelName);
//			
//			}
//			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
//				Application.LoadLevel ("Mainmenu");
//			}
			break;
		
		case 2:
			if (play)
				play.Active = false;
			
			//Screen.lockCursor = false;
			Time.timeScale = 0;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Pause");
		
			GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
		
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Resume")) {
				Mode = 0;
				Time.timeScale = 1;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
				Time.timeScale = 1;
				Mode = 0;
			//	Application.LoadLevel ("Mainmenu");
                    SceneManager.LoadScene("Mainmenu");
                    
			}
			break;
			
		}
		
	}

	void death(){
	
	//mmmmmm	GameDialogs.instance.Dia_Failed ();
	}
}
