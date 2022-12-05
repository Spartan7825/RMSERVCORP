using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siren1 : MonoBehaviour
{
    [SerializeField] Light light1;
    [SerializeField] Light light2;

    private Vector3 light1Temp;
    private Vector3 light2Temp;

    [SerializeField] int speed;

    public Material mat;
    public bool enabled;
    public bool toggle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (toggle)
            {
                mat.SetColor("_Color", Color.red);
                mat.EnableKeyword("_EMISSION");
                enabled = false;
                toggle = false;
            }
            else
            {
                mat.SetColor("_Color", Color.red);
                mat.EnableKeyword("_EMISSION");
                enabled = false;
                toggle = true;
            }

        }
        else
        {
            if (toggle)
            {
                mat.DisableKeyword("_EMISSION");
                enabled = true;
                toggle = false;
            }
            else
            {

                mat.DisableKeyword("_EMISSION");
                enabled = true;
                toggle = true;

            }

        }


        light1Temp.y += speed * Time.deltaTime;
        light2Temp.y -= speed * Time.deltaTime;

        light1.transform.eulerAngles = light1Temp;
        light2.transform.eulerAngles = light2Temp;

    }
}
