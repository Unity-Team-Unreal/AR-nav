using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;


// POI ������ ������ ���� �����̳�
public struct POIData
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


public static class POI
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
    IEnumerator requestCoroutine()
    {
        UnityWebRequest WebData = UnityWebRequest.Get(POIwebURL);
        yield return WebData.SendWebRequest();
        string json = WebData.downloadHandler.text;

        string[] jsonRow = json.Split('\n');    //�������� name,description,latitude,longtitude ������ �����Ͽ���.

        for(int i = 3; i < jsonRow.Length; i++)
        {
            string[] splited = jsonRow[i].Split(',');

            if (splited.Length == 4 && float.TryParse(splited[2], out float splited_2) && float.TryParse(splited[3],out float splited_3))
                POI.datalist.Add(new POIData(splited[0], splited[1],splited_2, splited_3));

        }


        yield break;

    }
}
