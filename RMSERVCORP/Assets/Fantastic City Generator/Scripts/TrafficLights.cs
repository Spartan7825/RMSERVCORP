using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficLights : MonoBehaviour {

    private float countTime = 0;
    private int step = 0;

    [System.Serializable]
    public class TrafficLightState
    {
        public int status = 0; // (1 and 4 = RED) , (2 = Yellow) , (3 = Green) 


        public GameObject t31;
        public GameObject t13;
        public GameObject t21;
        public GameObject t12;
        public GameObject t11;


        public GameObject stop31;
        public GameObject stop13;



    }


    public TrafficLightState tState;


    // Use this for initialization
    void Start () {

        countTime = 0;
        step = 0;

        tState.status = (Random.Range(1, 8) < 4) ? 13 : 31;
        EnabledObjects(tState.status);

        InvokeRepeating("Semaforo", Random.Range(0,4), 1);


    }

    private void Semaforo()
    {
        countTime += 1;
        
        if (step == 0)
        {

            if (countTime > 10)
            {
                countTime = 0;
                step = 1;

                    if (tState.status == 13)
                        tState.status = 12;
                    else if (tState.status == 31)
                        tState.status = 21;

                    EnabledObjects(tState.status);

            }

        }
        else if (step == 1)
        {

            if (countTime >= 3)
            {
                countTime = 0;
                step = 2;

                    if (tState.status == 12)
                        tState.status = 41;
                    else if (tState.status == 21)
                        tState.status = 14;
                    EnabledObjects(tState.status);

                }

        }
        else if (step == 2)
        {

            if (countTime >= 3)
            {
                countTime = 0;
                step = 0;

                    if (tState.status == 14)
                        tState.status = 13;
                    else if (tState.status == 41)
                        tState.status = 31;

                    EnabledObjects( tState.status);
            }

        }


    }

    
    void EnabledObjects(int habilita)
    {

        tState.t12.SetActive(habilita == 12);
        tState.t21.SetActive(habilita == 21);
        tState.t13.SetActive(habilita == 13);
        tState.t31.SetActive(habilita == 31);
        tState.t11.SetActive(habilita == 11 || habilita == 14 || habilita == 41);

        tState.stop13.SetActive(habilita != 31);
        tState.stop31.SetActive(habilita != 13);



    }



}
