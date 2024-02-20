using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class POI_Info_JT : MonoBehaviour
{
    //�̱��� �ν��Ͻ�
    private static POI_Info_JT instance;
    public static POI_Info_JT Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<POI_Info_JT>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<POI_Info_JT>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    //POI ������ ��ũ
    [Header("������ ��ũ")]
    string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI �����͸� �ҷ��� �� 
    string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:E6"; // POI �����͸� �ҷ��� �� 
    //gid=1564857787

    //������ �迭
    string[] rowsData;  // �� �����͸� �����ϴ� �迭
    string[] columnsData;   // �� �����͸� �����ϴ� �迭
    
    //�̹���
    [Header("������ �̹���")]
    public Texture2D[] Photozoneimage;
    public Texture2D[] Docentimage;

    //�̹��� ���
    [Header("������ �̹��� ���")]
    public string Photo_floder = "Photoimage";
    public string Docent_floder = "Docentimage";

    //POI ������
    [Header("POI ������")]
    public POIJT photozone;
    public POIJT Docent;

    //������ ������
    [Header("������ ���� ������Ʈ")]
    public GameObject contentsDataPfb; //POI ������

    //������ ���� ��ġ
    [Header("������ ���� ��ġ")]
    public Transform PhotoscrollViewContent; //������ ��ũ�� ���� Contents
    public Transform Docentscrolltransform; //����Ʈ ��ũ�� ���� Contents


    private void Awake()
    {
        //�̱��� ����
        var objs = FindObjectsOfType<POI_Info_JT>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //�̹��� �ε�
        Photozoneimage = Resources.LoadAll<Texture2D>("Photoimage");
        Docentimage = Resources.LoadAll<Texture2D>("Docentimage");

        //������ �ҷ����� ����
        StartCoroutine(requestCoroutine(photozone, PhotozonewebURL, Photozoneimage, PhotoscrollViewContent)); // �ڷ�ƾ ���� 
        StartCoroutine(requestCoroutine(Docent, DocentPOIwebURL, Docentimage, Docentscrolltransform));
    }


    /// <summary>
    /// �ڷ�ƾ : �� ������ ��û �� ó��
    /// </summary>
    /// <param name="contentspoi">POI ������ ����ü</param>
    /// <param name="link">�� ������ ��ũ</param>
    /// <param name="texture2D">�̹��� �迭</param>
    /// <param name="transform">��ũ�� ��</param>
    /// <returns></returns>
    IEnumerator requestCoroutine(POIJT contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        //�� ��û ����
        UnityWebRequest WebData = UnityWebRequest.Get(link); 

        //�� ��û ���� ���
        yield return WebData.SendWebRequest(); 


        // �� ���� ����� ���� ó��
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success: //����
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError: 
            case UnityWebRequest.Result.DataProcessingError:
                //����: ���� ó��
                Debug.LogError(WebData.error);
                break;
        }

        if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
        {
            POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform); // �� ��û�� ����� ���ڿ���    
        }
    }

    /// <summary>
    /// ������ �Ľ� �� ����
    /// </summary>
    /// <param name="tsv">������ ���ڿ�</param>
    /// <param name="poi">POI ������ ����ü</param>
    /// <param name="texture2D">�̹��� �迭</param>
    /// <param name="transform">��ũ�� ��</param>
    void POIDB(string tsv, POIJT poi, Texture2D[] texture2D, Transform transform)
    {
        // �� ������ �и�
        rowsData = tsv.Split('\n');

        // ������ Ȯ��
        int columnSize = rowsData[0].Split("\t").Length;

        //������ ��ȸ
        for (int i = 0; i < rowsData.Length; i++)
        {
            // �� ������ �и�
            columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

            // POI ������ ����
            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);

            // ������ ���� �� �ʱ�ȭ
            for (int j = 0; j < columnSize; j++)
            {

                ContentsData contentsData = poi.contentsdata[i];
                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = texture2D[i];

                poiPrefabsIntance.GetComponent<POIPrefabs>().Init(poi.contentsdata[i]);
            }

            // ���� �������� ��Ʈ ������ �����
            //foreach (var column in columnsData)
            //{
            //    Debug.Log("line: " + i + ": " + column);
            //}
        }
    }
}

