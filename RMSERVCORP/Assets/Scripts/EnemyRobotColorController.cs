using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotColorController : MonoBehaviour
{

    public Color eyes;
    public Color face;
    public Color mainbody;
    public Color shin;

    public Material eyesM;
    public Material faceM;
    public Material mainbodyM;
    public Material shinM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMaterial() {

        eyesM.SetColor("_Color", eyes);
        faceM.SetColor("_Color", face);
        mainbodyM.SetColor("_Color", mainbody);
        shinM.SetColor("_Color", shin);

    }
}
