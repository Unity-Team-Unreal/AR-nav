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
    private string latitude;
    private string longitude;
    public POIData(string name, string description, string latitude, string longtitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longtitude;
    }
    public string Name() => name;
    public string Description() =>description;
    public string Latitude() => latitude;
    public string Longitude() => longitude;
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

        string[] jsonRow = json.Split('\n');
        //�������� name,description,latitude,longtitude ������ �����Ͽ���.

        for(int i = 3; i < jsonRow.Length; i++)
        {
            string[] splited = jsonRow[i].Split(',');

            if(splited.Length==4) POI.datalist.Add(new POIData(splited[0], splited[1], splited[2], splited[3]));
        }


        yield break;

    }
}
