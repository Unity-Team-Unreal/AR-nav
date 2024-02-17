using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class POI_Info : MonoBehaviour
{
    string POIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI �����͸� �ҷ��� �� 

    [Header("img������ �ּ�")]
    public Texture2D[] image;
    //Texture2D image;

    public POI poi;
    public string[] rowsData;  // �� �����͸� �����ϴ� �迭
    public string[] columnsData;   // �� �����͸� �����ϴ� �迭


    private void Awake()
    {
        image = Resources.LoadAll<Texture2D>("Potoimage");
        StartCoroutine(requestCoroutine()); // �ڷ�ƾ ���� 
    }


    IEnumerator requestCoroutine()
    {
        UnityWebRequest WebData = UnityWebRequest.Get(POIwebURL); // �� ��û ����

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
            POIDB(WebData.downloadHandler.text); // �� ��û�� ����� ���ڿ���    
        }
    }

    void POIDB(string tsv)
    {
        rowsData = tsv.Split('\n'); // ���ڿ��� �ٹٲ�('n') �������� ������ �� �����ͷ� ����
        int rowsSize = rowsData.Length;
        int columnSize = rowsData[0].Split("\t").Length;

        for (int i = 0; i < rowsSize; i++)
        {
            columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

            for (int j = 0; j < columnSize; j++)
            {
                ContentsData contentsData = poi.contentsdata[i];

                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = image[i];
            }

            // ���� �������� ��Ʈ ������ �����
            foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }
        }
    }
}


