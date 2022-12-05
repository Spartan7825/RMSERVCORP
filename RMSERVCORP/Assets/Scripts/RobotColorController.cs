using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotColorController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(SaveManager.instance.currentCar).GetComponent<BuddyColorController>().changeMaterial();
        transform.GetChild(SaveManager.instance.currentCar).gameObject.transform.GetChild(30).GetComponent<FTruckColorController>().changeMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {

    }


    public void changingColors() 
    {
        transform.GetChild(SaveManager.instance.currentCar).GetComponent<BuddyColorController>().changeMaterial();
    }
}
