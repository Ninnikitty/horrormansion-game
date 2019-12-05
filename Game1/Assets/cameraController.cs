using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Camera cam;
    Vector3 thirdPLocation = new Vector3(0.544f, 3.815f, -1.378f);

    Vector3 firstPLocation = new Vector3(0, 1.862f, 0.2f);
    bool currently3rd = true;

    private float firstPSens = 5f;
    private float thirdPSens = 5f;
    private float maxYAngle = 80f;
    public Transform Player;
    public GameObject PlayerObj;
    private float mouseX, mouseY;

    private float yOffset = 10f;

    private Vector2 currentRotation;



    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        thirdPLocation = cam.transform.localPosition;
        //cam.transform.localPosition = thirdPLocation;
    }


    private void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        if (!currently3rd)
        {
            mouseX += Input.GetAxis("Mouse X") * thirdPSens;
            mouseY -= Input.GetAxis("Mouse Y") * thirdPSens;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Vector3 targetPostition = new Vector3(Player.position.x,
                                           this.transform.position.y,
                                           Player.position.z);
            this.transform.LookAt(targetPostition);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x + mouseY, this.transform.rotation.y, this.transform.rotation.z);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        else if(currently3rd){
            mouseX += Input.GetAxis("Mouse X") * thirdPSens;
            mouseY -= Input.GetAxis("Mouse Y") * thirdPSens;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Vector3 targetPostition = new Vector3(Player.position.x,
                                           this.transform.position.y,
                                           Player.position.z);
            this.transform.LookAt(targetPostition - new Vector3(-15,0,0));
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x+10+mouseY, this.transform.rotation.y+yOffset, this.transform.rotation.z);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }

    public void SwitchCamera(bool toFirst)
    {
        if (toFirst)
        {
            cam.transform.localPosition = firstPLocation;
            cam.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else if (!toFirst)
        {
            cam.transform.localPosition = thirdPLocation;
            cam.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        currently3rd = !currently3rd;
    }
    public void runningCamera(bool running)
    {
        Vector3 pos = new Vector3();
        if (currently3rd)
        {
            pos = new Vector3(thirdPLocation.x, thirdPLocation.y - 0.2f, thirdPLocation.z + 0.1f);
        }
        else if (!currently3rd)
        {
            pos = firstPLocation;
        }

        if (running)
        {
            if (!currently3rd)
            {
                PlayerObj.layer = 9;
            }
            cam.transform.localPosition = pos;
        }
        else if (!running)
        {
            PlayerObj.layer = 0;
            if (currently3rd)
            {
                cam.transform.localPosition = thirdPLocation;
            }
            else if (!currently3rd)
            {
                cam.transform.localPosition = firstPLocation;
            }
        }
    }
}