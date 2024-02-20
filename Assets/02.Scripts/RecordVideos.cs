using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Unity.VisualScripting;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Runtime.InteropServices;

public class RecordVideos : MonoBehaviour
{
    // ���� ����
    public int width = Screen.width;
    public int height = Screen.height;
    public int frameRate = 30;

    // ��ȭ ����
    private bool isRecording = false;

    // ������ �ؽ�ó
    private RenderTexture renderTexture;

    // ���� �ڵ�
    //private AVCodecContext* context;
    // ���� ����
    private FileStream fileStream;

    // ��ȭ ����
    public void StartRecording()
    {
        // ��ȭ ���� �÷��� ����
        isRecording = true;

        // ������ �ؽ�ó ����
        renderTexture = new RenderTexture(width, height, 24);
        renderTexture.Create();

        // ���� ���� ����
        string filePath = Application.dataPath + "/video.mp4";
        fileStream = new FileStream(filePath, FileMode.Create);

        // ��ȭ �ڷ�ƾ ����
        StartCoroutine(RecordingCoroutine());
    }

    // ��ȭ ����
    public void StopRecording()
    {
        // ��ȭ ���� �÷��� ����
        isRecording = false;

        // ��ȭ �ڷ�ƾ ����
        StopCoroutine(RecordingCoroutine());

        // ���� ���� ����
        fileStream.Close();

        // ������ �ؽ�ó ����
        renderTexture.Release();
    }

    // ��ȭ �ڷ�ƾ
    private IEnumerator RecordingCoroutine()
    {
        while (isRecording)
        {
            // ������ ������
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.clear);
            Graphics.Blit(null, renderTexture);
            RenderTexture.active = null;

            // ���� ���Ͽ� ������ ������ ����
            //fileStream.Write(encodedFrame, 0, encodedFrame.Length);

            // ���� �����ӱ��� ���
            yield return new WaitForEndOfFrame();
        }
    }

}

