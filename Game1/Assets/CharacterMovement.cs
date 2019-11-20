using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour{

    float speed = 0;
    float rotSpeed = 80;
    float gravity = 10;
    float rot = 0;
    bool crouched = false;

    float crouchSpeed = 1;
    float walkSpeed = 3;
    float sprintSpeed = 6;
    float walkSpeedBack = -2;
    float crouchSpeedBack = -1;

    KeyCode forward = KeyCode.W;
    KeyCode backwards = KeyCode.S;
    KeyCode sprint = KeyCode.LeftShift;
    KeyCode crouch = KeyCode.LeftControl;

    Vector3 moveDirection = Vector3.zero;

    CharacterController ctrl;
    Animator animator;
    // Start is called before the first frame update
    void Start(){
        ctrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        crouched = animator.GetBool("crouched");
    }

    // Update is called once per frame
    void Update(){

        //Check if crouch is pressed and toggle crouch
        if (Input.GetKey(crouch))
        {
            animator.SetBool("crouched", !animator.GetBool("crouched"));
            crouched = animator.GetBool("crouched");
            if (crouched)
            {
                animator.SetInteger("playerAnimState", 3);
            }
        }

        if (Input.GetKey(forward))
        {
            if (Input.GetKey(sprint) && !crouched){
                animator.SetInteger("playerAnimState", 2);
                speed = sprintSpeed;
            }
            else if (!crouched) {
                speed = walkSpeed;
                animator.SetInteger("playerAnimState", 1);
            }
            else if (crouched)
            {
                speed = crouchSpeed;
                animator.SetInteger("playerAnimState", 6);
            }
        }
        else if (Input.GetKey(backwards))
        {
            if (!crouched)
            {
                animator.SetInteger("playerAnimState", 1);
                speed = walkSpeedBack;
            }
            else if (crouched)
            {
                animator.SetInteger("playerAnimState", 6);
                speed = crouchSpeedBack;
            }
        }
        //After checking for position and setting speed and animation, move the character
        moveDirection = new Vector3(0, 0, 1);
        moveDirection *= speed;
        moveDirection = transform.TransformDirection(moveDirection);

        //if not holding forward or backwards stay still
        if (Input.GetKeyUp(forward))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        if(Input.GetKeyUp(backwards))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        ctrl.Move(moveDirection * Time.deltaTime);
    }
}
