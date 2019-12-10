using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCounterScript : MonoBehaviour
{
    public Camera camera; //the players camera that faces the objects
    public float interactDistance = 15f;
    public GameObject interactingGameObject;
    public string interactingObjectName;
    public bool interact;
    RaycastHit hit;

    [SerializeField] public GameObject itemsDB; //place itemsdb object here
    [SerializeField] public GameObject inventorySlots; //place invitemspace from inventory canvas here
    public RawImage[] slots;

    [SerializeField] public GameObject paperTextHolder; //place canvasforpapertext here
    [SerializeField] public bool usingPaperText;
    public Text PaperText; //papertext from canvasforpapertext

    public Text pressEText; //text to let the player know they can interact by pressing e (or any other button). this text object is inside counterui canvas
    private float timeToAppear = 0.001f;
    private float timeWhenDisappear;

    GameObject camObj;

    CharacterController charCtrl;

    [Header("Data")]
    public int data_amount_key = 0;
    public Text data_text_key; //place textcounter from counterui canvas here

    public static bool GameIsPaused;

    void Start()
    {
        InvokeRepeating("search", 0f, 0.5f); //item search
        data_text_key.text = data_amount_key.ToString(); //number text matches the amount of keys

        camObj = GameObject.Find("1stPCamera");

    }

    void Update()
    {
        try //catch the no object errors w try catch
        {
            if (Input.GetKeyDown(KeyCode.E)) //if E is pressed, item will be picked
            {
                if (hit.collider.tag == "Key") //if the hit collider has a tag "key"
                {
                    Debug.Log("I tried to pick up a " + interactingObjectName);

                    data_amount_key++; //key added, number goes up
                    data_text_key.text = data_amount_key.ToString();

                    Inventory.inventory.AddItem(interactingObjectName, interactingGameObject);
                    hit.transform.SetParent(itemsDB.transform);
                    AddToInventory(hit.transform);

                    clearData(); //deleting the item from scene
                    return;
                }

                if (hit.collider.tag == "Item") //if the object has a tag "item" -> can be instered to any game object. remember to add box colliders (2), one has the trigger option and is sized correctly and one keeps the gravity for the object. then add a raw image and the texture = icon of the item. shows up in the inventory
                {
                    Debug.Log("I tried to pick up a " + interactingGameObject.ToString()); //gives u the whole object name
                    Inventory.inventory.AddItem(interactingObjectName, interactingGameObject);
                    hit.transform.SetParent(itemsDB.transform);
                    AddToInventory(hit.transform);

                    clearData(); //deleting the item from scene
                    return;
                }

                if (hit.collider.tag == "door") //door interaction
                {
                    doorToggle doorSc = interactingGameObject.GetComponent<doorToggle>();
                    doorSc.toggleDoor();
                }

            }
            //remember to add text component (hidden) to the paper object! and write the text you want to appear
            if (hit.collider.tag == "paper") //also remember to set a triggered box collider to the paper object. make sure the character's interaction camera can see the paper collider from close distance
            {
                pressEText.enabled = true;
                pressEText.text = "press E to interact"; //let the player know they can interact with this object
                timeWhenDisappear = Time.time + timeToAppear;

                if (Input.GetKeyDown(KeyCode.E)) //press e to interact
                {
                      pressEText.enabled = false;
                      usingPaperText = true;
                      paperTextHolder.SetActive(true); //set the paper canvas active
                      PaperText.text = interactingGameObject.GetComponent<Text>().text.ToString(); //gets the text component from the object you're interacting with and shows it
                      PaperText.enabled = true;  //text of the paper
                      Time.timeScale = 0f; // <- if we want to stop the character. camera needs a separated stop function
                      camObj.GetComponent<cameraController>().enabled = false; //no camera movement allowed
                }
                else if (Input.GetKeyDown(KeyCode.Escape) && usingPaperText) // esct -> get away from paper screen
                {
                    usingPaperText = false;
                    paperTextHolder.SetActive(false);
                    PaperText.enabled = false;
                    Time.timeScale = 1f;
                    camObj.GetComponent<cameraController>().enabled = true;
                }
            }

            if (pressEText.enabled && (Time.time >= timeWhenDisappear)) //pickup text disappearance time
            {
                pressEText.enabled = false;
            }

        } catch 
        {
            Debug.Log("No object tagged");
        }


        useKey(hit.transform);
     emptyInv();
    }

    void search ()
    {
        //charCtrl = GetComponent<CharacterController>(); //testing
        //Vector3 p1 = camera.transform.position; //testing
        //Vector3 p2 = camera.transform.forward; //testing

        if(Physics.Raycast(camera.transform.position, transform.InverseTransformDirection(camera.transform.forward), out hit) && hit.distance <= interactDistance) //cameras distance and ray from interactable items
        //if(Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, 5))
        //if (Physics.Raycast(p1, p2, out hit) && hit.distance <= interactDistance)
        {
            //hit.distance = interactDistance;
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
                emptyInv();

                slots = inventorySlots.GetComponentsInChildren<RawImage>();
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].texture = null; //now it deletes all the rawimages from inventory (items picked)  
                }
            }
        }
    }
    
    public void emptyInv() //deletes all items from inventory. implement a method that lets u choose the item to choose Or let the game events take the specific item
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            slots = inventorySlots.GetComponentsInChildren<RawImage>();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].texture = null; //now it deletes all the rawimages from inventory (items picked)  
            }
        }
    }

    public void AddToInventory(Transform item)
    {
        Debug.Log("Adding to inv..");
        slots = inventorySlots.GetComponentsInChildren<RawImage>(); //slots match the rawimages from slot objects

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].texture == null)
            {
                slots[i].texture = item.GetComponent<RawImage>().texture; //replacing slot texture with the texture of the picked object (icon)
                return;
            }
        }
    }
}
