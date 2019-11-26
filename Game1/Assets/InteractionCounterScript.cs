using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCounterScript : MonoBehaviour
{
    public Camera camera;
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
        InvokeRepeating("search", 0f, 0.5f);
        data_text_key.text = data_amount_key.ToString();
    }

    void Update()
    {
       // Ray ray_Cast = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
       // RaycastHit ray_Hit;

       /* if(Physics.Raycast(ray_Cast, out ray_Hit, ray_Range))
        {
            if (ray_Hit.collider.tag == "Key")
            { */
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(hit.collider.tag == "Key") //interactingObjectName ==)//GameObject.FindWithTag("Key").ToString())
                    {
                        Debug.Log("I tried to pick up a key");
                        //Destroy(ray_Hit.collider.gameObject);

                        data_amount_key++;
                        data_text_key.text = data_amount_key.ToString();

                        Inventory.inventory.AddItem(interactingObjectName, interactingGameObject);
                clearData();
                return;
                    }
                }
            //}
       // }
    }

    void search ()
    {
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && hit.distance <= interactDistance)
        {
            resetData();
            interact = true;

            interactingObjectName = hit.collider.tag;
            interactingGameObject = hit.transform.gameObject;

           // interactingGameObject.GetComponen<Renderer>().material.color 
        }
        else
        {
            interact = false;
            resetData();
        }
    }

    void resetData()
    {
        if (interactingGameObject == null) return;

        interactingGameObject = null;
        interactingObjectName = null;
    }

    void clearData()
    {
        if (interactingGameObject != null)
        {
            Destroy(interactingGameObject);
        }
        interactingGameObject = null;
    } 
}
