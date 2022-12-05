using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
   



    public void Start() 
    {
        //  target = target.transform.position + new Vector3(0, 0, 10);



    }
    private void Update()
    {

/*        if (Input.GetKeyDown("a"))
        {
            Camera.main.GetComponent<Animation>().Stop("IntroCinematic");
         
        }
        if (Input.touchCount > 0)
        {
            Camera.main.GetComponent<Animation>().Stop("IntroCinematic");
        }*/
    }
    private void FixedUpdate()
    {
        //transform.position = target.transform.position + Quaternion.AngleAxis
        if (MainMenu.instance.GetCurrentLevel() < 990)
        {
            HandleTranslation();
            HandleRotation();
        }
    }


    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        //var targetPosition = target.transform.position+ offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        //var rotation = target.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }

}
