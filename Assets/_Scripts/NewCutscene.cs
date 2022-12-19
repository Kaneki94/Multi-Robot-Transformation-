using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewCutscene : MonoBehaviour
{
    public GameObject fade, SolarSystem, Planets, textPanel, spaceShip, Garage, dummyearth, afterstory, skipbtn;
    public GameObject BGMusic;
    public Text[] text;
    public GameObject[] PlanetsModel , cams, robots, Conversation;
    public Transform flyObject, GarageCamTras, EnemiescamTrans, CityCamTrans;
    void Start()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.hideSmallBanner();
        }
        BGMusic.SetActive(false);
        for(int i = 0;i<cams.Length; i++)
        {
            cams[i].SetActive(false);
        }
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        cams[0].SetActive(true);
        textPanel.SetActive(true);
        text[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        textPanel.SetActive(false);
        text[0].gameObject.SetActive(false);
        SolarSystem.SetActive(true);
        yield return new WaitForSeconds(4f);
        fade.SetActive(true);
        SolarSystem.SetActive(false);
        textPanel.SetActive(true);
        text[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textPanel.SetActive(false);
        text[1].gameObject.SetActive(false);
        Planets.SetActive(true);
        spaceShip.SetActive(true);
        flyObject.DOLocalMoveZ(10.0f, 7f);
        flyObject.DOLocalMoveY(-0.6f, 7f);
        yield return new WaitForSeconds(7f);
        skipbtn.SetActive(true);
        fade.SetActive(true);
        spaceShip.SetActive(false);
        Planets.SetActive(false);
        Garage.SetActive(true);
        cams[0].SetActive(false);
        cams[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        EnemiescamTrans.DOLocalMove(new Vector3(104, 13, -100), 3f);
        yield return new WaitForSeconds(3f);
        EnemiescamTrans.DOLocalMoveX(41.5f, 3f);
        yield return new WaitForSeconds(3f);
        EnemiescamTrans.DOLocalMoveX(-12.5f, 3f);
        yield return new WaitForSeconds(3f);
        EnemiescamTrans.DOLocalMoveX(-77.5f, 3f);
        yield return new WaitForSeconds(3f);
        cams[1].SetActive(false);
        cams[2].SetActive(true);
        robots[1].SetActive(true);
        robots[1].GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSeconds(6.5f);
        robots[1].GetComponent<Animator>().SetBool("Talk", true);
        this.gameObject.transform.parent.GetComponent<AudioSource>().enabled = false;
        Conversation[0].SetActive(true);
        yield return new WaitForSeconds(4f);
        robots[1].GetComponent<Animator>().SetBool("Idle", true);
        //robots[1].GetComponent<Animator>().enabled = false;
        Conversation[0].SetActive(false);
        Conversation[1].SetActive(true);
        GarageCamTras.DOLocalMove(new Vector3(-40f, 16f, 72.4f), 5);
        GarageCamTras.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 5);
        robots[0].GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1f);
        dummyearth.SetActive(true);
        yield return new WaitForSeconds(5f);
        Conversation[1].SetActive(false);
        BGMusic.SetActive(true);
        GarageCamTras.DOLocalMove(new Vector3(-18.773f, 26.1f, 110.42f), 4f);
        yield return new WaitForSeconds(4f);
        cams[2].SetActive(false);
        Garage.SetActive(false);
        cams[3].SetActive(true);
        CityCamTrans.DOLocalMoveY(-117, 6f);
        yield return new WaitForSeconds(4f);
        afterstory.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
