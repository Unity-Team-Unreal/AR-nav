using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;


// POI 데이터 저장을 위한 컨테이너
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

    public string Name() { return name; }
    public string Description() { return description; }
    public string Latitude() { return latitude; }
    public string Longitude() { return longitude; }
}

public static class POI
{
    public static POIData[] datalist = new POIData[1];
}



public class POI_Info : MonoBehaviour
{
    [Header("POI데이터가 있는 웹 주소")]
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
        //예제에는 name,description,latitude,longtitude 순서로 설정하였음.

        

        string[] splited = jsonRow[4].Split(',');

        foreach(string s in splited)
        {
            Debug.Log(s);
        }

        POI.datalist[0] = new POIData(splited[0], splited[1], splited[2], splited[3]);

        yield break;

    }
}
