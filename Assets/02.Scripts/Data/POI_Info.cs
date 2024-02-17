using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class POI_Info : MonoBehaviour
{
    //public POI poi;
    //ContentsData contents;

    [Header("POI�����Ͱ� �ִ� �� �ּ�")]
    [SerializeField] string POIwebURL; // POI �����͸� �ҷ��� �� 

    public string[] rowsData;  // �� �����͸� �����ϴ� �迭
    public string[] columnsData;   // �� �����͸� �����ϴ� �迭


    private void Awake()
    {
        StartCoroutine(requestCoroutine()); // �ڷ�ƾ ����
        GameObject POIDataGO = new GameObject("POIDataDB");
        POIDataGO.transform.position = transform.position;
        POIDataGO.transform.parent = transform;
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
                break;
            case UnityWebRequest.Result.ProtocolError: // �������� ����
                yield break;
                break;
            case UnityWebRequest.Result.DataProcessingError: // ������ ó��
                yield break; // �ڷ�ƾ ����
                break;
        }

        if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
        {
            string json = WebData.downloadHandler.text; // �� ��û�� ����� ���ڿ��� �޾ƿ�
            rowsData = json.Split('\n'); // ���ڿ��� �ٹٲ�('n') �������� ������ �� �����ͷ� ����
            ContentsData[] contentsdataArray = new ContentsData[rowsData.Length - 1];

            for (int i = 1; i < rowsData.Length; i++)
            {
                columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

                // ���� �������� ��Ʈ ������ �����
                foreach (var column in columnsData)
                {
                    Debug.Log("line: " + i + ": " + column);
                }
            }

            /*POI poi = ScriptableObject.CreateInstance<POI>();
            poi.contentsData = contentsdataArray;

            Debug.Log("ScriptableObeject created: " + poi);*/

            // ��ũ���ͺ� ������Ʈ ���� ���
            //foreach (var contentsData in poi.contentsData)
            //{
            //    Debug.Log(contentsData);
            //}
        }
    }
}


