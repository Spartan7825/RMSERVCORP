using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TrafficCar : MonoBehaviour
{

    private float timeStoped;

    //[HideInInspector]
    public GameObject path;

    //[HideInInspector]
    public GameObject atualWay;


    [HideInInspector]
    public Transform mRayC;

    [HideInInspector]    
    public Transform[] wheel;

    public WheelCollider[] wCollider;
    
    private int countWays;
    private Transform[] nodes;
    public int currentNode = 0;
    private float distance;
    private float steer = 0.0f;

    private float speed;
    private float brake = 0;
    private float motorTorque = 0;

    private Vector3 steerCurAngle = Vector3.zero;

    private Rigidbody myRigidbody;

    private FCGWaypointsContainer atualWayScript;

    private Vector3 relativeVector;

    public CarWheelsTransform wheelsTransforms;

    private FCGWaypointsContainer fcgWaypointsContainer;

    

    [System.Serializable]
    public class CarWheelsTransform 
    {

        public Transform frontRight;
        public Transform frontLeft;

        public Transform backRight;
        public Transform backLeft;

        public Transform backRight2;
        public Transform backLeft2;

    }


    public CarSetting carSetting;

    [System.Serializable]
    public class CarSetting
    {

        public bool showNormalGizmos = false;

        public Transform carSteer;
        
        [Range(10000, 60000)]
        public float springs = 25000.0f;

        [Range(1000, 6000)]
        public float dampers = 1500.0f;

        [Range(60, 200)]
        public float carPower = 120f;

        [Range(5, 10)]
        public float brakePower = 8f;

        
        [Range(20, 30)]
        public float limitSpeed = 30.0f;

        [Range(30, 72)]
        public float maxSteerAngle = 40.0f;

    }

    private Vector3 shiftCentre = new Vector3(0.0f, -0.05f, 0.0f);

    private Transform GetTransformWheel(string wheelName)
    {
        GameObject[] wt;

        wt = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Equals(wheelName) && g.transform.parent.root == transform).ToArray();

        if (wt.Length > 0)
            return wt[0].transform;
        else
            return null;

    }


    public void Configure()
    {

        if (!wheelsTransforms.frontRight)
            wheelsTransforms.frontRight = GetTransformWheel("FR");

        if (!wheelsTransforms.frontLeft)
            wheelsTransforms.frontLeft = GetTransformWheel("FL");

        if (!wheelsTransforms.backRight)
            wheelsTransforms.backRight = GetTransformWheel("BR");

        if (!wheelsTransforms.backLeft)
            wheelsTransforms.backLeft = GetTransformWheel("BL");

        if (!wheelsTransforms.backRight2)
            wheelsTransforms.backRight2 = transform.Find("BR2");

        if (!wheelsTransforms.backLeft2)
            wheelsTransforms.backLeft2 = transform.Find("BL2");


        if (!transform.GetComponent<Rigidbody>())
            transform.gameObject.AddComponent<Rigidbody>();
        
        if (transform.gameObject.GetComponent<Rigidbody>().mass < 4000f)
            transform.gameObject.GetComponent<Rigidbody>().mass = 4000f;


        float p = wheelsTransforms.frontRight.localPosition.z + 0.6f;

        if (!transform.Find("RayC"))
        {
            mRayC = new GameObject("RayC").transform;
            mRayC.SetParent(transform);
            mRayC.localRotation = Quaternion.identity; 
            mRayC.localPosition = new Vector3(0f, 0.5f, p);
        }
        else if (!mRayC)
            mRayC = transform.Find("RayC");


        carSetting.maxSteerAngle = (int)Mathf.Clamp(Vector3.Distance(wheelsTransforms.frontRight.transform.position, wheelsTransforms.backRight.transform.position) * 12, 35, 72);

             
        wheel = new Transform[4];
        wCollider = new WheelCollider[4];


        GameObject center = new GameObject("Center");
        Vector3[] centerPos = new Vector3[4];
        Vector3 nCenter = new Vector3(0, 0, 0);


        wheel[0] = wheelsTransforms.frontRight;
        wheel[1] = wheelsTransforms.frontLeft;
        wheel[2] = wheelsTransforms.backRight;
        wheel[3] = wheelsTransforms.backLeft;

        for(int i = 0; i < 4; i++)
        {
            wCollider[i] = SetWheelComponent(i);
            // Define CenterOfMass
            center.transform.SetParent(wheel[i].transform);
            center.transform.localPosition = new Vector3(0, 0, 0);
            center.transform.SetParent(transform);
            centerPos[i] = center.transform.localPosition -= new Vector3(0, wCollider[i].radius, 0);
            nCenter += centerPos[i];

        }

        shiftCentre = (nCenter / 4);
        DestroyImmediate(center);

    }


    void Start()
    {

        if (path)
        Init(path);
       
    }

    public void Init(GameObject pth)
    {
        path = pth;

        myRigidbody = transform.GetComponent<Rigidbody>();

        myRigidbody.centerOfMass = shiftCentre;


        atualWay = path;
        atualWayScript = atualWay.GetComponent<FCGWaypointsContainer>();

        
        DefineNewPath();

        currentNode = 1;

        distance = Vector3.Distance(nodes[currentNode].position, transform.position);

        InvokeRepeating("MoveCar", 0.02f, 0.02f);

    }


    private WheelCollider SetWheelComponent(int w)
    {
        WheelCollider result;

        if (transform.Find(wheel[w].name + " - WheelCollider"))
        DestroyImmediate(transform.Find(wheel[w].name + " - WheelCollider").gameObject);

        GameObject wheelCol = new GameObject(wheel[w].name + " - WheelCollider");

        wheelCol.transform.SetParent(transform);
        wheelCol.transform.position = wheel[w].position;
        wheelCol.transform.eulerAngles = transform.eulerAngles;

        WheelCollider col = (WheelCollider)wheelCol.AddComponent(typeof(WheelCollider));

        result = wheelCol.GetComponent<WheelCollider>();

        JointSpring js = col.suspensionSpring;

        js.spring = carSetting.springs;
        js.damper = carSetting.dampers;
        col.suspensionSpring = js;

        col.suspensionDistance = 0.05f;
        col.radius = (wheel[w].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * wheel[w].transform.localScale.z) * 0.5f;
        col.mass = 1500;

        return result;

    }


    

  

    void DefineNewPath()
    {

        nodes = new Transform[atualWay.transform.childCount];
        int n = 0;
        foreach (Transform child in atualWay.transform)
            nodes[n++] = child;

        countWays = nodes.Length;
        currentNode = 0;

    }



    float iRC = 0;
 
    void MoveCar()
    {

        relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        steer = ((relativeVector.x / relativeVector.magnitude) * carSetting.maxSteerAngle);

        speed = myRigidbody.velocity.magnitude * 3.6f;

        mRayC.localRotation = Quaternion.Euler(new Vector3(0, steer, 0));

        VerificaPoints();

                     
        
        iRC++;
        if (iRC >= 6)
        {
            brake = FixedRaycasts();
            iRC = 0;
        }



        if (speed < 1)
        {
            timeStoped += Time.deltaTime;

            if (timeStoped > 60)
            {
                //Debug.Log(transform.name + " was destroyed"); 
                Destroy(transform.gameObject);
            }
        }
        else
            timeStoped = 0;

        float bk = 0;

        Quaternion _rot;
        Vector3 _pos;

        for (int k = 0; k < 4; k++)
        {


            if (speed > carSetting.limitSpeed)
                bk = Mathf.Lerp(100, 1000, (speed - carSetting.limitSpeed) / 10);

            if (bk > brake) brake = bk;


            /*
            try
            {
            */

                if (brake == 0)
                 wCollider[k].brakeTorque = 0;
                else
                {
                 wCollider[k].motorTorque = 0;
                 wCollider[k].brakeTorque = carSetting.brakePower * brake;
                }

            /*
            }
            catch (System.Exception e)
            {
                Debug.Log("Error - wheels in " + transform.name);
                Debug.LogException(e, this);
                Destroy(transform.gameObject);
                return;
            }
            */


            if (k < 2)
            {
                motorTorque = Mathf.Lerp(carSetting.carPower * 30, 0, speed / carSetting.limitSpeed);
                wCollider[k].motorTorque = motorTorque;
                wCollider[k].steerAngle = steer;
            }

            wCollider[k].GetWorldPose(out _pos, out _rot);
            wheel[k].position = _pos;
            wheel[k].rotation = _rot;


        }


        if (wheelsTransforms.backRight2)
        {
            wheelsTransforms.backRight2.rotation = wheelsTransforms.backRight.rotation;
            wheelsTransforms.backLeft2.rotation = wheelsTransforms.backRight.rotation;
        }

        //steeringwheel movement
        if (carSetting.carSteer)
        carSetting.carSteer.localEulerAngles = new Vector3(steerCurAngle.x, steerCurAngle.y, steerCurAngle.z - steer);  //carSetting.carSteer.localEulerAngles = new Vector3(steerCurAngle.x, steerCurAngle.y, steerCurAngle.z + ((steer / 180) * -30.0f));
        
    }



    private void VerificaPoints()
    {

        if (distance < 5)
        {

            if (currentNode < countWays - 1)
                currentNode++;
            else
            {
                atualWay = atualWayScript.nextWay[Random.Range(0, atualWayScript.nextWay.Length)];

                atualWayScript = atualWay.GetComponent<FCGWaypointsContainer>();

                DefineNewPath();

            }

        }

        distance = Vector3.Distance(nodes[currentNode].position, transform.position);


    }


    float FixedRaycasts()
    {

        RaycastHit hit;
        int wdist = 6;
        float rStop = 0;

        mRayC.localRotation = Quaternion.Euler(new Vector3(0, steer, 0));

        Debug.DrawRay(mRayC.position, mRayC.forward * wdist, Color.yellow);

        if (Physics.Raycast(mRayC.position, mRayC.forward, out hit, wdist))
        {

            Debug.DrawRay(mRayC.position, mRayC.forward * wdist, Color.red);
            rStop = 6000 / hit.distance;

        }

        return rStop;


    }

}
