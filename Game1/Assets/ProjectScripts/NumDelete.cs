using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumDelete : MonoBehaviour
{
    public Button numButton;

    public Text code;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
       code.text = "";
       KeyPadSystem.maxNumbers = 0;
    }
}
