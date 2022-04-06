using UnityEngine;

public class EffectListener : MonoBehaviour
{
    public GameObject Booseffect;
    public GameObject nos_AirEffect;
    //public GameObject TyreTrail;
    public void set_statusAirEffect(bool val)
    {
        nos_AirEffect.SetActive(val);
        Booseffect.SetActive(val);
        //if (val)
        //{
        //    if (isgrounded)
        //        TyreTrail.SetActive(val);
        //}
        //else
        //    TyreTrail.SetActive(val);

    }

}
