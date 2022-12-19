using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffFade : MonoBehaviour
{
    public GameObject Fade;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(off());

    }

   IEnumerator off()
    {
        yield return new WaitForSeconds(6f);
        Fade.SetActive(false);
    }
}
