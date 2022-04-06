using System.Collections;
using UnityEngine;
using CnControls;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController1 : MonoBehaviour {

	[Header(" Control Settings ")]
	public Rigidbody thisRigidbody;
	public SimpleJoystick joystick;
	public float moveSpeed;
	public float RotSpeed;
	public float maxX;
	public float maxZ;
	bool move;
	public static bool canMove;
	public bool GmRun,die,chwya,win;
	

	//[Header(" Rotation Control ")]	


	// Use this for initialization
	void Start () {
		
		// Store some values
		Application.targetFrameRate = 60;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		//joystick.
		if(GmRun && !die && !win)
        {
			if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
			{
				if(FindObjectOfType<enemCtr>().animcor)
                {
					die = true;
					StartCoroutine(dieplayer());
                }
				// Move Player
				move = true;
				GetComponent<Animator>().Play("run");
				transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
				GetComponent<Animator>().speed = 1;
			}
			else
			{
				move = false;
				GetComponent<Animator>().speed = 0;
			}
		}

		if(die && !chwya)
        {
			if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
			{
				// Move Player
				move = true;
				GetComponent<Animator>().Play("run");
				transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
				GetComponent<Animator>().speed = 1;
			}
			else
			{
				move = false;
				GetComponent<Animator>().speed = 0;
			}
		}

		if(FindObjectOfType<UiManager>().t<=0 && !win && !die)
        {
			die = true;
			StartCoroutine(dieplayer());
		}
		
	}

	private void FixedUpdate() {
		if(GmRun && !die && !win)
        {
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
			pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
			transform.position = pos;

			if (canMove)
			{
				if (move)
					Move();
				else
					thisRigidbody.velocity = Vector3.zero;
			}
		}


		if (die && !chwya)
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
			pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
			transform.position = pos;

			if (canMove)
			{
				if (move)
					Move();
				else
					thisRigidbody.velocity = Vector3.zero;
			}
		}
	}


	public void Move()
	{
		Vector3 movement = new Vector3(joystick.HorizintalAxis.Value, 0, joystick.VerticalAxis.Value);
		movement *= moveSpeed * Time.deltaTime;

		thisRigidbody.velocity = movement;
	}

	IEnumerator dieplayer()
    {

		yield return new WaitForSeconds(0.5f);
		chwya = true;
		GetComponent<BoxCollider>().isTrigger = true;
		int t = Random.Range(1, 3);
		SoundManager.instance.Play("fire" + t.ToString());
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play("hit2");
		GameObject gm = Instantiate(FindObjectOfType<UiManager>().bloodeefect, transform);
		gm.transform.localPosition = new Vector3(0, 1.3f, 0);
		int bb = Random.Range(1, 5);
		GetComponent<Animator>().Play("die" + bb.ToString());
		GetComponent<Animator>().speed = 1;
		yield return new WaitForSeconds(5f);
		//Advertisements.Instance.ShowInterstitial();
		FindObjectOfType<UiManager>().losepanel.SetActive(true);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="win")
        {
			win = true;
            Debug.Log("winnnnn1");
            StartCoroutine(winplayer());
        }
    }


	IEnumerator winplayer()
    {
        Debug.Log("winnnnn2");
		transform.eulerAngles = new Vector3(0, 180, 0);
		FindObjectOfType<UiManager>().wineffet.SetActive(true);
		GetComponent<Animator>().Play("win");
		GetComponent<Animator>().speed = 1;
		SoundManager.instance.Play("win");
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 5000);
        yield return new WaitForSeconds(7f);
		
		FindObjectOfType<UiManager>().winpanel.SetActive(true);

    }
}