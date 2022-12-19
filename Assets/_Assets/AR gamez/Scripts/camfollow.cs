
using UnityEngine;

public class camfollow : MonoBehaviour
{
    GameObject playercContainer;
    Vector3 offset;
    public float speed;
    public Transform winfollow;
    PlayerController1 playercont;
    // Start is called before the first frame update
    void Start()
    {
        playercont = GameObject.FindObjectOfType<PlayerController1>();
        if (playercont != null)
        {
            playercContainer = playercont.gameObject;
            offset = transform.position - playercContainer.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playercContainer != null )
        {
            transform.position = Vector3.Lerp(transform.position, playercContainer.transform.position + offset, speed * Time.deltaTime);
        }

        //if (playercont.winlevel)
        //{
        //    transform.position = Vector3.Lerp(transform.position, winfollow.position, 5 * Time.deltaTime);
        //}

    }
    public void OpenCp(string cp)
    {
        Application.OpenURL(cp);
    }
}
