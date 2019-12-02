using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenCapture : MonoBehaviour
{
    private static ScreenCapture instance;

    private Camera captureCamera;
    private bool takeScreenShotOnNextFrame;
    private int picCounter = 0;

    private void Awake()
    {
        instance = this;
        captureCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if(takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = captureCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            picCounter++;

            byte[] byteArray = renderResult.EncodeToPNG();
            //System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            //var folder = Directory.CreateDirectory(@"C:\Users\%\Documents\TeamErrorPics\");
            System.IO.File.WriteAllBytes(@"C:\Users\alex\Desktop\ExamplePictureFolder" + "/ " + picCounter + " CameraScreenshot.png", byteArray); //testpath
            //System.IO.File.WriteAllBytes(System.Environment.SpecialFolder.ApplicationData + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved " + picCounter + " CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            captureCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        captureCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
