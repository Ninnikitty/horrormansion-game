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
    float sprintSpeed = 4;
    float walkSpeedBack = -1;
	float sideStepSpeed = 0.8f;
    float timeTillIdleDefault = 10;
    float timeTillIdle;

    bool in3rdOerson = true;

    [SerializeField] public GameObject polaroidCanvas; //set the polaroid canvas from inspector
    bool canvasOn;
    public GameObject keyUI; //place counterui here

    GameObject flashObj; //for camera flash
    private float timeToAppear = 1f;
    private float timeWhenDisappear;

    private float minTime = 0.01f;
    private float lastTime = 0f;

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

        flashObj = GameObject.Find("CameraFlashLight"); //find the flash cameobject
        flashObj.GetComponent<Light>().enabled = false; //light is off
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
            ShowCanvas(); //show polaroid canvas
            canvasOn = true;
            HideKeyUI(); // hide key ui because the polaroid camera is on
        } else if (in3rdOerson && canvasOn) //if canvas is on and we're in 3rd person, take canvas off
        {
            canvasOn = false;
            HideCanvas(); //hide polaroid canvas
            ShowKeyUI(); //show key ui 

            flashObj.GetComponent<Light>().enabled = false; //turn the flash off
        }

        if (flashObj.GetComponent<Light>().enabled = true && (0.02 - lastTime) > minTime)//if (flashObj.GetComponent<Light>().enabled = true && (Time.time >= timeWhenDisappear)) //disappearance time
        {
            flashObj.GetComponent<Light>().enabled = false; 
        } 
         
        if (Input.GetKeyDown(KeyCode.F) && canvasOn) //capturing pictures
        {
            ScreenCapture.TakeScreenshot_Static(1000, 1000); //set width and height
            flashObj.GetComponent<Light>().enabled = true; //allow the light to be flashed
            timeWhenDisappear = timeToAppear; //Time.time + timeToAppear;
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        ctrl.Move(moveDirection * Time.deltaTime);
    }

    void ShowCanvas() //show and hide functions for polaroid canvas
    {
        polaroidCanvas.SetActive(true);
    }

    void HideCanvas()
    {
        polaroidCanvas.SetActive(false);
    }

    public void ShowKeyUI() //show and hide keyUI (made public so other classes can access
    {
        keyUI.SetActive(true);
    }

    public void HideKeyUI()
    {
        keyUI.SetActive(false);
    }
}