using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount-1; i++) 
        {
            Debug.Log(i);
            transform.GetChild(i).gameObject.GetComponent<Button>().interactable=SaveManager.instance.levelsUnlocked[i];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
