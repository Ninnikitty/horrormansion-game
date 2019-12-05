using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    float speed = 0;
    float rotSpeed = 80;
    float gravity = 600;
    float rot = 0;

    float walkSpeed = 2;
    float walkSpeedBack = -1;
	float sideStepSpeed = 0.8f;
    float timeTillIdleDefault = 10;
    float timeTillIdle;

    bool in3rdOerson = true;

    KeyCode forward = KeyCode.W;
    KeyCode backwards = KeyCode.S;
    KeyCode right = KeyCode.D;
    KeyCode left = KeyCode.A;
    KeyCode sprint = KeyCode.LeftShift;
    KeyCode crouch = KeyCode.LeftControl;
    KeyCode switchCamera = KeyCode.C;

    Vector3 moveDirection = Vector3.zero;

    CharacterController ctrl;
    Animator animator;
    cameraController cs;
    // Start is called before the first frame update
    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cs = FindObjectOfType<cameraController>();
        timeTillIdle = timeTillIdleDefault;
    }

    // Update is called once per frame
    void Update()
    {
        timeTillIdle -= Time.deltaTime;

        //If pressed forward
        if (Input.GetKey(forward))
        {
            moveDirection = new Vector3(0, 0, 1);
            if (Input.GetKey(sprint))
            {
                animator.SetInteger("playerAnimState", 2);
                speed = sprintSpeed;
                cs.runningCamera(true);
            }
            else
            {
                speed = walkSpeed;
                animator.SetInteger("playerAnimState", 1);
            }
            if (Input.GetKeyUp(sprint))
            {
                cs.runningCamera(false);
            }
        }
        //If pressed backwards
        else if (Input.GetKey(backwards))
        {
            moveDirection = new Vector3(0, 0, 1);
            animator.SetInteger("playerAnimState", 1);
            speed = walkSpeedBack;
        }
        //If pressed right
        else if (Input.GetKey(right))
        {
            animator.SetInteger("playerAnimState", 5);
            speed = sideStepSpeed;
            moveDirection = new Vector3(1, 0, 0);
        }
        //If pressed left
        else if (Input.GetKey(left))
        {
            animator.SetInteger("playerAnimState", 4);
            speed = sideStepSpeed;
            moveDirection = new Vector3(-1, 0, 0);
        }
        //After checking for position and setting speed and animation, move the character
        moveDirection *= speed;
        moveDirection = transform.TransformDirection(moveDirection);

        //if not holding forward or backwards stay still
        if (Input.GetKeyUp(forward))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        if (Input.GetKeyUp(backwards))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        if (Input.GetKeyUp(right))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        if (Input.GetKeyUp(left))
        {
            speed = 0;
            animator.SetInteger("playerAnimState", 0);
            moveDirection = Vector3.zero;
        }
        if (timeTillIdle < 0)
        {
            animator.SetInteger("playerAnimState", -1);
            timeTillIdle = timeTillIdleDefault;
        }
        if (Input.GetKeyUp(switchCamera))
        {
            cs.SwitchCamera(in3rdOerson);
            in3rdOerson = !in3rdOerson;
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        ctrl.Move(moveDirection * Time.deltaTime);
    }
}