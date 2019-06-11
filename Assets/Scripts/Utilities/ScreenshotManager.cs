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

    public void TakeScreenshot(bool showText)
    {
        string filePath = directoryPath + "screenshot" + fileCount++ + ".png";
        ScreenCapture.CaptureScreenshot(filePath);

        if (showText)
            UIController.instance.ScreenshotSuccessful();
    }


}
