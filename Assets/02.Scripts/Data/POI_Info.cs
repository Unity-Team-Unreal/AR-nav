using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class POI_Info : MonoBehaviour
{
    string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI �����͸� �ҷ��� �� 
    string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:E6"; // POI �����͸� �ҷ��� �� 
    //gid=1564857787

    [Header("������ �̹���")]
    public Texture2D[] Photozoneimage;
    public Texture2D[] Docentimage;

    [Header("������ �̹��� ���")]
    public string Photo_floder = "Photoimage";
    public string Docent_floder = "Docentimage";


    public POI poi;
    public POI Docent;
    
    public string[] rowsData;  // �� �����͸� �����ϴ� �迭
    public string[] columnsData;   // �� �����͸� �����ϴ� �迭

    public GameObject contentsDataPfb; //POI ������
    public Transform PhotoscrollViewContent; //��ũ�� ���� Contents�� ����Ű�� ����
    public Transform Docentscrolltransform; //��ũ�� ���� Contents�� ����Ű�� ����


    private void Awake()
    {
        Photozoneimage = Resources.LoadAll<Texture2D>("Photoimage");
        Docentimage = Resources.LoadAll<Texture2D>("Docentimage");

        //poi.contentsdata = new ContentsData[.Length];

        //StartCoroutine(requestCoroutine(PhotozonewebURL)); // �ڷ�ƾ ���� 

        StartCoroutine(requestCoroutine(poi, PhotozonewebURL, Photozoneimage, PhotoscrollViewContent)); // �ڷ�ƾ ���� 
        StartCoroutine(requestCoroutine(Docent, DocentPOIwebURL, Docentimage, Docentscrolltransform));
    }


    IEnumerator requestCoroutine(POI contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        UnityWebRequest WebData = UnityWebRequest.Get(link); // �� ��û ����

        yield return WebData.SendWebRequest(); // �� ��û ������ �� ���� ���


        // �� ���� ����� ���� ó��
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success: //����
                break;
            case UnityWebRequest.Result.ConnectionError: // ���� ����
                yield break;
            case UnityWebRequest.Result.ProtocolError: // �������� ����
                yield break;
            case UnityWebRequest.Result.DataProcessingError: // ������ ó��
                yield break; // �ڷ�ƾ ����
        }

        if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
        {
            POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform); // �� ��û�� ����� ���ڿ���    
        }
    }

    void POIDB(string tsv, POI poi, Texture2D[] texture2D, Transform transform)
    {
        rowsData = tsv.Split('\n'); // ���ڿ��� �ٹٲ�('n') �������� ������ �� �����ͷ� ����
        int rowsSize = rowsData.Length;
        int columnSize = rowsData[0].Split("\t").Length;

        

        for (int i = 0; i < rowsSize; i++)
        {
            columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);
            

            for (int j = 0; j < columnSize; j++)
            {
                ContentsData contentsData = poi.contentsdata[i];

                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = texture2D[i];
            }

            poiPrefabsIntance.GetComponent<POIPrefab>().Init(poi.contentsdata[i]);

            // ���� �������� ��Ʈ ������ �����
            foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }
        }
    }
}


