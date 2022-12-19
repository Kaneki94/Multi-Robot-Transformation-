using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoType : MonoBehaviour
{
    public float letterPause = 0.02f;
    public AudioClip sound;
    public string message;
    public GameObject okBt;
   
    // Use this for initialization
    void OnEnable()
    {
        if(okBt)
        {
            okBt.SetActive(false);
        }
        
        message = GetComponent<Text>().text;
        this.GetComponent<Text>().text = "";
        //GetComponent<AudioSource>().Play();

        StartCoroutine(TypeText());
        //if (sound)
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            GetComponent<Text>().text += letter;
            if (sound)
                GetComponent<AudioSource>().PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        if(okBt)
        {
            okBt.SetActive(true);
        }
        
    }
}
