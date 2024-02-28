using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TakePhotos : MonoBehaviour
{
    //���� ���� ���� ���
    string path = "/storage/emulated/0/DCIM/AR-Nav";

    private void Start()
    {
        // ���� ���� ���� Ȯ��
        bool isExists = Directory.Exists(path);

        // ������ ������ ����
        if (!isExists)
        {
            Directory.CreateDirectory(path);
        }
    }

    //��ư�� ������ ���� �Կ��� �����ϴ� �ڷ�ƾ���� �̵�
    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
        
    }

    /// <summary>
    /// ���� �Կ� �� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeAPhoto()
    {
        //������ ������ ���
        yield return new WaitForEndOfFrame();

        //���� ī�޶� ��������
        Camera camera = Camera.main;

        //ȭ�� ũ�� ��������
        int width = Screen.width;
        int height = Screen.height;

        //���� �ؽ��� ���� �� ī�޶� Ÿ�� ����
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        //���� Ȱ�� ���� �ؽ��� ���� �� ����
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        //ī�޶� �� ������
        camera.Render();

        //���ο� �ؽ��� ���� �� Ȱ�� ���� �ؽ��ķκ��� �ȼ� �б�
        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        //ī�޶� Ÿ�� ����
        camera.targetTexture = null;

        //���� �ؽ��� ����
        RenderTexture.active = currentRT;

        //�̹����� ����Ʈ �迭�� ��ȯ
        byte[] bytes = image.EncodeToPNG();

        //���� �̸� ����
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        //���� ��� ����
        string filePath = Path.Combine(path, fileName);

        //����
        File.WriteAllBytes(filePath, bytes);

        //���� �ؽ��� �� �̹��� �޸� ����
        Destroy(rt);
        Destroy(image);
    }
}
