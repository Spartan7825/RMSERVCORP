using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class TouchLook : MonoBehaviourPunCallbacks
{

    public float mouseSensitivity = 100f;
   

    float xRotation;

    public Animator anim;

    public LayerMask layerMask;
    public Joystick joystick;
    public float m_Speed;
    public float rotation_speed;


    protected float cameraAngleY;
    protected float cameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    public FixedTouchField touchField;

    public Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float mouseX = 0;
            float mouseY = 0;

            if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                mouseX = Input.GetTouch(0).deltaPosition.x;
                mouseY = Input.GetTouch(0).deltaPosition.y;

            }

            /*
                            *//*
                             mouseX *= mouseSensitivity;
                             mouseY *= mouseSensitivity;

                             xRotation -= mouseY * Time.deltaTime;
                             xRotation = Mathf.Clamp(xRotation, -80, 80);

                             transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

                             transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
                            *//*

                           // float translation = mouseY * m_Speed * Time.deltaTime;
                            float rotation = mouseX * rotation_speed
                                * Time.deltaTime;

                          //  transform.Translate(0, 0, translation);
                           transform.Rotate(0, rotation, 0);

                           // this.anim.SetFloat("Vertical", mouseY);
                           // this.anim.SetFloat("Horizontal", mouseX);
                    */
            var input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            var vel = Quaternion.AngleAxis(cameraAngleY + 180, Vector3.up) * input * 5f;

            r.velocity = new Vector3(vel.x, r.velocity.y, vel.z);


            transform.rotation = Quaternion.AngleAxis(cameraAngleY + 180 + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);


            cameraAngleY += touchField.TouchDist.x * cameraAngleSpeed;
            CameraPosY = Mathf.Clamp(CameraPosY - touchField.TouchDist.y * CameraPosSpeed, 1f, 6f);
            Camera.main.transform.position = transform.position + Quaternion.AngleAxis(cameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, 4);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 4.5f - Camera.main.transform.position, Vector3.up);
        }
    }
}
