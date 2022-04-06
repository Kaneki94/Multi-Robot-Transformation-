/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;
using CnControls;
//[RequireComponent(typeof(FlightSystem))]

public class PlayerController : MonoBehaviour {
	
	FlightSystem flight;// Core plane system
	FlightView View;
	
	public bool Active = true;
	public bool SimpleControl;// make it easy to control Plane will turning easier.
	public bool Acceleration;// Mobile*** enabled gyroscope controller
	public float AccelerationSensitivity = 5;// Mobile*** gyroscope sensitivity
//mm	private TouchScreenVal controllerTouch;// Mobile*** move
//mm	private TouchScreenVal fireTouch;// Mobile*** fire
//mm	private TouchScreenVal switchTouch;// Mobile*** swich
//mm	private TouchScreenVal sliceTouch;// Mobile*** slice
	private bool directVelBack;
	public GUISkin skin;
	public bool ShowHowto;
	bool fire1 = false;
	bool boost = false;
	bool brake = false;
	int boostspeed = 0;
	void Start () {
		flight = this.GetComponent<FlightSystem>();
		View = (FlightView)GameObject.FindObjectOfType(typeof(FlightView));
		// setting all Touch screen controller in the position
//mm		controllerTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, Screen.height - 100));
//mm		fireTouch = new TouchScreenVal (new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height));
//mm		switchTouch = new TouchScreenVal (new Rect (0, Screen.height - 100, Screen.width / 2, 100));
		
//mm		sliceTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, 50));
		
		if(flight)
		directVelBack = flight.DirectVelocity;
	}
	
	void Update () {
		if(!flight || !Active)
			return;
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		// On Desktop
		DesktopController();
		#else
		// On Mobile device
		MobileController();
		#endif

		if (fire1 == true) {
			flight.WeaponControl.LaunchWeapon();
		}

		if (boost == true) {
			//Debug.Log ("boostspeed" + boostspeed);
			flight.SpeedUp(boostspeed);
			boostspeed++;
		}
		if (brake == true) {

			//this.gameObject.transform.Translate (transform.position.x, transform.position.y, transform.position.z - 0.00001f);
		//	Debug.Log ("boostspeed" + boostspeed);
			flight.SpeedUp(boostspeed);
			boostspeed--;
		}
		
	}
	
	
	void DesktopController(){
		// Desktop controller
		flight.SimpleControl = SimpleControl;
		
		// lock mouse position to the center.
//mm		Screen.lockCursor = true;
		
//mm		flight.AxisControl(new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y") ));
		flight.AxisControl(new Vector2(0,-CnInputManager.GetAxis("Vertical") ));
		if(SimpleControl){
			flight.DirectVelocity = false;
//mm			flight.TurnControl(Input.GetAxis("Mouse X"));
			flight.TurnControl(CnInputManager.GetAxis("Horizontal"));
		}else{
			flight.DirectVelocity = directVelBack;	
		}

//mm		flight.TurnControl(Input.GetAxis ("Horizontal"));
//mm		flight.SpeedUp(Input.GetAxis ("Vertical"));
		flight.TurnControl(CnInputManager.GetAxis ("Horizontal"));
//mm		flight.SpeedUp(CnInputManager.GetAxis("Vertical"));
		
		if(CnInputManager.GetButtonDown("Speedup")){
			boost = true;
			}
		if(CnInputManager.GetButtonUp("Speedup")){
			boost = false;
			boostspeed = 0;
		}
		if(CnInputManager.GetButtonDown("Speeddown")){
			brake = true;
		}
		if(CnInputManager.GetButtonUp("Speeddown")){
			brake = false;
			boostspeed = 0;
		}
//mm		if(Input.GetButton("Fire1")){
		if(CnInputManager.GetButtonDown("Fire1")){
			fire1 = true;
//mm            flight.WeaponControl.LaunchWeapon();
        }
		if(CnInputManager.GetButtonUp("Fire1")){
			fire1 = false;
			//mm            flight.WeaponControl.LaunchWeapon();
		}
		
//mm		if(Input.GetButtonDown("Fire2")){
		if(CnInputManager.GetButtonDown("Fire2")){
            flight.WeaponControl.SwitchWeapon();
        }
		
//       	if (Input.GetKeyDown (KeyCode.C)) {
		if (CnInputManager.GetButtonDown("Camswitch")) {
			if(View)
				View.SwitchCameras ();	
		}	
	}
	
