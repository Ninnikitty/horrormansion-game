using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumOK : MonoBehaviour
{
    public static NumOK numok;

    public Button numButton;

    public Text code;

    public bool rightCode;

    void Start()
    {
        numok = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (code.text == "024789") //if the code is correct
        {
            //what happens
            Debug.Log("Correct");
            rightCode = true;
        }
        else
        {
            rightCode = false;
            code.text = "";
            KeyPadSystem.maxNumbers = 0;
            Debug.Log("Incorrect");
        }
    }
}
