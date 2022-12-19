using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Storyone : MonoBehaviour
{
    public Transform flyObject;
    public Transform storyCamera, teleportTrans;
    public GameObject teleport;
    public Vector3 cameraRot;
    public Vector3 camScene;
    public GameObject storyPanel, skipPanel;
    public GameObject[] animals;
    public GameObject[] staticRobots;
    public GameObject players;

    // Start is called before the first frame update
    void Start()
    {
        StoryCutscene();
    }

    public void StoryCutscene()
    {
        flyObject.DOLocalMoveX(-82.4f, 7f).OnComplete(() =>
        {
            flyObject.DOLocalMoveY(-30f, 4f).OnComplete(() =>
            {
                storyCamera.DOLocalMoveX(-0.04f, 0.1f);
                storyCamera.DOLocalMoveY(-9.01f, 0.1f);
                storyCamera.DOLocalMoveZ(50f, 0.1f);

                storyCamera.DOLocalRotate(camScene, 2f).OnComplete(() =>
                {
                    teleport.SetActive(true);
                    //teleportTrans.DOScale(2, 0.1f).OnComplete(() =>
                    //{
                        foreach (var a in animals)
                        {
                            a.SetActive(true);
                            a.transform.DOLocalMoveX(-90f, 5.5f).OnComplete(() =>
                            {
                                a.SetActive(false);
                            });
                        }
                    //});

                });
                

                Invoke("ActivatePlayer", 5f);
            });
        });
    }

    public void ActivatePlayer()
    {
        storyPanel.SetActive(false);
        skipPanel.SetActive(true);
        storyCamera.DOLocalMoveY(33f, 0.1f);
        storyCamera.DOLocalMoveX(3.38f, 0.1f);
        storyCamera.DOLocalRotate(cameraRot, 2f).OnComplete(() =>
        {
            foreach (var r in staticRobots)
            {
                r.SetActive(false);
            }
            players.SetActive(true);
        });
        Invoke("CutsceneOff", 8f);
    }
    void CutsceneOff()
    {
        levelmanager.instance.StartGamePlay();
    }
    private void OnDisable()
    {
        DOTween.Pause(flyObject);
        DOTween.Pause(storyCamera);
        foreach (var a in animals)
        {
            DOTween.Pause(a.transform);
        }
    }
}
