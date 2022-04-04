using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class levelmanager : MonoBehaviour
{

    public static levelmanager instance;
    public GameObject[] levels;
    public int selectedlevel = 0, selectedgun = 0;
    public int remaintargets, seasonnum = 0, coinsearned;
    public int[] targets, targetcoins;
    public GameObject[] players, controls, cameras, Trans, playerpos, pickups;
    public GameObject minimap, currentplayer, env, buildings1, buildings2, trees, windance, currentpickup, videobtn;
    public Text enemies_remain, coinstext, novideotext;
    public GameObject currentlevelz, othercanvas, story;
    public GameObject enemycoinsprefab, enemycoins;
    public GameObject[] Animals, Animalfortrans;
    public Avatar[] animalavatars;
    public RuntimeAnimatorController[] animalanims;
    int counter = 0;
    public Animator animaltrans, toytrans, tankanim;
    //public RuntimeAnimatorController[] transanim;

    int otherz = 0;
    public bool picked = false;
    public Material robobodymat, carbodymat, wingmat, toymat, tankmat;
    public Texture[] roboskins, carskins, wingskins, toyskins, tankskins;
    public Animator rcolourmenu, ccolourmenu, tcolourmenu, tkcolormenu;

    //  int targetframes = 30;

    // Use this for initialization
    void Awake()
    {
        instance = this;
        enemycoins = Instantiate(enemycoinsprefab, transform.position, transform.rotation);
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (SystemInfo.systemMemorySize < 2048 || SystemInfo.graphicsMemorySize <= 512)
        {
            buildings1.SetActive(true);
        }
        else
        {
            buildings2.SetActive(true);
            trees.SetActive(true);
        }

        env.SetActive(true);

        story.SetActive(true);
        Invoke("soundstory", 3f);
        coinsearned = 0;
        DamageManager.playerHP = 3000;
        selectedlevel = PlayerPrefs.GetInt("selectedlevel");

        coinstext.text = "<size=18><color=#00FF0D>" + coinsearned.ToString() + "</color></size><size=20><color=#F2FF00>/" + targetcoins[selectedlevel - 1].ToString() + "</color></size>";

        selectedgun = PlayerPrefs.GetInt("Gun");
        for (int i = 0; i < players.Length; i++)
        {

            players[i].SetActive(false);
            controls[i].SetActive(false);
            cameras[i].SetActive(false);
        }
        remaintargets = targets[selectedlevel - 1];

        currentlevelz = Instantiate(levels[selectedlevel - 1].gameObject, transform.position, transform.rotation);
        Animals[selectedgun].SetActive(true);
        Animalfortrans[selectedgun].SetActive(true);

        players[1].GetComponent<Animator>().avatar = animalavatars[selectedgun];
        players[1].GetComponent<Animator>().runtimeAnimatorController = animalanims[selectedgun];
        players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.2f;
        players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 4.5f;

    }

    public void colormenustart()
    {

        if (rcolourmenu.gameObject.activeInHierarchy)
            rcolourmenu.Play("open");
        else if (ccolourmenu.gameObject.activeInHierarchy)
            ccolourmenu.Play("open");
        else if (tcolourmenu.gameObject.activeInHierarchy)
            tcolourmenu.Play("open");
        else if (tkcolormenu.gameObject.activeInHierarchy)
            tkcolormenu.Play("open");



        Invoke("delaymenuoff", 3f);
    }

    void delaymenuoff()
    {

        if (rcolourmenu.gameObject.activeInHierarchy)
            rcolourmenu.Play("close");
        else if (ccolourmenu.gameObject.activeInHierarchy)
            ccolourmenu.Play("close");
        else if (tcolourmenu.gameObject.activeInHierarchy)
            tcolourmenu.Play("close");
        else if (tkcolormenu.gameObject.activeInHierarchy)
            tkcolormenu.Play("close");

    }

    public void AnimalchangerReward(string rewardtype)
    {
        if (AdsManager.Instance)
            AdsManager.Instance.ShowRewardedInterstitialAd(rewardtype, 100);
    }

    public void ToychangerReward(string rewardtype)
    {
        if (AdsManager.Instance)
            AdsManager.Instance.ShowRewardedInterstitialAd(rewardtype, 100);
    }

    public void novideopopup()
    {

        novideotext.gameObject.SetActive(true);
        videobtn.GetComponent<Button>().interactable = false;
        Invoke("videoofftext", 2f);
    }

    void videoofftext()
    {

        novideotext.gameObject.SetActive(false);
        videobtn.GetComponent<Button>().interactable = true;
    }

    public void AnimalChanger()
    {

        Animals[counter].SetActive(false);
        Animalfortrans[counter].SetActive(false);

        Debug.Log(counter);
        if (counter < Animals.Length - 1)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }

        Animals[counter].SetActive(true);
        Animalfortrans[counter].SetActive(true);

        players[1].GetComponent<Animator>().avatar = animalavatars[counter];
        players[1].GetComponent<Animator>().runtimeAnimatorController = animalanims[counter];

        switch (counter)
        {

            case 0:
            case 1:
            case 5:
            case 6:
            case 8:
                players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.2f;
                players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 4.5f;
                break;

            case 4:
                players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.4f; // for tiger for sea horse
                players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 4.5f;
                break;

            case 2:
            case 3:
                players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.35f; // for horse
                players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 4.5f;
                break;

            case 7:
                players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.35f;
                players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 1f;
                break;

            case 9:
                players[1].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.2f;
                players[1].GetComponent<ThirdPersonCharacter>().m_AnimSpeedMultiplier = 1.5f;
                break;

        }

    }


    public void powerupchk()
    {
        if (currentplayer.GetComponent<newpick>())
        {
            if (Vector3.Distance(currentplayer.transform.position, currentpickup.transform.position) > 31f || picked)
            {
                currentpickup.SetActive(false);

                currentpickup = pickups[otherz];


                currentpickup.transform.position = currentplayer.GetComponent<newpick>().spawnpos.position;


                currentpickup.SetActive(true);



                if (otherz < pickups.Length - 1)
                {
                    otherz++;

                    if (otherz == 2 && currentplayer.gameObject.name != "animal")
                    {
                        otherz = 0;
                    }

                }
                else
                {
                    otherz = 0;


                }
                picked = false;

            }
        }


        Invoke("powerupchk", 3f);

    }

    void soundstory()
    {
        story.GetComponent<AudioSource>().enabled = true;

        Invoke("StartGamePlay", 40f);
    }

    public void StartGamePlay()
    {
        if (story.activeInHierarchy)
        {
            delaystartup();
        }
    }

    public void delaystartup()
    {
        story.SetActive(false);
        othercanvas.SetActive(true);
        switch (selectedlevel)
        {
            case 1:
                minimap.GetComponent<MapCanvasController>().playerTransform = players[5].transform;
                controls[5].SetActive(true);
                cameras[5].SetActive(true);
                players[5].SetActive(true);
                currentplayer = players[5].gameObject;
                break;
            case 4:
            case 9:
            case 16:
            case 22:
            case 25:
            case 31:
            case 34:
            case 37:
                minimap.GetComponent<MapCanvasController>().playerTransform = players[0].transform;
                controls[0].SetActive(true);
                cameras[0].SetActive(true);
                players[0].SetActive(true);
                currentplayer = players[0].gameObject;
                break;

            case 2:
            case 5:
            case 11:
            case 14:
            case 17:
            case 23:
            case 27:
            case 33:
            case 39:
                minimap.GetComponent<MapCanvasController>().playerTransform = players[1].transform;
                controls[1].SetActive(true);
                cameras[1].SetActive(true);
                players[1].SetActive(true);
                currentplayer = players[1].gameObject;
                break;

            case 3:
            case 7:
            case 13:
            case 19:
            case 21:
            case 24:
            case 29:
            case 36:
                minimap.GetComponent<MapCanvasController>().playerTransform = players[2].transform;
                controls[2].SetActive(true);
                cameras[2].SetActive(true);
                players[2].SetActive(true);
                currentplayer = players[2].gameObject;
                break;

            case 6:
            case 8:
            case 10:
            case 12:
            case 15:
            case 18:
            case 20:
            case 26:
            case 28:
            case 30:
            case 32:
            case 35:
            case 38:
            case 40:
                minimap.GetComponent<MapCanvasController>().playerTransform = players[3].transform;
                controls[3].SetActive(true);
                cameras[3].SetActive(true);
                players[3].SetActive(true);
                currentplayer = players[3].gameObject;
                break;

        }

        currentplayer.transform.localPosition = playerpos[selectedlevel - 1].transform.localPosition;
        currentplayer.transform.localRotation = playerpos[selectedlevel - 1].transform.localRotation;

        GetComponent<missions>().skipstartstatement();



    }



    public void cointextz()
    {
        if (coinsearned >= targetcoins[selectedlevel - 1])
        {
            coinstext.text = "<size=18><color=#F2FF00>" + coinsearned.ToString() + "</color></size><size=20><color=#F2FF00>/" + targetcoins[selectedlevel - 1].ToString() + "</color></size>";
        }
        else
        {
            coinstext.text = "<size=18><color=#00FF0D>" + coinsearned.ToString() + "</color></size><size=20><color=#F2FF00>/" + targetcoins[selectedlevel - 1].ToString() + "</color></size>";
        }

    }


    public void countfunt()
    {
        remaintargets--;

        if (remaintargets <= 0)
        {

            Invoke("waitsuccess", 2f);
            GetComponent<mygamemanager>().gameplaycontrols.SetActive(false);
        }
    }

    public void coinscount(int coinsreceived)
    {


        coinsearned = coinsearned + coinsreceived;

        cointextz();

        if (coinsearned >= targetcoins[selectedlevel - 1])
        {
            PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + coinsearned);
            GetComponent<mygamemanager>().gameplaycontrols.SetActive(false);
            Invoke("victorydance", 2f);

        }
    }

    public void colormenu()
    {
        if (rcolourmenu.gameObject.activeInHierarchy)
            rcolourmenu.Play("open");
        else if (ccolourmenu.gameObject.activeInHierarchy)
            ccolourmenu.Play("open");
        else if (tcolourmenu.gameObject.activeInHierarchy)
            tcolourmenu.Play("open");
        else if (tkcolormenu.gameObject.activeInHierarchy)
            tkcolormenu.Play("open");

    }

    public void rccolours(int colno)
    {
        //.............Robot Color.................................
        robobodymat.SetTexture("_MainTex", roboskins[colno]);
        robobodymat.SetTexture("_EmissionMap", roboskins[colno]);
        robobodymat.SetTexture("_DetailAlbedoMap", roboskins[colno]);

        //.................Toy Color...............................
        toymat.SetTexture("_MainTex", toyskins[colno]);
        toymat.SetTexture("_EmissionMap", toyskins[colno]);
        toymat.SetTexture("_DetailAlbedoMap", toyskins[colno]);

        //................Car Color.....................................
        carbodymat.SetTexture("_MainTex", carskins[colno]);
        carbodymat.SetTexture("_EmissionMap", carskins[colno]);
        carbodymat.SetTexture("_DetailAlbedoMap", carskins[colno]);

        //...........Wings Color......................................
        wingmat.SetTexture("_MainTex", wingskins[colno]);
        wingmat.SetTexture("_EmissionMap", wingskins[colno]);
        wingmat.SetTexture("_DetailAlbedoMap", wingskins[colno]);

        //...............TankColor.....................................
        tankmat.SetTexture("_MainTex", tankskins[colno]);
        tankmat.SetTexture("_EmissionMap", tankskins[colno]);
        tankmat.SetTexture("_DetailAlbedoMap", tankskins[colno]);

        if (rcolourmenu.gameObject.activeInHierarchy)
            rcolourmenu.Play("close");
        else if (ccolourmenu.gameObject.activeInHierarchy)
            ccolourmenu.Play("close");
        else if (tcolourmenu.gameObject.activeInHierarchy)
            tcolourmenu.Play("close");
        else if (tkcolormenu.gameObject.activeInHierarchy)
            tkcolormenu.Play("close");


        // Debug.Log("close");

    }


    void victorydance()
    {

        windance.SetActive(true);

        for (int i = 0; i < players.Length; i++)
        {

            players[i].SetActive(false);
            controls[i].SetActive(false);
            cameras[i].SetActive(false);
        }

        Invoke("waitsuccess", 8f);
    }

    public void failcall()
    {

        Invoke("waitfail", 0f);

        //GetComponent<mygamemanager> ().gameplaycontrols.SetActive (false);
    }

    public void waitfail()
    {

        GetComponent<mygamemanager>().Failed();
    }

    public void waitsuccess()
    {

        //  windance.SetActive(false);
        this.gameObject.GetComponent<mygamemanager>().Success();
    }


    public void RobotoTiger()
    {

        controls[0].SetActive(false);
        players[0].SetActive(false);

        Trans[0].transform.localPosition = players[0].transform.localPosition;
        Trans[0].transform.localRotation = players[0].transform.localRotation;



        Trans[0].SetActive(true);
        animaltrans.Play("rt");

        players[1].transform.localPosition = players[0].transform.localPosition;
        players[1].transform.localRotation = players[0].transform.localRotation;



        cameras[0].SetActive(false);

        Invoke("rt", 5f);
    }

    void rt()
    {

        cameras[1].SetActive(true);

        Trans[0].SetActive(false);

        controls[1].SetActive(true);
        players[1].SetActive(true);
        currentplayer = players[1].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[1].transform;

    }

    public void TigertoRobot()
    {

        controls[1].SetActive(false);
        players[1].SetActive(false);

        Trans[1].transform.localPosition = players[1].transform.localPosition;
        Trans[1].transform.localRotation = players[1].transform.localRotation;



        Trans[1].SetActive(true);
        animaltrans.Play("rt1");

        players[0].transform.localPosition = players[1].transform.localPosition;
        players[0].transform.localRotation = players[1].transform.localRotation;



        cameras[1].SetActive(false);

        Invoke("tr", 5f);
    }

    void tr()
    {

        cameras[0].SetActive(true);

        Trans[1].SetActive(false);

        controls[0].SetActive(true);
        players[0].SetActive(true);
        currentplayer = players[0].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[0].transform;
    }

    public void RobotoToy()
    {

        controls[0].SetActive(false);
        players[0].SetActive(false);

        Trans[4].transform.localPosition = players[0].transform.localPosition;
        Trans[4].transform.localRotation = players[0].transform.localRotation;



        Trans[4].SetActive(true);
        toytrans.Play("rt");

        players[4].transform.localPosition = players[0].transform.localPosition;
        players[4].transform.localRotation = players[0].transform.localRotation;



        cameras[0].SetActive(false);

        Invoke("rto", 5f);
    }

    void rto()
    {

        cameras[4].SetActive(true);

        Trans[4].SetActive(false);

        controls[4].SetActive(true);
        players[4].SetActive(true);
        currentplayer = players[4].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[4].transform;

    }

    public void ToytoRobot()
    {

        controls[4].SetActive(false);
        players[4].SetActive(false);

        Trans[4].transform.localPosition = players[4].transform.localPosition;
        Trans[4].transform.localRotation = players[4].transform.localRotation;



        Trans[4].SetActive(true);
        toytrans.Play("rt1");

        players[0].transform.localPosition = players[4].transform.localPosition;
        players[0].transform.localRotation = players[4].transform.localRotation;



        cameras[4].SetActive(false);

        Invoke("tor", 5f);
    }

    void tor()
    {

        cameras[0].SetActive(true);

        Trans[4].SetActive(false);

        controls[0].SetActive(true);
        players[0].SetActive(true);
        currentplayer = players[0].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[0].transform;
    }

    /////////////////////////////////////////////////////////////////////////////////////


    public void RobotoCar()
    {

        controls[0].SetActive(false);
        players[0].SetActive(false);

        Trans[2].transform.localPosition = players[0].transform.localPosition;
        Trans[2].transform.localRotation = players[0].transform.localRotation;

        Trans[2].SetActive(true);

        players[2].transform.localPosition = players[0].transform.localPosition;
        players[2].transform.localRotation = players[0].transform.localRotation;

        players[2].transform.localPosition = new Vector3(players[2].transform.localPosition.x, players[2].transform.localPosition.y + 2f, players[2].transform.localPosition.z);


        cameras[0].SetActive(false);

        Invoke("rc", 4f);
    }

    void rc()
    {

        cameras[2].SetActive(true);

        Trans[2].SetActive(false);

        controls[2].SetActive(true);
        players[2].SetActive(true);
        currentplayer = players[2].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[2].transform;
    }

    /// ////////////////////////////////////////////////////////////////////////////////////////////////

    public void CartoRobot()
    {

        controls[2].SetActive(false);
        players[2].SetActive(false);

        Trans[3].transform.localPosition = players[2].transform.localPosition;
        Trans[3].transform.localRotation = players[2].transform.localRotation;

        Trans[3].SetActive(true);

        players[0].transform.localPosition = players[2].transform.localPosition;
        players[0].transform.localRotation = players[2].transform.localRotation;

        players[0].transform.localPosition = new Vector3(players[0].transform.localPosition.x, players[0].transform.localPosition.y - 1.8f, players[0].transform.localPosition.z);



        cameras[2].SetActive(false);

        Invoke("cr", 4f);
    }

    void cr()
    {

        cameras[0].SetActive(true);

        Trans[3].SetActive(false);

        controls[0].SetActive(true);
        players[0].SetActive(true);
        currentplayer = players[0].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[0].transform;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////


    public void RobotoTank()
    {
        controls[0].SetActive(false);
        players[0].SetActive(false);

        Trans[5].transform.localPosition = players[0].transform.localPosition;
        Trans[5].transform.localRotation = players[0].transform.localRotation;


        Trans[5].SetActive(true);
        tankanim.Play("rt");

        players[5].transform.localPosition = players[0].transform.localPosition + new Vector3(0f, 0.5f, 0f);
        players[5].transform.localRotation = players[0].transform.localRotation;



        cameras[0].SetActive(false);

        Invoke("rtk", 5f);
    }

    void rtk()
    {

        cameras[5].SetActive(true);

        Trans[5].SetActive(false);

        controls[5].SetActive(true);
        players[5].SetActive(true);
        currentplayer = players[5].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[5].transform;

    }

    //...............................................................................

    public void TanktoRobot()
    {
        controls[5].SetActive(false);
        players[5].SetActive(false);

        Trans[5].transform.localPosition = players[5].transform.localPosition;
        Trans[5].transform.localRotation = players[5].transform.localRotation;



        Trans[5].SetActive(true);
        tankanim.Play("rt1");

        players[0].transform.localPosition = players[5].transform.localPosition;
        players[0].transform.localRotation = players[5].transform.localRotation;



        cameras[5].SetActive(false);

        Invoke("tkr", 5f);
    }

    void tkr()
    {

        cameras[0].SetActive(true);

        Trans[5].SetActive(false);

        controls[0].SetActive(true);
        players[0].SetActive(true);
        currentplayer = players[0].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[0].transform;
    }



    /// ///////////////////////////////////////////////////////////////////////////////////////////////////

    public void CartoFlyingCar()
    {

        controls[2].SetActive(false);
        players[2].SetActive(false);
        players[3].transform.localPosition = players[2].transform.localPosition;
        players[3].transform.localRotation = players[2].transform.localRotation;

        //players[0].transform.localPosition = new Vector3(players[0].transform.localPosition.x, players[0].transform.localPosition.y - 1.8f, players[0].transform.localPosition.z);


        cameras[3].SetActive(true);
        cameras[2].SetActive(false);

        controls[3].SetActive(true);
        players[3].SetActive(true);
        currentplayer = players[3].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[3].transform;

    }

    public void FlyingCartoCar()
    {
        controls[3].SetActive(false);
        players[3].SetActive(false);
        players[2].transform.localPosition = players[3].transform.localPosition;
        players[2].transform.localRotation = players[3].transform.localRotation;

        //players[0].transform.localPosition = new Vector3(players[0].transform.localPosition.x, players[0].transform.localPosition.y - 1.8f, players[0].transform.localPosition.z);


        cameras[2].SetActive(true);
        cameras[3].SetActive(false);

        controls[2].SetActive(true);
        players[2].SetActive(true);
        currentplayer = players[2].gameObject;

        minimap.GetComponent<MapCanvasController>().playerTransform = players[2].transform;

    }
}