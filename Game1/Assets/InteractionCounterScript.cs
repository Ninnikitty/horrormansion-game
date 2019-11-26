using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCounterScript : MonoBehaviour
{
    public Camera camera; //the players camera that faces the objects
    public float interactDistance = 300f;
    public GameObject interactingGameObject;
    public string interactingObjectName;
    public bool interact;
    RaycastHit hit;

    [Header("Data")]
    public int data_amount_key = 0;
    public Text data_text_key;

    void Start()
    {
        InvokeRepeating("search", 0f, 0.5f); //item search
        data_text_key.text = data_amount_key.ToString(); //number text matches the amount of keys
    }

    void Update()
    {
                if (Input.GetKeyDown(KeyCode.E)) //if E is pressed, item will be picked
                {
                    if(hit.collider.tag == "Key") //if the hit collider has a tag "key"
                    {
                        Debug.Log("I tried to pick up a key");

                        data_amount_key++; //key added, number goes up
                        data_text_key.text = data_amount_key.ToString();

                        Inventory.inventory.AddItem(interactingObjectName, interactingGameObject);
                        clearData();
                        return;
                    }
                }
    }

    void search ()
    {
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && hit.distance <= interactDistance)
        {
            resetData();
            interact = true; 

            interactingObjectName = hit.collider.tag; //object name = tag the object has
            interactingGameObject = hit.transform.gameObject; //object = the object the player cam is facing
        }
        else
        {
            interact = false;
            resetData();
        }
    }

    void resetData() //if theres no item, reset
    {
        if (interactingGameObject == null) return;

        interactingGameObject = null;
        interactingObjectName = null;
    }

    void clearData() //if there is an item, destroy the item (taking)
    {
        if (interactingGameObject != null)
        {
            Destroy(interactingGameObject);
        }
        interactingGameObject = null;
    } 
}
