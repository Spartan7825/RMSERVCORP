using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public GameObject robotPrefab;
    public GameObject truckPrefab;

    public Vector3 truckOffset;
    public Vector3 robotoffset;
    public static bool Istruck = true;
    public static bool Isrobot = false;
    public Camera c;




    public static Transformation instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartValues() 
    {
            Istruck = true;
            Isrobot = false;
    }
    public void Transform()
    {

        if (Isrobot)
        {

            //GameObject ns = Instantiate(truckPrefab,robotPrefab.transform.position,robotPrefab.transform.rotation);
            truckPrefab.transform.position = robotPrefab.transform.position;
            truckPrefab.transform.rotation = robotPrefab.transform.rotation;

            Rigidbody rr = robotPrefab.GetComponent<Rigidbody>();
            Rigidbody rt = truckPrefab.GetComponent<Rigidbody>();
            rt.isKinematic = false;
            rr.isKinematic = true;
            // ns.transform.localScale = robotPrefab.transform.localScale;
            c.GetComponent<CameraScript>().target = truckPrefab.transform;

            robotPrefab.transform.position = new Vector3(0, 10000, 0);
            Isrobot = false;
            Istruck = true;
        }
        else if (Istruck)
        {


            robotPrefab.transform.position = truckPrefab.transform.position;
            robotPrefab.transform.rotation = truckPrefab.transform.rotation;


         

            Rigidbody rr = robotPrefab.GetComponent<Rigidbody>();
            Rigidbody rt = truckPrefab.GetComponent<Rigidbody>();
            rt.isKinematic = true;
            rr.isKinematic = false;


            c.GetComponent<CameraScript>().target = robotPrefab.transform;


            Isrobot = true;
            Istruck = false;

           

            truckPrefab.transform.position = new Vector3(0, 10000, 0);
           


            // StartCoroutine(ExecuteAfterTime(1));
      

        }

    }
    public bool GetIsTruck()
    {
        return Istruck;
    }

}
