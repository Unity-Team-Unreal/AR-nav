using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TakePhotos : MonoBehaviour
{
    //사진 저장 폴더 경로
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

    //버튼을 누르면 사진 촬영과 저장하는 코루틴으로 이동
    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
        
    }

    /// <summary>
    /// 사진 촬영 및 저장
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeAPhoto()
    {
        //프레임 끝까지 대기
        yield return new WaitForEndOfFrame();

        //메인 카메라 가져오기
        Camera camera = Camera.main;

        //화면 크기 가져오기
        int width = Screen.width;
        int height = Screen.height;

        //렌더 텍스쳐 생성 및 카메라 타겟 설정
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        //현재 활성 렌더 텍스쳐 저장 및 설정
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        //카메라 뷰 렌더링
        camera.Render();

        //새로운 텍스쳐 생성 및 활성 렌더 텍스쳐로부터 픽셀 읽기
        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        //카메라 타겟 해제
        camera.targetTexture = null;

        //렌더 텍스쳐 복원
        RenderTexture.active = currentRT;

        //이미지를 바이트 배열로 변환
        byte[] bytes = image.EncodeToPNG();

        //파일 이름 설정
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        //파일 경로 설정
        string filePath = Path.Combine(path, fileName);

        //저장
        File.WriteAllBytes(filePath, bytes);

        //렌더 텍스쳐 및 이미지 메모리 해제
        Destroy(rt);
        Destroy(image);
    }
}
