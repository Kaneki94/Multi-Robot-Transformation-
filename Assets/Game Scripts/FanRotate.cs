using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour {

	public float speed;
	public enum Axis
	{
		X,
		Y,
		Z,
	}
	public Axis RotateAxis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		switch (RotateAxis)
		{
		case Axis.Y:
			transform.Rotate (Vector3.up * speed);
			break;
		case Axis.Z:
			transform.Rotate (Vector3.forward * speed);
			break;
		default:
			transform.Rotate (Vector3.right * speed);
			break;
		}


	}
}
