using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAdd : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("no money :(");
            SaveManager.instance.money += 1000;
            SaveManager.instance.Save();
        } 
        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveManager.instance.money -= 100;
            SaveManager.instance.Save();
        }         
    }
}
