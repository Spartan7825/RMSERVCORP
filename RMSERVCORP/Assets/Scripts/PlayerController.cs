using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    public Animator anim;
  
    public LayerMask layerMask;
    public Joystick joystick;
    public float m_Speed;
    public float rotation_speed;

    public bool CanMove=true;
    public Rigidbody r;

    public void Start()
    {
        r.GetComponent<Rigidbody>();
        
    }

    public void Update()
    {
        Quaternion originalRot = transform.rotation;
        r.centerOfMass = new Vector3(0, 0, 0);
        transform.rotation = originalRot * Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = originalRot * Quaternion.AngleAxis(0, Vector3.right);

        if (MainMenu.instance.GetCurrentLevel() < 990)
        {

        }
        else
        {

            if (photonView.IsMine)
            {
                transform.GetChild(31).gameObject.SetActive(false);
            }
            else { transform.GetChild(31).gameObject.SetActive(true); }
        }

    }

    private void FixedUpdate() 
    {
        if (MainMenu.instance.GetCurrentLevel() < 990)
        {

            moveSingle();

        }
        else
        {
            if (photonView.IsMine)
            {
                Move();
            }

        }
    }
    private void moveSingle()
    {

        if (CanMove)
        {
            float horizontalAxis = joystick.Horizontal;
            float verticalAxis = joystick.Vertical;
            //float verticalAxis = Input.GetAxis("Vertical");
            //float horizontalAxis = Input.GetAxis("Horizontal");

            /*
            Vector3 movement = this.transform.forward * verticalAxis;  // + this.transform.right * horizontalAxis;
            movement.Normalize();

            this.transform.position += movement * 0.04f;
            */
            float translation = verticalAxis * m_Speed * Time.deltaTime;
            float rotation = horizontalAxis * rotation_speed
                * Time.deltaTime;

            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);

            this.anim.SetFloat("Vertical", verticalAxis);
            this.anim.SetFloat("Horizontal", horizontalAxis);

        }

    }

    private void Move()
    {
        if (CanMove)
        {
            float horizontalAxis = joystick.Horizontal;
            float verticalAxis = joystick.Vertical;
            //float verticalAxis = Input.GetAxis("Vertical");
            //float horizontalAxis = Input.GetAxis("Horizontal");

            
            Vector3 movement = this.transform.forward * verticalAxis + this.transform.right * horizontalAxis;
            movement.Normalize();

            this.transform.position += movement * 0.04f;
            
       //     float translation = verticalAxis * m_Speed * Time.deltaTime;
        //    float rotation = horizontalAxis * rotation_speed
          //      * Time.deltaTime;

         //   transform.Translate(0, 0, translation);
            //transform.Rotate(0, rotation, 0);
          //  transform.Translate(rotation, 0, 0);

            this.anim.SetFloat("Vertical", verticalAxis);
            this.anim.SetFloat("Horizontal", horizontalAxis);

        }
    }

}
