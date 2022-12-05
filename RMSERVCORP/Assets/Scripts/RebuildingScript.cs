using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebuildingScript : MonoBehaviour
{
    public GameObject orignalPrefab;
    public GameObject fractureObject;



    public void rebuild()
    {
        orignalPrefab.SetActive(true);
        fractureObject.SetActive(false);
    }
}
