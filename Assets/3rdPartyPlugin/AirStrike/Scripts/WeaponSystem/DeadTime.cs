using UnityEngine;
using System.Collections;

public class DeadTime : MonoBehaviour {

	public float LifeTime = 3;
	void Start () {
        Invoke("wait",LifeTime);
      
	}

    void wait() {
        CancelInvoke(nameof(wait));
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, LifeTime);
    }
	
	
}