		void MobileController(){
		// Desktop controller
		flight.SimpleControl = SimpleControl;

		// lock mouse position to the center.
		//mm		Screen.lockCursor = true;

		//mm		flight.AxisControl(new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y") ));
		flight.AxisControl(new Vector2(0,-CnInputManager.GetAxis("Vertical") ));
		if(SimpleControl){
			flight.DirectVelocity = false;
			//mm			flight.TurnControl(Input.GetAxis("Mouse X"));
			flight.TurnControl(CnInputManager.GetAxis("Horizontal"));
		}else{
			flight.DirectVelocity = directVelBack;	
		}

		//mm		flight.TurnControl(Input.GetAxis ("Horizontal"));
		//mm		flight.SpeedUp(Input.GetAxis ("Vertical"));
		flight.TurnControl(CnInputManager.GetAxis ("Horizontal"));
		//mm		flight.SpeedUp(CnInputManager.GetAxis("Vertical"));

		if(CnInputManager.GetButtonDown("Speedup")){
			boost = true;
		}
		if(CnInputManager.GetButtonUp("Speedup")){
			boost = false;
			boostspeed = 0;
		}
		if(CnInputManager.GetButtonDown("Speeddown")){
			brake = true;
		}
		if(CnInputManager.GetButtonUp("Speeddown")){
			brake = false;
			boostspeed = 0;
		}
		//mm		if(Input.GetButton("Fire1")){
		if(CnInputManager.GetButtonDown("Fire1")){
			fire1 = true;
			//mm            flight.WeaponControl.LaunchWeapon();
		}
		if(CnInputManager.GetButtonUp("Fire1")){
			fire1 = false;
			//mm            flight.WeaponControl.LaunchWeapon();
		}

		//mm		if(Input.GetButtonDown("Fire2")){
		if(CnInputManager.GetButtonDown("Fire2")){
			flight.WeaponControl.SwitchWeapon();
		}

		//       	if (Input.GetKeyDown (KeyCode.C)) {
		if (CnInputManager.GetButtonDown("Camswitch")) {
			if(View)
				View.SwitchCameras ();	
		}	












































//		// Mobile controller
//		
//		flight.SimpleControl = SimpleControl;
//		
//		if (Acceleration) {
//			// get axis control from device acceleration
//			Vector3 acceleration = Input.acceleration;
//			Vector2 accValActive = new Vector2 (acceleration.x, (acceleration.y + 0.3f) * 0.5f) * AccelerationSensitivity;
//			flight.FixedX = false;
//			flight.FixedY = false;
//			flight.FixedZ = true;
//			
//			flight.AxisControl (accValActive);
//			flight.TurnControl (accValActive.x);
//		} else {
//			flight.FixedX = true;
//			flight.FixedY = false;
//			flight.FixedZ = true;
//			// get axis control from touch screen
////mm			Vector2 dir = controllerTouch.OnDragDirection (true);
//			dir = Vector2.ClampMagnitude(dir,1.0f);
//			flight.AxisControl (new Vector2 (dir.x,-dir.y) * AccelerationSensitivity * 0.7f);
//			flight.TurnControl (dir.x * AccelerationSensitivity * 0.3f);
//		}
////mm		sliceTouch.OnDragDirection(true);
//		// slice speed
////mm		flight.SpeedUp(sliceTouch.slideVal.x);
//		
////mm		if (fireTouch.OnTouchPress ()) {
//			flight.WeaponControl.LaunchWeapon ();
//		}	
	}
	
	
	// you can remove this part..
	void OnGUI ()
	{
		if(!ShowHowto)
			return;
		
		if(skin)
			GUI.skin = skin;
		
		if(GUI.Button(new Rect(20,150,200,40),"Gyroscope "+Acceleration)){
			Acceleration = !Acceleration;
		}
		
		if(GUI.Button(new Rect(20,200,200,40),"Change View")){
			if(View)
				View.SwitchCameras ();	
		}
		
		if(GUI.Button(new Rect(20,250,200,40),"Change Weapons")){
			if(flight)
				flight.WeaponControl.SwitchWeapon ();
		}
		
		if(GUI.Button(new Rect(20,300,200,40),"Simple Control "+SimpleControl)){
			if(flight)
				SimpleControl = !SimpleControl;
		}
	}

}
