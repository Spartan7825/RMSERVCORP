using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShieldEffectController : MonoBehaviourPunCallbacks
{
    public GameObject m_EmissiveObject;
    float emissiveIntensity = 1;
    float maxValue = 1;
    bool toggle;
    bool toggleReady;

    public GameObject truck;
    public GameObject robot;

    public bool followRobot;


    float timeRemaining = 0;

    public Image progressBar;
    public Button shieldbutton;


 



    public static ShieldEffectController instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {





        InvokeRepeating("OutputTime", 0.1f, 1f);


        // HMScript.

    }



    private void Update()
    {


        if (MainMenu.instance.GetCurrentLevel() < 990)
        {


            if (followRobot)
            {

                if (Transformation.instance.GetIsTruck())
                {
                    transform.localScale = new Vector3(350f, 350f, 650f);
                    Vector3 newPos = truck.transform.position;
                    newPos.y = truck.transform.position.y + 1;
                    //newPos.x = truck.transform.position.x + 1;

                    transform.position = newPos;

                    transform.rotation = truck.transform.rotation;

                }
                else if (!Transformation.instance.GetIsTruck())
                {

                    transform.localScale = new Vector3(300f, 300f, 300f);
                    Vector3 newPos = robot.transform.position;
                    newPos.y = robot.transform.position.y + 1;

                    transform.position = newPos;

                    transform.rotation = robot.transform.rotation;
                }


            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                progressBar.fillAmount = timeRemaining / 20f;


            }
            else
            {

                shieldbutton.enabled = true;
            }

        }



    }

    public void ActiveRobotShield()
    {

        followRobot = true;
        StartCoroutine(delay(10f));

    }
    public void cooldown()
    {
        timeRemaining = 20;
        shieldbutton.enabled = false;

    }

    IEnumerator delay(float time)
    {

        yield return new WaitForSeconds(time);
        followRobot = false;

        transform.position = new Vector3(0, 1000, 0);

    }



    void OnTriggerStay(Collider collision)
    {




        if (collision.gameObject.tag == "Missile")
        {


            Color emissiveColor = Color.red;
            m_EmissiveObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

            if (emissiveIntensity > 10)
            {
                maxValue = emissiveIntensity;
                emissiveIntensity = emissiveIntensity - 1;
            }
            else if (maxValue > 1 && emissiveIntensity > 1)
            {
                //Debug.Log("asdfas");
                maxValue = maxValue - 1;
                emissiveIntensity = emissiveIntensity - 1;

            }

            else
            {
                maxValue = 0;
                emissiveIntensity = emissiveIntensity + 1;
            }
        }
    }






    void OutputTime()
    {

        Color emissiveColor = Color.blue;
        m_EmissiveObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

        if (emissiveIntensity > 10)
        {
            maxValue = emissiveIntensity;
            emissiveIntensity = emissiveIntensity - 1;
        }
        else if (maxValue > 1 && emissiveIntensity > 1)
        {
            //Debug.Log("asdfas");
            maxValue = maxValue - 1;
            emissiveIntensity = emissiveIntensity - 1;

        }

        else
        {
            maxValue = 0;
            emissiveIntensity = emissiveIntensity + 1;
        }



        /*        if (toggle)
                {
                    Color emissiveColor = Color.blue;
                    m_EmissiveObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

                    if (emissiveIntensity > 10)
                    {
                        maxValue = emissiveIntensity;
                        emissiveIntensity = emissiveIntensity - 1;
                    }
                    else if (maxValue > 1 && emissiveIntensity > 1)
                    {
                        //Debug.Log("asdfas");
                        maxValue = maxValue - 1;
                        emissiveIntensity = emissiveIntensity - 1;
                        toggleReady = true;
                    } else if (toggleReady) { toggle = false; toggleReady = false; }
                    else
                    {
                        maxValue = 0;
                        emissiveIntensity = emissiveIntensity + 1;
                    }
                }
                else
                {
                    Color emissiveColor = Color.red;
                    m_EmissiveObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

                    if (emissiveIntensity > 10)
                    {
                        maxValue = emissiveIntensity;
                        emissiveIntensity = emissiveIntensity - 1;
                    }
                    else if (maxValue > 1 && emissiveIntensity > 1)
                    {
                        //Debug.Log("asdfas");
                        maxValue = maxValue - 1;
                        emissiveIntensity = emissiveIntensity - 1;
                        toggleReady = true;
                    }
                    else if (toggleReady) { toggle = true; toggleReady = false; }
                    else
                    {
                        maxValue = 0;
                        emissiveIntensity = emissiveIntensity + 1;
                    }


                }*/



    }
}
