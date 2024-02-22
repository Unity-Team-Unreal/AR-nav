using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// POI �����͸� �����ϴ� �����̳ʿ� POI �����͸� �޾ƿͼ� �����̳ʿ� �����ϴ� ��ũ��Ʈ
/// </summary>
public struct POIData   //������ POI�� ���� ����ü
{
    private string name;
    private string description;
    private float latitude;
    private float longitude;
    public POIData(string name, string description, float latitude, float longtitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longtitude;
    }
    public string Name() => name;
    public string Description() =>description;
    public float Latitude() => latitude;
    public float Longitude() => longitude;
}


public static class POI   // POI�����ʹ� ������ ���̹Ƿ� static ����Ʈ�� ����
{
    public static List<POIData> datalist = new List<POIData>();
}



public class POI_Info : MonoBehaviour
{
    [Header("POI�����Ͱ� �ִ� �� �ּ�")]
    [SerializeField] string POIwebURL;

    private void Awake()
    {
        StartCoroutine(requestCoroutine());
    }
    IEnumerator requestCoroutine()  //���ͳݿ��� POI �����͸� �޾ƿ� POI.datalist�� �����ϴ� �޼���
    {
        UnityWebRequest WebData = UnityWebRequest.Get(POIwebURL);

        yield return WebData.SendWebRequest();  //POI ������ �ִ� �ּҷκ��� ������ �޾ƿ��� ��û

        string json = WebData.downloadHandler.text;  //�޾ƿ� �����͸� ����.

        string[] jsonRow = json.Split('\n');    //POI �����͸� �� �ٸ��� �и�.

        for(int i = 3; i < jsonRow.Length; i++) //3�� ������ �������� �������� �����Ͱ� 4��° �ٺ��� �����ϱ� ����
        {
            string[] splited = jsonRow[i].Split(',');   //������ �̸�,����,����,�浵 �� ��µǾ� ,�� �������� �и�

            if (splited.Length == 4 && float.TryParse(splited[2], out float splited_2
                )&& float.TryParse(splited[3],out float splited_3))   //POI ���� �����Ϳ� �̿��� ���� �ɷ����� ���� ���ǹ�

                POI.datalist.Add(new POIData(splited[0], splited[1],splited_2, splited_3));     //�и��� POI�����͸� �˸°� �й��Ͽ� datalist�� POI������ ����

        }


        yield break;

    }
}
