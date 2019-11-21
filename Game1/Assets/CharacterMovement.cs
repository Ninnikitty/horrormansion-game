using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour{

    float speed = 0;
    float rotSpeed = 80;
    float gravity = 600;
    float rot = 0;

    float walkSpeed = 2;
    float sprintSpeed = 4;
    float walkSpeedBack = -1;
    float timeTillIdleDefault = 10;
    float timeTillIdle;

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
        timeTillIdle = timeTillIdleDefault;
    }

    // Update is called once per frame
    void Update(){

        timeTillIdle -= Time.deltaTime;

        if (Input.GetKey(forward))
        {
            if (Input.GetKey(sprint)){
                animator.SetInteger("playerAnimState", 2);
                speed = sprintSpeed;
            }
            else{
                speed = walkSpeed;
                animator.SetInteger("playerAnimState", 1);
            }
        }
        else if (Input.GetKey(backwards))
        {
            animator.SetInteger("playerAnimState", 1);
            speed = walkSpeedBack;
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
        if (timeTillIdle < 0)
        {
            animator.SetInteger("playerAnimState", - 1);
            timeTillIdle = timeTillIdleDefault;
        }
        
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        ctrl.Move(moveDirection * Time.deltaTime);
    }
}
