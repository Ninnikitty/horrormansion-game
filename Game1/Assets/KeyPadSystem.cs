using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadSystem : MonoBehaviour //inside of keypadcanvas

{
    public static int maxNumbers = 0; // max count of numbers you can type

    public GameObject keypad; //insert canvas here
    public Text code; //insert code text here

    GameObject camObj; //to stop the camera

    void Update()
    {
        camObj = GameObject.Find("1stPCamera");

        try
        {
            if (NumOK.numok.rightCode) //if the numcode is correct 
        {
                keypad.SetActive(false);
                Cursor.visible = false;
                Time.timeScale = 1f;
                camObj.GetComponent<cameraController>().enabled = true;

                Inventory.inventory.AddItem("lobby key", GameObject.Find("lobby key")); //adding the lobby key to the user inventory
                InteractionCounterScript.interactioncounterscript.hit.transform.SetParent(GameObject.Find("ItemsDB").transform);
                InteractionCounterScript.interactioncounterscript.AddToInventory(GameObject.Find("lobby key").transform);
                Destroy(GameObject.Find("lobby key")); //destroy the object from the world
                Debug.Log("Lobby key obtained");

                InteractionCounterScript.interactioncounterscript.GotLobbyKey = true; //needed for the door: inform that the player has the lobby key
            }

        if (Input.GetKeyDown(KeyCode.Escape) && InteractionCounterScript.interactioncounterscript.usingKeyPad) //if we press esc and the keypad is on, its cancelled
            {
                keypad.SetActive(false);
                code.text = "";
                maxNumbers = 0;

                Cursor.visible = false;
                Time.timeScale = 1f;
                camObj.GetComponent<cameraController>().enabled = true;
            }
        }
        catch
        {
            Debug.Log("Error msg from keypadsystem, numlock part. key has already been taken");
        }
    }
}
