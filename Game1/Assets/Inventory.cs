using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameObject invHolder;
    [SerializeField] public bool usingInv;
    [SerializeField] public GameObject movement;
    public static Inventory inventory;
    public int[] collectableItems = { 0, 0 };
    [SerializeField] private Text pickupText;
    private float timeToAppear = 2f;
    private float timeWhenDisappear;

    void Start()
    {
        inventory = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //Press I to activate inventory
        {
            usingInv = true;
            ShowInv();
            stopMovement();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && usingInv) //Press esc to cancel inventory
        {
            usingInv = false;
            HideInv();
            allowMovement();
        }

        if (pickupText.enabled && (Time.time >= timeWhenDisappear))
        {
            pickupText.enabled = false;
        }
    }

    void ShowInv()
    {
        invHolder.SetActive(true);
    }

    void HideInv() //hide inventory
    {
        invHolder.SetActive(false); //canvas state false
    }

    void allowMovement()
    {
        movement = GameObject.Find("mrs_template_mk2"); //find the game object (character)
        movement.GetComponent<CharacterMovement>().enabled = true; //allow the movement script to run
    }

    void stopMovement()
    {
        movement = GameObject.Find("mrs_template_mk2"); //find the game object (character)
        movement.GetComponent<CharacterMovement>().enabled = false; //disable the movement script
    }

    public void AddItem(string ItemID, GameObject Object) //adding item from interaction
    {
        if(ItemID == GameObject.FindWithTag("Key").ToString()) //if an item has a tag "key"
        {
            collectableItems[0]++;
        }

        textAnimation(ItemID); //calling the pickup text
    }

    void textAnimation (string ItemID) //let text appear when item has been picked with the item name
    {
        pickupText.text = "Picked up a " + ItemID;
        pickupText.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    } 
}
