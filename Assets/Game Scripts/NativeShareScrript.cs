using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NativeShareScrript : MonoBehaviour {


	private bool isProcessing = false;
	public string AppLinkURL { get; set; }
	private string shareText = "Counter Terrorist Shooting";
	private string gameLink = "Download the game on play store at \n " + " https://play.google.com/store/apps/details?id=com.abc";

    // htps://play.google.com/store/apps/details?id=com.stormcode.real.car.parking.free.apps";
    public void shareImage()
	{
     //mmm   CustomAnalytics.logLevelStarted("ShareApp", "");
        if (!isProcessing)
			StartCoroutine(ShareScreenshot());
	}

	private IEnumerator ShareScreenshot()
	{
		isProcessing = true;
		yield return new WaitForEndOfFrame();

		string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		Debug.Log(destination);

		if (!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText + gameLink);
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intentObject);

			isProcessing = false;
		}
	}  
}
