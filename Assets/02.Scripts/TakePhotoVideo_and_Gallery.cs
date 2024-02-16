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
        // AR 카메라가 렌더링되는 카메라 설정
        Camera arCamera = arCameraManager.GetComponentInChildren<Camera>();
        if (arCamera != null)
        {
            // UI 레이어를 제외하고 AR 카메라만 렌더링되도록 설정
            arCamera.cullingMask = ~(1 << LayerMask.NameToLayer("UI"));

            // UI가 렌더링되지 않도록 clear flags 설정
            arCamera.clearFlags = CameraClearFlags.SolidColor;
            arCamera.backgroundColor = Color.black; // 또는 원하는 배경색으로 설정
        }
    }

    void TakePhoto()
    {
        // AR 카메라 화면 캡쳐
        StartCoroutine(CaptureScreen());
    }

    IEnumerator CaptureScreen()
    {
        // AR 카메라 화면 캡쳐를 위한 준비
        Texture2D capturedImage = null;
        Camera arCamera = arCameraManager.GetComponentInChildren<Camera>();
        if (arCamera == null)
        {
            Debug.LogError("AR 카메라를 찾을 수 없습니다.");
            yield break;
        }

        // AR 카메라 화면 캡쳐
        yield return new WaitForEndOfFrame();
        RenderTexture renderTexture = arCamera.targetTexture;
        capturedImage = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        capturedImage.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedImage.Apply();
        RenderTexture.active = null;

        // 캡쳐된 이미지 처리 (저장 및 저장 경로 기억)
        SaveImage(capturedImage);
    }

    void SaveImage(Texture2D image)
    {
        // 이미지를 파일로 저장
        byte[] bytes = image.EncodeToPNG();
        string fileName = "ARPhoto_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // 저장된 이미지 경로 기억
        savedImagePath = filePath;

        Debug.Log("사진이 저장되었습니다: " + filePath);
    }

    void OnClickGalleryBtn()
    {
        OpenGallery(savedImagePath);
    }

    void OpenGallery(string imagePath)
    {
        // Android Intent를 사용하여 갤러리 열기
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
