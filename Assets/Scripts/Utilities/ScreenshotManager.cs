using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager instance = null;

    [SerializeField] private Camera camera;

    private bool takeScreenshotOnNextFrame = false;
    private int fileCount = 0;
    private string directoryPath;

    void Awake()
    {
        instance = this;
        directoryPath = Application.dataPath + "/Screenshots/";
        Directory.CreateDirectory(directoryPath);

        DirectoryInfo di = new DirectoryInfo(directoryPath);
        fileCount = di.GetFiles("*.png", SearchOption.AllDirectories).Length;
    }

    //private void OnPostRender()
    //{
    //    if (takeScreenshotOnNextFrame) {
    //        takeScreenshotOnNextFrame = false;
    //        RenderTexture renderTexture = camera.targetTexture;

    //        // Get the image
    //        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
    //        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

    //        // Write to file
    //        byte[] byteArray = renderResult.EncodeToPNG();
    //        string filePath = directoryPath + "screenshot" + fileCount++ + ".png";
    //        File.WriteAllBytes(filePath, byteArray);
    //        Debug.Log("Saved screenshot" + fileCount + ".png");
    //        Debug.Log("Saved to " + filePath);
    //        UIController.instance.ScreenshotSuccessful();

    //        // Release resources
    //        RenderTexture.ReleaseTemporary(renderTexture);
    //        camera.targetTexture = null;
    //    }
    //}

    public void TakeScreenshot(int width, int height)
    {
        camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public void TakeScreenshot(bool showText)
    {
        string filePath = directoryPath + "screenshot" + fileCount++ + ".png";
        ScreenCapture.CaptureScreenshot(filePath);

        if (showText)
            UIController.instance.ScreenshotSuccessful();
    }


}
