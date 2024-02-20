using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Unity.VisualScripting;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Runtime.InteropServices;

public class RecordVideos : MonoBehaviour
{
    // 비디오 설정
    public int width = Screen.width;
    public int height = Screen.height;
    public int frameRate = 30;

    // 녹화 여부
    private bool isRecording = false;

    // 프레임 텍스처
    private RenderTexture renderTexture;

    // 비디오 코덱
    //private AVCodecContext* context;
    // 비디오 파일
    private FileStream fileStream;

    // 녹화 시작
    public void StartRecording()
    {
        // 녹화 시작 플래그 설정
        isRecording = true;

        // 프레임 텍스처 생성
        renderTexture = new RenderTexture(width, height, 24);
        renderTexture.Create();

        // 비디오 파일 생성
        string filePath = Application.dataPath + "/video.mp4";
        fileStream = new FileStream(filePath, FileMode.Create);

        // 녹화 코루틴 시작
        StartCoroutine(RecordingCoroutine());
    }

    // 녹화 중지
    public void StopRecording()
    {
        // 녹화 중지 플래그 설정
        isRecording = false;

        // 녹화 코루틴 종료
        StopCoroutine(RecordingCoroutine());

        // 비디오 파일 종료
        fileStream.Close();

        // 프레임 텍스처 해제
        renderTexture.Release();
    }

    // 녹화 코루틴
    private IEnumerator RecordingCoroutine()
    {
        while (isRecording)
        {
            // 프레임 렌더링
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.clear);
            Graphics.Blit(null, renderTexture);
            RenderTexture.active = null;

            // 비디오 파일에 프레임 데이터 쓰기
            //fileStream.Write(encodedFrame, 0, encodedFrame.Length);

            // 다음 프레임까지 대기
            yield return new WaitForEndOfFrame();
        }
    }

}

