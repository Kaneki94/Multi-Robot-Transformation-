using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class camrotate : MonoBehaviour {

	public Transform Target;

	public float speed;

	
	// Update is called once per frame

	void Update () {

		//transform.LookAt(Target.transform);
		//transform.Rotate(Vector3.up, -speed * Time.deltaTime);
		transform.RotateAround(Target.position, Vector3.up, -speed * Time.deltaTime);
		//transform.Translate(Vector3.right * Time.deltaTime*speed);


	}
}
