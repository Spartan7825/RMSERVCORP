using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTruckColorController : MonoBehaviour
{

    public Color kaba;
    public Color kabad;
    public Color tank;

    public Material kabaM;
    public Material kabadM;
    public Material tankM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMaterial() {

        kabaM.SetColor("_Color", kaba);
        kabadM.SetColor("_Color", kabad);
        tankM.SetColor("_Color", tank);
    }
}
