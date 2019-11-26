using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameObject invHolder;
    [SerializeField] public bool usingInv;
    [SerializeField] public GameObject movement;
    public static Inventory inventory;
    public int[] collectableItems = { 0, 0 };

    void Start()
    {
        inventory = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //Press I to activate inventory
        {
            //if (GameManager.gameManager.inGameFunction) return;
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
    }

    void ShowInv()
    {
        invHolder.SetActive(true);
        //SceneManager.LoadScene(Inventory);
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

    public void AddItem(string ItemID, GameObject Object)
    {
        if(ItemID == GameObject.FindWithTag("Key").ToString())
        {
            collectableItems[0]++;
        }
        //can implement a textanimation
    }
}
