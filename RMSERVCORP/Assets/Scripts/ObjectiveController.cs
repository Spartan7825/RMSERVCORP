using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{

    public Transform truck;
    public Transform robot;
    public Transform player;
    public GameObject objective1;
    public GameObject objective2;
    public Vector3 startPosObjective1;
    public Vector3 startPosObjective2;

    //public Transformation trans;

    // Start is called before the first frame update
    void Start()
    {
        player = truck;
        startPosObjective1 = objective1.transform.position;
        startPosObjective2 = objective2.transform.position;

    }


    // Update is called once per frame
    void Update()
    {
        
        if (Transformation.instance.GetIsTruck())
        {
            player = truck;
           
            //Debug.Log("Player is Truck");
        }
        else if(!Transformation.instance.GetIsTruck())
        {
            player = robot;
            

            //Debug.Log("Player is Robot");
        }

        if (objective1 != null)
        {
            if (Vector3.Distance(player.transform.position, objective1.transform.position) > 200)
            {
                //Debug.Log("here");
                Vector3 pos = startPosObjective1;
                Vector3 dir = (player.transform.position - startPosObjective1).normalized;
                 //Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);



                Vector3 newPosition = dir + pos;

                newPosition.y = objective1.transform.position.y;

              

                objective1.transform.position = player.transform.position-dir*200;




            }
            else
            {
                if (Vector3.Distance(player.transform.position, startPosObjective1) > 190)
                {
                    if (Vector3.Distance(player.transform.position, objective1.transform.position) < 190)
                    {

                        Vector3 pos = startPosObjective1;
                        Vector3 dir = (player.transform.position - startPosObjective1).normalized;

                       // Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);


                        Vector3 newPosition = -dir + pos;

                        newPosition.y = objective1.transform.position.y;


                        objective1.transform.position = newPosition;
                    }


                }

            }

        }

        if (objective2 != null)
        {
            if (Vector3.Distance(player.transform.position, objective2.transform.position) > 200)
            {

                Vector3 pos = startPosObjective2;
                Vector3 dir = (player.transform.position - startPosObjective2).normalized;




                Vector3 newPosition = dir + pos;

                newPosition.y = objective2.transform.position.y;


                objective2.transform.position = player.transform.position - dir * 200;




            }
            else
            {
                if (Vector3.Distance(player.transform.position, startPosObjective2) > 190)
                {
                    if (Vector3.Distance(player.transform.position, objective2.transform.position) < 190)
                    {

                        Vector3 pos = startPosObjective2;
                        Vector3 dir = (player.transform.position - startPosObjective2).normalized;




                        Vector3 newPosition = -dir + pos;

                        newPosition.y = objective2.transform.position.y;


                        objective2.transform.position = newPosition;
                    }


                }




            }
        }

    }
}
