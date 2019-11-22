using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Camera cam;
    Vector3 thirdPLocation = new Vector3(0, 1.97f, -1.79f);
    Vector3 firstPLocation = new Vector3(0, 1.75f, 0.25f);
    bool currently3rd = true;

    private float firstPSens = 5f;
    private float thirdPSens = 5f;
    private float maxYAngle = 80f;
    public Transform Player;
    private float mouseX, mouseY;

    private Vector2 currentRotation;
    

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        if (!currently3rd)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * firstPSens;
            currentRotation.y -= Input.GetAxis("Mouse Y") * firstPSens;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            cam.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        else if(currently3rd){
            mouseX += Input.GetAxis("Mouse X") * thirdPSens;
            mouseY -= Input.GetAxis("Mouse Y") * thirdPSens;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Vector3 targetPostition = new Vector3(Player.position.x,
                                           this.transform.position.y,
                                           Player.position.z);
            this.transform.LookAt(targetPostition);
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
            pos = new Vector3(thirdPLocation.x, thirdPLocation.y - 0.4f, thirdPLocation.z + 0.6f);
        }
        else if (!currently3rd)
        {
            pos = new Vector3(firstPLocation.x, firstPLocation.y - 0.4f, firstPLocation.z + 1f);
        }

        if (running)
        {
            cam.transform.localPosition = pos;
        }
        else if (!running)
        {
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
