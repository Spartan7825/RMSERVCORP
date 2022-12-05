using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tf01 : MonoBehaviour {

    //[HideInInspector]
    public Transform[] tf01;

    private bool tested = false;

    public Transform[] getTF01()
    {

        if (!tested)
            tf01 = GameObject.FindObjectsOfType(typeof(Transform)).Select(g => g as Transform).Where(g => g.name.Equals("TF-01")).ToArray();

        tested = true;

        return tf01;

    }

}
