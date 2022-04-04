using UnityEngine;
using System.Collections;

public class playhorn : MonoBehaviour {

    public AudioClip impact;
    public AudioSource horn1;

  
	
	
    public void playhorn1()
    {
        horn1.PlayOneShot(impact, 0.7F);

    }
}
