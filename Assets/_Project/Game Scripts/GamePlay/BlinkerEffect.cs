using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerEffect : MonoBehaviour
{
    public List<GameObject> Bodymesheslist;
    public int no_ofBlinkingeffect = 20;
    private int blinkertime = 0;
    // Start is called before the first frame update
    void Start()
    {
      //  set_StatusBlinker();
    }
    public void set_StatusBlinker()
    {
        StartCoroutine(Blinkingeffect());
    }
    private IEnumerator Blinkingeffect()
    {
        while (blinkertime <= no_ofBlinkingeffect)
        {
            foreach (GameObject g in Bodymesheslist)
                g.SetActive(false);
            yield return new WaitForSeconds(0.05f);
            foreach (GameObject g in Bodymesheslist)
                g.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            blinkertime++;
        }
    }
}
