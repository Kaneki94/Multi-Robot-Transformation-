using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public string[] TargetTag = new string[1]{"Enemy"};
	public WeaponLauncher[] WeaponLists;
	public int CurrentWeapon = 0;
	public bool ShowCrosshair, shootchk=false;
    int totalguns, totalbtns;
    public Animator[] guntorotate;
    public Animator mechwing;


    void Awake ()
	{
        
        // find all attached weapons.
        if (this.transform.GetComponentsInChildren (typeof(WeaponLauncher)).Length > 0) {
			var weas = this.transform.GetComponentsInChildren (typeof(WeaponLauncher));
			WeaponLists = new WeaponLauncher[weas.Length];
			for (int i=0; i<weas.Length; i++) {
				WeaponLists [i] = weas [i].GetComponent<WeaponLauncher> ();
				WeaponLists [i].TargetTag = TargetTag;
			}
		}
	}
	public WeaponLauncher GetCurrentWeapon(){
		if (CurrentWeapon < WeaponLists.Length && WeaponLists [CurrentWeapon] != null) {
			return WeaponLists [CurrentWeapon];
		}
		return null;
	}
	
	private void Start ()
	{
        if (GetComponent<weapons>())
        {
            totalguns = GetComponent<weapons>().guns.Length;
            totalbtns = GetComponent<weapons>().firebuttons.Length;
        }

        for (int i=0; i<WeaponLists.Length; i++) {
			if (WeaponLists [i] != null) {
				WeaponLists [i].TargetTag = TargetTag;
				WeaponLists [i].ShowCrosshair = ShowCrosshair;
			}
		}
        for (int i = 0; i < WeaponLists.Length; i++)
        {

            WeaponLists[i].gameObject.SetActive(false);
        }
    }

	private void Update ()
	{
		
		for (int i=0; i<WeaponLists.Length; i++) {
			if (WeaponLists [i] != null) {
				WeaponLists [i].OnActive = false;
			}
		}
		if (CurrentWeapon < WeaponLists.Length && WeaponLists [CurrentWeapon] != null) {
			WeaponLists [CurrentWeapon].OnActive = true;
		}

        if (shootchk)
        {
            if (CurrentWeapon < WeaponLists.Length && WeaponLists[CurrentWeapon] != null)
            {
                WeaponLists[CurrentWeapon].Shoot();
            }
        }

    }

    public void weaponch(int gun)
    {

        CurrentWeapon = gun;
    }

    public void LaunchWeapon(int index)
    {
        CurrentWeapon = index;
        if (CurrentWeapon < WeaponLists.Length && WeaponLists[index] != null)
        {
            WeaponLists[index].Shoot();
        }
    }

    public void LaunchWeapon()
    {
        WeaponLists[CurrentWeapon].gameObject.SetActive(true);

        if (CurrentWeapon < WeaponLists.Length && WeaponLists[CurrentWeapon] != null)
        {
            WeaponLists[CurrentWeapon].Shoot();
        }

        Invoke("offdelay", 0.5f);
    }

    public void SwitchWeapon ()
	{
		CurrentWeapon += 1;
		if (CurrentWeapon >= WeaponLists.Length) {
			CurrentWeapon = 0;	
		}
	}
	
	public void WeaponStart ()
	{
        //if (this.gameObject.tag == "Player")
        for (int i = 0; i < totalbtns; i++)
        {
            GetComponent<weapons>().firebuttons[i].SetActive(false);

        }
        for (int i = 0; i < totalguns; i++)
        {

            GetComponent<weapons>().guns[i].SetActive(false);
        }
        //  GetComponent<weapons>().firebuttons[0].GetComponent<Button>().interactable = false;
        // GetComponent<weapons>().firebuttons[1].GetComponent<Button>().interactable = false;
        WeaponLists[0].gameObject.SetActive(true);
        GetComponent<weapons>().guns[0].SetActive(true);
        
        WeaponLists[0].GetComponent<AudioSource>().PlayOneShot(WeaponLists[0].GetComponent<WeaponLauncher>().SoundReloading);
        GetComponent<Animator>().Play("shoot");
        
        //shootchk = chk;
		
	}
    public void Weapon2Start()
    {
        //if (this.gameObject.tag == "Player")
        for (int i = 0; i < totalbtns; i++)
        {
            GetComponent<weapons>().firebuttons[i].SetActive(false);

        }
        for (int i = 0; i < totalguns; i++)
        {

            GetComponent<weapons>().guns[i].SetActive(false);
        }
        //   GetComponent<weapons>().firebuttons[0].GetComponent<Button>().interactable = false;
        //  GetComponent<weapons>().firebuttons[1].GetComponent<Button>().interactable = false;
        WeaponLists[1].gameObject.SetActive(true);
        GetComponent<weapons>().guns[1].SetActive(true);
       
        WeaponLists[1].GetComponent<AudioSource>().PlayOneShot(WeaponLists[1].GetComponent<WeaponLauncher>().SoundReloading);
        GetComponent<Animator>().Play("shoot1");
        //shootchk = chk;

    }


    public void Weaponanim(string chk)
    {
        //if (this.gameObject.tag == "Player")
        // GetComponent<Animator>().Play("shoot");
        
        if (chk == "true")
            if (CurrentWeapon < WeaponLists.Length && WeaponLists[CurrentWeapon] != null)
            {
                WeaponLists[0].Shoot();
            }
        Invoke("weaponoff",1f);
      //  WeaponLists[0].GetComponent<AudioSource>().PlayOneShot(WeaponLists[0].GetComponent<WeaponLauncher>().SoundReloading);

    }
    
    public void Weaponanim1(string chk)
    {
        //if (this.gameObject.tag == "Player")
        // GetComponent<Animator>().Play("shoot");
        if (chk == "true")
            if (CurrentWeapon < WeaponLists.Length && WeaponLists[CurrentWeapon] != null)
            {
                WeaponLists[1].Shoot();
            }

        Invoke("weaponoff", 1f);
    }


    void weaponoff()
    {

   
        for (int i = 0; i < totalguns; i++) {

            GetComponent<weapons>().guns[i].SetActive(false);
        }
        for (int i = 0; i < WeaponLists.Length; i++)
        {

            WeaponLists[i].gameObject.SetActive(false);
        }
        GetComponent<weapons>().guns[2].SetActive(true);
        for (int i = 0; i < totalbtns; i++)
        {
            GetComponent<weapons>().firebuttons[i].SetActive(true);
            
        }

    }
    public void WStart(bool chk)
    {
        if (mechwing)
        {
            mechwing.enabled = true;
            mechwing.Play("firewingson");
           
        }
        shootchk = chk;
        //if(WeaponLists.Length == 3)
        //WeaponLists[2].gameObject.SetActive(true);
        WeaponLists[CurrentWeapon].gameObject.SetActive(true);
        for (int i = 0; i < guntorotate.Length; i++)
        {
            if (guntorotate[i] != null)
            {
                guntorotate[i].enabled = true;

            }
        }

    }

    //void Wwingstart() {

       
    //    for (int i = 0; i < guntorotate.Length; i++)
    //    {
    //        if (guntorotate[i] != null)
    //        {
    //            guntorotate[i].enabled = true;

    //        }
    //    }

    //}
    public void WExit(bool chk)
    {
        if (mechwing)
        {
            mechwing.enabled = true;
            mechwing.Play("firewingsoff");
          
        }

        shootchk = chk;
        //if (WeaponLists.Length == 3)
        //    WeaponLists[2].gameObject.SetActive(false);
       
        for (int i = 0; i < guntorotate.Length; i++)
        {
            if (guntorotate[i] != null)
            {
                guntorotate[i].enabled = false;

            }
        }
        Invoke("offdelay",0.5f);
       
    }

    void offdelay() {

        WeaponLists[CurrentWeapon].gameObject.SetActive(false);
    }

    //void Wwingend()
    //{


    //    for (int i = 0; i < guntorotate.Length; i++)
    //    {
    //        if (guntorotate[i] != null)
    //        {
    //            guntorotate[i].enabled = false;

    //        }
    //    }

    //}
}
