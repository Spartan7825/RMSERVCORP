using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsPresetController : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public void OnValueChanged() 
    {
        if (dropdown.options[dropdown.value].text == "Low")
        {
            QualitySettings.SetQualityLevel(0);
        }
        else if (dropdown.options[dropdown.value].text == "Medium") 
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (dropdown.options[dropdown.value].text == "Ultra")
        {
            QualitySettings.SetQualityLevel(2);
        }
        
    }

}
