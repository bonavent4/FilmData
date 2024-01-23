using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField inputfield;
    [SerializeField] string infoName;
    [SerializeField] List<string> Options = new List<string>();
    TMP_Dropdown dropDown;
    private void Start()
    {
        dropDown = gameObject.GetComponent<TMP_Dropdown>();
        dropDown.AddOptions(Options);
    }
    public void addOption()
    {
        if(dropDown.value != 0)
        {
            inputfield.text += " " + infoName + ":" + Options[dropDown.value] + " ";
        }
        dropDown.value = 0;
        
    }
}
