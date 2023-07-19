using UnityEngine;
using UnityEditor;
using System.IO;

public class Screenshotter : EditorWindow
{
    [MenuItem("screenshot/TakeScreenshot")]
    public static void CaptureScreenshot()
    {
        Debug.Log(Application.dataPath);
        string path = Application.dataPath + "/../screenShoots";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        int filesLen = (new DirectoryInfo(path)).GetFiles().Length;
        ScreenCapture.CaptureScreenshot(path + "/Screenshot" + filesLen + ".png");
        Debug.Log("Captured screenshoot No" + filesLen);
    }
}
