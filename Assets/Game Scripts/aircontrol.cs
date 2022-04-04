using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class aircontrol : MonoBehaviour
{
    public GameObject nossound;
    private float MoveSpeed = 10f;
    public float SpeedMax = 50.0f;
    public float Speed = 5.0f;
    //int boostspeed = 0;
    public bool boost = false;
    bool brake = false;
    public float DampingVelocity = 5f;
    public GameObject Car;
    public GameObject carnitro;
    float cameradistance;
    bool enablezoom = false;
    bool disablezoom = false;
    float temp;
  //  bool zoomdelayer = true;
    bool gasin = false;
    float boostamount = 25;
    //float breakamount = -70;
    bool breakin = false;
    int tiresOnGround;
  //  private float timeCount = 0.0f;
    private Transform down;
    public float airtime = 0;
    bool airactive = false;


    // Start is called before the first frame update
    public static aircontrol ins;
    private void Start()
    {
        ins = this;
        foreach (Transform child in Car.transform)
        {
            if (child.tag == "nitroparticle")
            {
                carnitro = child.gameObject;
            }
           
                
        }
    }
    
    private void FixedUpdate()
    {

        //Debug.Log(Car.GetComponent<Rigidbody>().rotation.z);
        Vector3 velocityTarget = Vector3.zero;
        Quaternion rot = Car.transform.rotation;
        
        velocityTarget = (Car.GetComponent<Rigidbody>().rotation * Vector3.forward) * (Speed + MoveSpeed);
        MoveSpeed = Mathf.Lerp(MoveSpeed, Speed, Time.deltaTime);
        //Debug.Log(velocityTarget);
        
        if (boost == true)
        {
           

            //if (carcollisiondetector.instance.isgrounded == false )
            //{
            //    //Car.GetComponent<Rigidbody>().Sleep();
            //    Car.GetComponent<Rigidbody>().rotation = new Quaternion(rot.x, rot.y, rot.z + 0.002f, rot.w);
            //}

            //boostspeed++;
            //SpeedUpp(boostspeed);
            //Car.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Car.GetComponent<Rigidbody>().velocity, velocityTarget, Time.fixedDeltaTime * DampingVelocity * 0.4f);

            //Car.GetComponent<Rigidbody>().AddForce(0, 5000, 0);
            //Debug.Log(Car.GetComponent<Rigidbody>().rotation.y);
            
            if (tiresOnGround == 4 && airtime > 0.5f)
            {
                if(airactive)
                {
                    Car.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    airactive = false;
                }
                if (rot.w > 0)
                  Car.transform.rotation = new Quaternion(rot.x + 0.0025f, rot.y , rot.z , rot.w).normalized;
                else if(rot.w < 0)
                    Car.transform.rotation = new Quaternion(rot.x - 0.0025f, rot.y, rot.z, rot.w).normalized;
                //  Car.GetComponent<Rigidbody>().rotation = new Quaternion(rot.x, rot.y , rot.z , rot.w - 0.01f).normalized;

                //Car.GetComponent<Rigidbody>().rotation = Quaternion.Slerp(Car.GetComponent<Rigidbody>().rotation, new Quaternion(0, 0, rot.z + 10f, 0).normalized, timeCount);
                //down.rotation = new Quaternion(0, 0, 0, 0).normalized;
                //down.rotation = down.rotation.normalized;
            }
            //Car.transform.rotation = Quaternion.Slerp(Car.transform.rotation, down.rotation , timeCount);
            //Car.GetComponent<Rigidbody>().AddForce(
            //Car.GetComponent<RCC_CarControllerV3>().COM.TransformDirection(new Vector3(0, -10, boostamount)),
            //ForceMode.Acceleration);

            

            Car.GetComponent<Rigidbody>().AddForce(
            Car.GetComponent<RCC_CarControllerV3>().COM.TransformDirection(new Vector3(0, 0 , boostamount)),
            ForceMode.Acceleration);

            if (Car.GetComponent<RCC_CarControllerV3>().speed < 1)
                StartCoroutine(Waitforsec());
            if (gasin)
            {
                Car.GetComponent<RCC_CarControllerV3>().gasInput = 1f;
                Car.GetComponent<RCC_CarControllerV3>().clutchInput = 0f;
            }
            if(tiresOnGround == 0)
            {
                airactive = true;
            }
            //if (tiresOnGround == 4)
            //    Car.GetComponent<Rigidbody>().rotation = new Quaternion(rot.x, rot.y, rot.z + 0.002f, rot.w);

            //if(airtime > 5)
            //{
            //    Car.GetComponent<Rigidbody>().AddForce(
            //Car.GetComponent<RCC_CarControllerV3>().COM.TransformDirection(new Vector3(0, boostamount, 0)),
            //ForceMode.Acceleration);
            //}




            //    Car.GetComponent<Rigidbody>().AddForce(
            //    Car.GetComponent<RCC_CarControllerV3>().COM.TransformDirection(new Vector3(0, -10, 0)),
            //    ForceMode.Acceleration);


        }
        // Car.GetComponent<Rigidbody>().rotation = new Quaternion(rot.x, rot.y + 1f, rot.z , rot.w);
        //Debug.Log(Car.GetComponent<Rigidbody>().rotation.z);
        //if(boost == false)
        //{
        //    Car.GetComponent<Rigidbody>().WakeUp();
        //}
        //Debug.Log(Car.GetComponent<Rigidbody>().rotation);
        if ( brake == true )
        {
            //Car.GetComponent<Rigidbody>().AddForce(
            //Car.GetComponent<RCC_CarControllerV3>().COM.TransformDirection(new Vector3(0, 0, breakamount)),
            //ForceMode.Acceleration);
            if (Car.GetComponent<RCC_CarControllerV3>().direction == 1 )
            {
                velocityTarget = (Car.GetComponent<Rigidbody>().rotation * Vector3.forward);
                Car.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Car.GetComponent<Rigidbody>().velocity, velocityTarget, Time.fixedDeltaTime * DampingVelocity);
                //GetComponent<Rigidbody>().AddForce((Physics.gravity * GetComponent<Rigidbody>().mass) * 2f);
                //Debug.Log(GetComponent<Rigidbody>().velocity);
            }
            if(Car.GetComponent<RCC_CarControllerV3>().speed < 10)
            {
                Car.GetComponent<RCC_CarControllerV3>().clutchInput = 1f;
            }
            if(Car.GetComponent<RCC_CarControllerV3>().clutchInput == 1)
            {
                StartCoroutine(waitforsecbreak());
            }
            if(breakin)
                Car.GetComponent<RCC_CarControllerV3>().clutchInput = 0f;
            //velocityTarget = (Car.GetComponent<Rigidbody>().rotation * Vector3.forward);
            //Car.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Car.GetComponent<Rigidbody>().velocity, velocityTarget, Time.fixedDeltaTime * DampingVelocity * 0.2f);
        }
        if (Car.GetComponent<RCC_CarControllerV3>().speed < 1)
            gasin = false;
        if (Car.GetComponent<RCC_CarControllerV3>().speed > 200)
            boostamount = 0;
        else
            boostamount = 25;

        //if (!(Car.GetComponent<RCC_CarControllerV3>().speed > 60) && Car.GetComponent<RCC_CarControllerV3>().brakeInput == 1)
        //    breakamount = 0f;
        //if((Car.GetComponent<RCC_CarControllerV3>().gasInput == 1 || boost == true) && Car.GetComponent<RCC_CarControllerV3>().brakeInput == 0)
        //    breakamount = -70f;
        if (Car.GetComponent<RCC_CarControllerV3>().brakeInput == 1 && Car.GetComponent<RCC_CarControllerV3>().speed > 60)
            Car.GetComponent<RCC_CarControllerV3>().cutGas = true;
        if (Car.GetComponent<RCC_CarControllerV3>().direction == 1)
            breakin = false;
       

    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(0f);
        gasin = true;

    }
    IEnumerator waitforsecbreak()
    {
        yield return new WaitForSeconds(1f);
        breakin = true;
    }    
    private void Update()
    {
        tiresOnGround = 0;
        if (!Car.GetComponent<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.isGrounded)
        {
            tiresOnGround++;

        }
        if (!Car.GetComponent<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.isGrounded)
        {
            tiresOnGround++;

        }
        if (!Car.GetComponent<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.isGrounded)
        {
            tiresOnGround++;

        }
        if (!Car.GetComponent<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.isGrounded)
        {
            tiresOnGround++;
        }
        if (tiresOnGround == 4)
        {
            airtime += Time.deltaTime;
        }
        if(tiresOnGround == 0)
        {
            airtime = 0;
        }
        if (enablezoom)
        {

            if (temp <= 15f)
            {
                temp += 0.05f;
                RCC_Camera.instance.TPSDistance = temp;
                //Debug.Log(temp);
            }
        }
        if(disablezoom)
        {
            if(temp >= 12)
            {
                temp -= 0.05f;
                RCC_Camera.instance.TPSDistance = temp;
                //Debug.Log(cameradistance);
            }
                       
        }
        


        
    }
    public void Spdup()
    {
        nossound.gameObject.GetComponent<AudioSource>().Play();
        airactive = true;
       
         cameradistance = RCC_Camera.instance.TPSDistance;
        temp = cameradistance;
       carnitro.SetActive(true);
        boost = true;
        enablezoom = true;
        disablezoom = false;
        
    }
    public void Spdup1()
    {
        nossound.gameObject.GetComponent<AudioSource>().Stop();
        airactive = false; 
        boost = false;
       carnitro.SetActive(false);
        //boostspeed = 0;
        enablezoom = false;
        disablezoom = true;
        
    }
    public void Spdup2()
    {
        Invoke("Spdup1",0.5f);
    }
    public void Spddown()
    {
        brake = true;
    }
    public void Spddown1()
    {
        brake = false;
      //  boostspeed = 0;
    }
    public void SpeedUpp(float delta)
    {
        MoveSpeed = Mathf.Lerp(MoveSpeed, SpeedMax, Time.deltaTime * (10 * delta));
    }

}
