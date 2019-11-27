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

    [SerializeField] public GameObject itemsDB;
    [SerializeField] public GameObject inventorySlots;
    public RawImage[] slots;

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
                        hit.transform.SetParent(itemsDB.transform);
                        AddToInventory(hit.transform);

                        clearData(); //deleting the item from scene
                        return;
                    }
                }

                useKey(hit.transform);
    }

    void search ()
    {
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && hit.distance <= interactDistance) //cameras distance and ray from interactable items
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

    void clearData() //if there is an item (in cameras view), destroy the item (taking)
    {
        if (interactingGameObject != null)
        {
             Destroy(interactingGameObject);
            //interactingGameObject.SetActive(false);
        }
        interactingGameObject = null;
    } 

    public void useKey(Transform item) //function to use key (deletes a key one by one atm)
    {
        if(data_amount_key >= 1 && data_amount_key <= 10) //cant go below 0
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("use");
                data_amount_key--;
                data_text_key.text = data_amount_key.ToString();
                //invText.text = data_amount_key.ToString();
                //Inventory.inventory.pickupText.text = interactingObjectName + "has been used";

                /* if (data_amount_key == 0)
                 {
                     Inventory.inventory.slotImage.enabled = false; //icon and number goes away from inventory
                     invText.enabled = false;

                 } */
                for (int i = 0; i < slots.Length; i++)
                {
                    if (transform.childCount > 0)
                    {
                        //slots = inventorySlots.GetComponentsInChildren<RawImage>();
                        //slots[i].transform.GetChild(i).gameObject.SetActive(false);
                        // Destroy(slots[i].transform.GetChild(0).gameObject);
                        }
                } 

                }
        }
    }

    public void AddToInventory(Transform item)
    {
        Debug.Log("Adding to inv..");
        slots = inventorySlots.GetComponentsInChildren<RawImage>();

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].texture == null)
            {
                slots[i].texture = item.GetComponent<RawImage>().texture;
                return;
            }
        }
    }
}
