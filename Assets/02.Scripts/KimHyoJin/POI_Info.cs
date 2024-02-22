using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// POI 데이터를 저장하는 컨테이너와 POI 데이터를 받아와서 컨테이너에 저장하는 스크립트
/// </summary>
public struct POIData   //저장할 POI의 정보 구조체
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


public static class POI   // POI데이터는 공유할 것이므로 static 리스트로 구현
{
    public static List<POIData> datalist = new List<POIData>();
}



public class POI_Info : MonoBehaviour
{
    [Header("POI데이터가 있는 웹 주소")]
    [SerializeField] string POIwebURL;

    private void Awake()
    {
        StartCoroutine(requestCoroutine());
    }
    IEnumerator requestCoroutine()  //인터넷에서 POI 데이터를 받아와 POI.datalist에 저장하는 메서드
    {
        UnityWebRequest WebData = UnityWebRequest.Get(POIwebURL);

        yield return WebData.SendWebRequest();  //POI 정보가 있는 주소로부터 데이터 받아오기 요청

        string json = WebData.downloadHandler.text;  //받아온 데이터를 저장.

        string[] jsonRow = json.Split('\n');    //POI 데이터를 각 줄마다 분리.

        for(int i = 3; i < jsonRow.Length; i++) //3인 이유는 예제에서 실질적인 데이터가 4번째 줄부터 시작하기 때문
        {
            string[] splited = jsonRow[i].Split(',');   //예제상 이름,설명,위도,경도 로 출력되어 ,를 기준으로 분리

            if (splited.Length == 4 && float.TryParse(splited[2], out float splited_2
                )&& float.TryParse(splited[3],out float splited_3))   //POI 정보 데이터와 이외의 것을 걸러내기 위한 조건문

                POI.datalist.Add(new POIData(splited[0], splited[1],splited_2, splited_3));     //분리한 POI데이터를 알맞게 분배하여 datalist에 POI데이터 생성

        }


        yield break;

    }
}
