using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorToggle : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void toggleDoor()
    {
        animator.SetBool("isDoorOpen", !(animator.GetBool("isDoorOpen")));
        Debug.Log("Opened or closed door");
    }
}
