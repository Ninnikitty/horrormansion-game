using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButtonAdd : MonoBehaviour
{
    public Button numButton;

    public Text code;

    public string addNumber;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if(KeyPadSystem.maxNumbers < 6) 
        {
            code.text += addNumber; //adding numbers
            KeyPadSystem.maxNumbers++;
        }
    } 
}
