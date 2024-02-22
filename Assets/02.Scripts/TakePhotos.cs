using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TakePhotos : MonoBehaviour
{
    string path = "/storage/emulated/0/DCIM/AR-Nav";

    private void Start()
    {
        // 폴더 존재 여부 확인
        bool isExists = Directory.Exists(path);

        // 폴더가 없으면 생성
        if (!isExists)
        {
            Directory.CreateDirectory(path);
        }
    }

    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
        
    }

    IEnumerator TakeAPhoto()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine("/storage/emulated/0/DCIM/AR-Nav", fileName);
        File.WriteAllBytes(filePath, bytes);

        Destroy(rt);
        Destroy(image);
    }
}
