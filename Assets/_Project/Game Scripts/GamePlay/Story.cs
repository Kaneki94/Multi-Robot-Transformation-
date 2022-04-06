using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story : MonoBehaviour
{
    public Transform flyObject;
    public Transform storyCamera;
    public Vector3 cameraRot;
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
        flyObject.DOLocalMoveX(-40f, 10f).OnComplete(() =>
        {
            flyObject.DOLocalMoveY(-35f, 7f).OnComplete(() =>
            {
                foreach (var a in animals)
                {
                    a.SetActive(true);
                    a.transform.DOLocalMoveX(-90f, 6.5f).OnComplete(() =>
                    {
                        a.SetActive(false);
                    });
                }

                Invoke("ActivatePlayer", 7f);
            });
        });
    }

    public void ActivatePlayer()
    {
        storyPanel.SetActive(false);
        skipPanel.SetActive(true);
        storyCamera.DOLocalRotate(cameraRot, 2f).OnComplete(() =>
        {
            foreach (var r in staticRobots)
            {
                r.SetActive(false);
            }
            players.SetActive(true);
        });
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
