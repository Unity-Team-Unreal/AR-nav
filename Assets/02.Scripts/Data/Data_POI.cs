using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class Data_POI : MonoBehaviour
{
    public Contents_POI DocentsPOI;
    public Contents_POI PhotoPOI;
    public GameObject POIprefabs;

    public string DocentsWebRink = "";
    public string PhotoWebRink = "";
    
    public void GetData()
    {
        StartCoroutine(requestCoroutine(DocentsWebRink, DocentsPOI));
        StartCoroutine(requestCoroutine(PhotoWebRink, PhotoPOI));
    }

    IEnumerator requestCoroutine(string POIlink, Contents_POI poicontainer)
    {
        //�� ��û ����
        UnityWebRequest WebData = UnityWebRequest.Get(POIlink);

        //�� ��û ���� ���
        yield return WebData.SendWebRequest();


        // �� ���� ����� ���� ó��
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success:
                //���� : ������ ó��
                if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
                {
                    SpreadSpilt(WebData.downloadHandler.text, poicontainer);
                }
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
            case UnityWebRequest.Result.DataProcessingError:
                //���� : ���� ó��
                Debug.LogError(WebData.error);
                break;
        }
    }

    private void SpreadSpilt(string text, Contents_POI poicontainer)
    {
        string[] rows;
        string[] columns;

        rows = text.Split('\n');

        POIcontainerLength(rows.Length, poicontainer);

        for (int i = 0; i < rows.Length; i++)
        {
            columns = rows[i].Split("\t");

            for (int j = 0; j < columns.Length; j++)
            {
                //ContentsData�� �������� ����
                ContentsData contentsData = new ContentsData();

                contentsData.number = columns[0];
                contentsData.contentsname = columns[1];
                contentsData.description = columns[2];
                contentsData.latitude = columns[3];
                contentsData.longitude = columns[4];
                contentsData.guide = columns[5];

                //poi.contentsdata �迭�� �߰�
                poicontainer.contentsdata[i] = contentsData;
            }
/*            foreach (var column in columns)
            {
                Debug.Log("line: " + i + " " + column);
            }*/
        }

    }


    private void POIcontainerLength(int length, Contents_POI poicontainer)
    {
        if (poicontainer == null || poicontainer.contentsdata.Length != length)
        {
            poicontainer.contentsdata = new ContentsData[length];
        }
        else
        {
            Debug.Log("������ �迭�� ũ�Ⱑ �Ҵ�Ǿ� �ֽ��ϴ�.");
            return;
        }
    }

}
