using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyColorController : MonoBehaviour
{

    public Color lambert1;

    public Material lambert1M;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMaterial() {

        lambert1M.SetColor("_Color", lambert1);
    }
}
