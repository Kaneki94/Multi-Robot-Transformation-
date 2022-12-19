using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missions : MonoBehaviour {

	public int level = 0;
	int n;
	public Text missionbottom;
	public string[] bottomstatements;
    public AudioClip[] bottomstatementsaudio;
   // public AudioClip typingsound;
//	public string[] missionstatements = {"Kill all enemies and safe the city.",
//		"Load vehicle into the lifter.",
//		"Take lifter truck to other destination.",
//		"Unload vehicle from lifter.",
//		"Unload vehicle from lifter.",
//		"Take the helicopter to another city.",
//		"Unload vehicle and load into the truck.",
//		"Unload vehicle and load into the truck.",
//		"Drive truck to the destination.",
//		"Unload vehicle from the truck."
//	};
		
	// Use this for initialization
	void Start () {

		level = PlayerPrefs.GetInt ("selectedlevel");
	
		//levelnumber.text = "MISSION " + level.ToString();
		n = PlayerPrefs.GetInt ("selectedlevel")-1;
	
		//StartCoroutine (missionbottomread());
	}


    public void skipstartstatement() {
        StartCoroutine(missionbottomread());
    }
	
	IEnumerator missionbottomread()
	{
		missionbottom.text = null;
		yield return new WaitForSeconds (0.5f);
		char[] s = bottomstatements [n].ToCharArray();

        //GameObject.Find ("TypingSound").GetComponent<AudioSource> ().Play ();


        
            foreach (char c in s)
            {
			missionbottom.GetComponent<AudioSource>().enabled = true;
			yield return new WaitForSeconds(0.08f);


            if (this.gameObject.GetComponent<mygamemanager>().soundchk == true && !GetComponent<AudioSource>().isPlaying)
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(bottomstatementsaudio[n]);


            //if (this.gameObject.GetComponent<mygamemanager>().soundchk == true)
            //    this.gameObject.GetComponent<AudioSource>().PlayOneShot(typingsound);

            missionbottom.text += c;


            }

		//GameObject.Find ("TypingSound").GetComponent<AudioSource> ().Stop ();
		missionbottom.GetComponent<AudioSource>().enabled = false;


	}
}
