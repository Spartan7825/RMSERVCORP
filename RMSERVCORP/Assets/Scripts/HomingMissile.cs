using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject startPos;
    public GameObject target;
    public float speed = 10f;
    public Animator animator;
    public PlayerController controller;

    public GameObject targetFire;

    public ParticleSystem explosion;

    public ParticleSystem water;

    public bool isHolding;
    public float scaleFire;


    public RebuildingScript rebuildscript;

    bool nextFire = true;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (isHolding) 
        {
            scaleFire = scaleFire-(Time.deltaTime);
            if (scaleFire>0) 
            {
                targetFire.transform.localScale = new Vector3(scaleFire, scaleFire, scaleFire);
            }
            else
            {
                if (animator != null)
                {

                    Destroy(targetFire);
                    isHolding = false;
                    animator.SetBool("Fire", false);
                    controller.CanMove = true;
                    rebuildscript.rebuild();
                    Destroy(GameObject.Find("Water(Clone)"));
                }
                else 
                {
                    Destroy(targetFire);
                    isHolding = false;
                    //animator.SetBool("Fire", false);
                    controller.CanMove = true;
                    rebuildscript.rebuild();
                    Destroy(GameObject.Find("Water(Clone)"));
                }

            }
        }



    }

    public void RobotFire() 
    {
        if (nextFire)
        {
            nextFire = false;
            StartCoroutine(DelayFire(1));

            if (target)
            {

                if (!(Vector3.Distance(target.transform.position, transform.position) > 50f))
                {

                    //transform.LookAt(target.transform);

                    var relativePoint = transform.InverseTransformPoint(target.transform.position);


                    if (target.GetComponent<Renderer>().isVisible)
                    {
                        if (relativePoint.x > -15 && relativePoint.x < 15)
                        {
                            animator.SetBool("Fire", true);
                            controller.CanMove = false;
                            StartCoroutine(DelayRobot(1));
                        }

                    }
                    else
                    {
                        Debug.Log("not in vision");
                    }


                }
            }
        }
    }


    IEnumerator DelayFire(int time)
    {

        yield return new WaitForSeconds(time);
        nextFire = true;
    }


    public void TruckFire()
    {
        if (nextFire)
        {
            nextFire = false;
            StartCoroutine(DelayFire(1));
            if (target)
            {

                if (!(Vector3.Distance(target.transform.position, transform.position) > 50f))
                {


                    var relativePoint = transform.InverseTransformPoint(target.transform.position);


                    if (target.GetComponent<Renderer>().isVisible)
                    {
                        if (relativePoint.x > -15 && relativePoint.x < 15)
                        {
                            StartCoroutine(DelayTruck(1));
                        }

                    }
                    else
                    {
                        Debug.Log("not in vision");
                    }


                }
            }
        }
    }

    public void ExtinguishRobot() 
    {

        
                if (!(Vector3.Distance(targetFire.transform.position, transform.position) > 30f))
                {
                    Debug.Log("here");
                    var relativePoint = transform.InverseTransformPoint(targetFire.transform.position);


                    if (targetFire.GetComponent<Renderer>().isVisible)
                    {
                        if (relativePoint.x > -50 && relativePoint.x < 50)
                        {
                            isHolding = true;
                            animator.SetBool("Fire", true);
                            controller.CanMove = false;
                            //transform.LookAt(targetFire.transform);
                            Instantiate(water, startPos.transform.position, transform.rotation);
                        }

                    }
                    else
                    {
                        Debug.Log("not in vision");
                    }



                }

    }
    public void NotExtinguishRobot()
    {
        isHolding = false;
        animator.SetBool("Fire", false);
        controller.CanMove = true;
        Destroy(GameObject.Find("Water(Clone)"));
        //transform.LookAt(targetFire.transform);

    }
    public void ExtinguishTruck()
    {
        if (!(Vector3.Distance(targetFire.transform.position, transform.position) > 30f))
        {
            var relativePoint = transform.InverseTransformPoint(targetFire.transform.position);


            if (targetFire.GetComponent<Renderer>().isVisible)
            {
                Debug.Log(relativePoint.x);
                if (relativePoint.x > -50 && relativePoint.x < 50)
                {
                    isHolding = true;
                    //animator.SetBool("Fire", true);
                    //controller.CanMove = false;
                    //transform.LookAt(targetFire.transform);
                    Instantiate(water, startPos.transform.position, transform.rotation);
                }
                else
                {
                    Debug.Log("can't see!!!:)");
                }

            }
            else
            {
                Debug.Log("not in vision");
            }


        }

    }
    public void NotExtinguishTruck()
    {

        isHolding = false;
        //animator.SetBool("Fire", false);
        //controller.CanMove = true;
        Destroy(GameObject.Find("Water(Clone)"));
       

    }

    public IEnumerator launch(GameObject rocket)
    {
        while (Vector3.Distance(target.transform.position,rocket.transform.position) > 1.0f)

        {
            rocket.transform.position += (target.transform.position - rocket.transform.position).normalized * speed * Time.deltaTime;
            rocket.transform.LookAt(target.transform);
            yield return null;
        
        }
        Instantiate(explosion, rocket.transform.position, rocket.transform.rotation);
        Destroy(rocket);
        target.transform.parent.GetComponent<Damage>().npcDamage(10);
        //Destroy(target.transform.parent.gameObject);
    
    }

    IEnumerator DelayRobot(int time)
    {

        yield return new WaitForSeconds(time);
        animator.SetBool("Fire", false);

        GameObject rocket = Instantiate(rocketPrefab, startPos.transform.position, Quaternion.identity);
        //rocket.transform.LookAt(target.transform);
        controller.CanMove = true;
        StartCoroutine(launch(rocket));
    }
    IEnumerator DelayTruck(int time)
    {

        yield return new WaitForSeconds(time);
        // animator.SetBool("Fire", false);

        GameObject rocket = Instantiate(rocketPrefab, startPos.transform.position, Quaternion.identity);
        //rocket.transform.LookAt(target.transform);
        controller.CanMove = true;
        StartCoroutine(launch(rocket));
    }


}
