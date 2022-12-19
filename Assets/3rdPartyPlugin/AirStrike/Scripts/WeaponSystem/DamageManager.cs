using SWS;
/// <summary>
/// Damage manager. 
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public AudioClip[] HitSound;
    public GameObject Effect, enemybar;
    public GameObject[] robo;
    public int HP = 1000;
    public static int playerHP;
    private int HPmax;
    public ParticleSystem OnFireParticle;
    public Slider healthbar;
    public bool trafficspawner = false;
    public int rewardcoins = 500;
    public GameObject coinsimage;


    float temp;


    private void Start()
    {

        if (this.gameObject.tag == "Player")
        {
            HPmax = playerHP;
        }
        else
        {
            HPmax = HP;
        }
        if (OnFireParticle)
        {
            OnFireParticle.Stop();
        }

        if (!coinsimage && this.gameObject.tag != "Player")
        {

            coinsimage = levelmanager.instance.enemycoins;
        }
    }
    // Damage function
    public void ApplyDamage(int damage, GameObject killer)
    {
        //	Debug.Log (killer);
        if (HP < 0 || playerHP < 0)
            return;

        if (HitSound.Length > 0)
        {
            AudioSource.PlayClipAtPoint(HitSound[Random.Range(0, HitSound.Length)], transform.position);
        }


        if (this.gameObject.tag == "Player")
        {
            playerHP -= damage;
            healthbar.value -= damage;
            // Debug.Log("Player health is " +  playerHP);
        }
        else
        {

            HP -= damage;
            //  float damagemul = 1 / HP;
            if (enemybar)
            {
                enemybar.gameObject.transform.localScale -= new Vector3(damage * 0.003333f, 0, 0);
            }
        }
        //		if (this.gameObject.tag == "Base") {
        ////			 temp = healthbar.transform.localScale.x - damage * 0.0006f;
        ////			healthbar.transform.localScale.x = temp;
        //				//healthbar.gameObject.transform.localScale -= new Vector3 (damage *0.0003f,0,0);
        //		}
        if (OnFireParticle)
        {
            if (this.gameObject.tag == "Player")
            {
                if (playerHP < (int)(HPmax / 2.0f))
                {
                    OnFireParticle.Play();
                }
            }
            else
            {
                if (HP < (int)(HPmax / 2.0f))
                {
                    OnFireParticle.Play();
                }
            }
        }

        if (this.gameObject.tag == "Player")
        {
            if (playerHP <= 0)
            {
                if (this.gameObject.GetComponent<FlightOnDead>())
                {
                    this.gameObject.GetComponent<FlightOnDead>().OnDead(killer);
                }
                Dead();
            }
        }
        else
        {
            if (HP <= 0)
            {
                if (this.gameObject.GetComponent<FlightOnDead>())
                {
                    this.gameObject.GetComponent<FlightOnDead>().OnDead(killer);
                }
                Dead();
            }
        }
    }

    private void Dead()
    {
        if (Effect)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(Effect, transform.position, transform.rotation);

            if (this.GetComponent<Rigidbody>())
            {
                if (obj.GetComponent<Rigidbody>())
                {
                    obj.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
                    obj.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * Random.Range(100, 2000));
                }
            }
        }

        if (this.gameObject.tag == "Player")
        {
            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            Invoke("waitfail", 2f);
        }
        else if (this.gameObject.tag == "TrafficCar")
        {

            //  GetComponent<MapMarker>().isActive = false;
            trafficspawner = true;
            levelmanager.instance.coinscount(rewardcoins);
            if (coinsimage)
            {
                Invoke("coinshow1", 0.2f);

            }
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<splineMove>().Pause();
            Invoke("SpawnAgain", 10f);
            //Destroy(this.gameObject);
            // levelmanager.instance.countfunt();
        }
        else if (this.gameObject.tag == "Extra")
        {
            levelmanager.instance.coinscount(rewardcoins);
            enemycounter.instance.deadcounterextra();
            for (int i = 0; i < robo.Length; i++)
            {

                if (robo[i])
                {
                    Destroy(robo[i].gameObject);
                }
            }
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }
        else
        {


            //this.gameObject.SetActive(false);
            // Invoke("SpawnAg/ain", 10f);




            enemycounter.instance.deadcounter();
            levelmanager.instance.coinscount(rewardcoins);


            if (coinsimage)
            {
                for (int i = 0; i < robo.Length; i++)
                {

                    if (robo[i])
                    {
                        robo[i].SetActive(false);
                    }
                }
                Invoke("coinshow", 0.2f);
                this.gameObject.SetActive(false);
            }
            else
            {

                for (int i = 0; i < robo.Length; i++)
                {

                    if (robo[i])
                    {
                        Destroy(robo[i].gameObject);
                    }
                }
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            //levelmanager.instance.countfunt();
        }
        SpawnPiggyandchicken();
    }

    void coinshow()
    {

        coinsimage.SetActive(false);
        coinsimage.SetActive(true);


        if (levelmanager.instance.currentplayer.GetComponent<newpick>())
        {

            levelmanager.instance.currentplayer.GetComponent<newpick>().coinseffect();
        }

        for (int i = 0; i < robo.Length; i++)
        {

            if (robo[i])
            {
                Destroy(robo[i].gameObject);
            }
        }
        Destroy(this.gameObject);
    }

    void coinshow1()
    {

        coinsimage.SetActive(false);
        coinsimage.SetActive(true);

        if (levelmanager.instance.currentplayer.GetComponent<newpick>())
        {

            levelmanager.instance.currentplayer.GetComponent<newpick>().coinseffect();
        }
    }

    void SpawnAgain()
    {

        HP = HPmax = 100;
        enemybar.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<splineMove>().Resume();
        trafficspawner = false;
        this.gameObject.SetActive(true);

    }

    void waitfail()
    {

        Debug.Log("last failllllllllllllllllllllllllll");
        levelmanager.instance.failcall();

    }

    #region pets
    public GameObject[] pets;
    private void SpawnPiggyandchicken()
    {
        int ran = Random.Range(0, pets.Length);
        GameObject ch;
        if (pets.Length > 0)
        {
            ch = Instantiate(pets[ran], transform.position, transform.rotation);
            Destroy(ch,10f);
        }
    }
    #endregion
}
