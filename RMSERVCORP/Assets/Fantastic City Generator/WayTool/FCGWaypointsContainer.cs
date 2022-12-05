
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FCGWaypointsContainer : MonoBehaviour {

    //[HideInInspector]        
    public List<Transform> waypoints = new List<Transform>();

    //[HideInInspector]
    public GameObject[] nextWay;

    
    private bool inLeft = false;

    public Transform[] tf01;

    ArrayList arr = new ArrayList();
    GameObject tf01IDX;

    private void Awake()
    {
         
        inLeft = GameObject.Find("RoadMarkRev");
        
        tf01IDX = GameObject.Find("traffic-idx");
        
        if (!tf01IDX) {
            tf01IDX = new GameObject("traffic-idx");
            tf01IDX.AddComponent<Tf01>();
        } 

        if (tf01IDX)
        tf01 = tf01IDX.GetComponent<Tf01>().getTF01();
       

        for (int i = 0; i < waypoints.Count; i++)
        if (i < waypoints.Count - 1)
            waypoints[i].LookAt(waypoints[i + 1]);
        else
        {
            waypoints[i].rotation = Quaternion.LookRotation(waypoints[i].position - waypoints[i - 1].position);
            NextWays(waypoints[i]);
        }

        
    }


 

    

    private void NextWays(Transform referencia)
    {

        int n = tf01.Length;

        if (n < 1) return;

        //ArrayList arr = new ArrayList();

        arr.Clear();

        for (int i = 0; i < n; i++)
        {

            float distancia = Vector3.Distance(referencia.position, tf01[i].position);

                
            if (distancia < 35f && distancia > 8f)
            {
                
                float a = GetAngulo(referencia, tf01[i]);

                if (!inLeft && (a > 340 || a < 80) || inLeft && (a > 280 || a < 20))
                {
                    arr.Add(tf01[i]);
                }
                
            }



        }


        //referencia.transform.parent.gameObject.GetComponent<FCGWaypointsContainer>().AddNewWays( arr);

        
        //AddNewWays( arr);

        

        int qt = arr.Count;

        nextWay = new GameObject[qt];

        if (qt < 1) return;

        Transform stemp;

        for (int i = 0; i < qt; i++)
        {

            stemp = (Transform)arr[i];
            nextWay[i] = stemp.parent.gameObject;
                        
        }


    }



    private void AddNewWays( ArrayList arr)
    {
        Transform stemp;

        nextWay = new GameObject[arr.Count];

        for (int i = 0; i < arr.Count; i++)
        {

                stemp = (Transform)arr[i];
                nextWay[i] = stemp.parent.gameObject;
            

        }



    }

    private float GetAngulo(Transform origem , Transform target)
    {
        float r;

        GameObject compass = new GameObject("Compass");
        compass.transform.parent = origem;
        compass.transform.localPosition = new Vector3(0, 0, 0);

        compass.transform.LookAt(target);
        r = compass.transform.localEulerAngles.y;

        Destroy(compass);
        return r;

    }

    public void InvertNodesDirection()
    {
        Vector3 wtemp = new Vector3(0, 0, 0);

        int nn = Mathf.CeilToInt(waypoints.Count / 2);

        for (int i = 0; i < nn; i++)
        {
            wtemp = waypoints[i].position;
            waypoints[i].position = waypoints[waypoints.Count - i - 1].position;
            waypoints[waypoints.Count - i - 1].position = wtemp;
        }

    }

    void OnDrawGizmos() {

        if (waypoints.Count < 1) return;

        for (int i = 0; i < waypoints.Count; i ++){

            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);

            if (waypoints.Count < 1) return;

            Gizmos.DrawSphere(waypoints[i].transform.position, 1f);
			Gizmos.DrawWireSphere (waypoints[i].transform.position, 2f);

            if (waypoints.Count < 2) return;

            if (i < waypoints.Count - 1){
				if(waypoints[i] && waypoints[i+1]){
                    
					if (waypoints.Count > 0) {

                        if (i < waypoints.Count - 1)
                        {
                            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                            waypoints[i].LookAt(waypoints[i + 1]);
                           
                        } 

					}
				}
			}
            else if (i == waypoints.Count - 1)
            {
                waypoints[i].rotation = waypoints[i - 1].rotation; 
            }

        }
		
	}
 
    
}
