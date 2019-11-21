using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitcher : MonoBehaviour
{
    Camera cam;
    Vector3 thirdPLocation = new Vector3(0, 1.97f, -1.79f);
    Vector3 firstPLocation = new Vector3(0, 1.75f, 0.25f);
    bool currently3rd = true;

    public float sensitivity = 10f;
    public float maxYAngle = 80f;

    public float cameraRotSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    private Vector2 currentRotation;
    

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }


    void Update()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        cam.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * cameraRotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * cameraRotSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        Vector3 targetPostition = new Vector3(Player.position.x,
                                       this.transform.position.y,
                                       Player.position.z);
        this.transform.LookAt(targetPostition);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
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
            pos = new Vector3(thirdPLocation.x, thirdPLocation.y - 0.4f, thirdPLocation.z + 1f);
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
