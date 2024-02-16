using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TakePhotoVideo_and_Gallery : MonoBehaviour
{
    [SerializeField] private Button CaptureBtn;
    [SerializeField] private Button RecordBtn;
    [SerializeField] private Button GalleryBtn;

    public ARCameraManager arCameraManager;
    private string savedImagePath;

    // Start is called before the first frame update
    void Start()
    {
        CaptureBtn.onClick.AddListener(SetupCamera);
        //RecordBtn.onClick.AddListener();
        GalleryBtn.onClick.AddListener(OnClickGalleryBtn);
    }

    void SetupCamera()
    {
        // AR ī�޶� �������Ǵ� ī�޶� ����
        Camera arCamera = arCameraManager.GetComponentInChildren<Camera>();
        if (arCamera != null)
        {
            // UI ���̾ �����ϰ� AR ī�޶� �������ǵ��� ����
            arCamera.cullingMask = ~(1 << LayerMask.NameToLayer("UI"));

            // UI�� ���������� �ʵ��� clear flags ����
            arCamera.clearFlags = CameraClearFlags.SolidColor;
            arCamera.backgroundColor = Color.black; // �Ǵ� ���ϴ� �������� ����
        }
    }

    void TakePhoto()
    {
        // AR ī�޶� ȭ�� ĸ��
        StartCoroutine(CaptureScreen());
    }

    IEnumerator CaptureScreen()
    {
        // AR ī�޶� ȭ�� ĸ�ĸ� ���� �غ�
        Texture2D capturedImage = null;
        Camera arCamera = arCameraManager.GetComponentInChildren<Camera>();
        if (arCamera == null)
        {
            Debug.LogError("AR ī�޶� ã�� �� �����ϴ�.");
            yield break;
        }

        // AR ī�޶� ȭ�� ĸ��
        yield return new WaitForEndOfFrame();
        RenderTexture renderTexture = arCamera.targetTexture;
        capturedImage = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        capturedImage.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedImage.Apply();
        RenderTexture.active = null;

        // ĸ�ĵ� �̹��� ó�� (���� �� ���� ��� ���)
        SaveImage(capturedImage);
    }

    void SaveImage(Texture2D image)
    {
        // �̹����� ���Ϸ� ����
        byte[] bytes = image.EncodeToPNG();
        string fileName = "ARPhoto_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // ����� �̹��� ��� ���
        savedImagePath = filePath;

        Debug.Log("������ ����Ǿ����ϴ�: " + filePath);
    }

    void OnClickGalleryBtn()
    {
        OpenGallery(savedImagePath);
    }

    void OpenGallery(string imagePath)
    {
        // Android Intent�� ����Ͽ� ������ ����
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_VIEW"));
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
        intentObject.Call<AndroidJavaObject>("setDataAndType", uriObject, "image/*");
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivityObject = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        unityActivityObject.Call("startActivity", intentObject);
    }
}
