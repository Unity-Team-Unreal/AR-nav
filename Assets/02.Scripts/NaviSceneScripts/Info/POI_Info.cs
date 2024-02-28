using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// POI 데이터를 저장하는 컨테이너와 POI 데이터를 받아와서 컨테이너에 저장하는 스크립트
/// </summary>
public struct POIData   //저장할 POI의 정보가 담긴 구조체

{
    private int number;
    private string category;
    private string name;
    private double latitude;
    private double longitude;
    private string branch;
    private string address;
    private string description;
    private string eventinformation;


    //생성자로 POI 데이터 담기
    public POIData(int number, string category, string name, string branch, double latitude, double longtitude, string address, string description, string eventinformation)
    {
        this.number = number;
        this.category = category;
        this.name = name;
        this.branch = branch;
        this.description = description;
        this.address = address;
        this.latitude = latitude;
        this.longitude = longtitude;
        this.eventinformation = eventinformation;
    }

    //read only 프로퍼티로 POI 데이터 출력
    public int Number() => number;
    public string Category() => category;
    public string Name() => name;
    public string Branch() => branch;
    public double Latitude() => latitude;
    public double Longitude() => longitude;
    public string Address() => address;
    public string Description() =>description;
    public string Eventinformation() =>eventinformation;
}


public static class POI   // POI데이터는 공유할 것이므로 static, 리스트로 구현
{
    public static List<POIData> datalist = new List<POIData>();
}



public class POI_Info : MonoBehaviour
{
    [Header("POI데이터가 있는 웹 주소")]
    [SerializeField] string POIwebURL;
    [SerializeField] string POIsheetName;
    [SerializeField] string POIrange;

    private void Awake()
    {
        StartCoroutine(requestCoroutine()); // POI 요청 시작
    }
    IEnumerator requestCoroutine()  //인터넷에서 POI 데이터를 받아와 POI.datalist에 저장하는 메서드
    {
        UnityWebRequest WebData = UnityWebRequest.Get($"{POIwebURL}&{POIsheetName}&{POIrange}");

        

        yield return WebData.SendWebRequest();  //POI 정보가 있는 주소로부터 데이터 받아오기 요청

        string json = WebData.downloadHandler.text;  //받아온 데이터를 저장.

        string[] jsonRow = json.Split('\n');    //받아온 데이터를 POI별로 분리.


        string[] splited=new string[jsonRow[0].Length]; //POI의 각각의 정보를 분리할 배열

        for (int i = 0; i < jsonRow.Length; i++)
        {
            jsonRow[i] = jsonRow[i].Replace("\"", string.Empty);    //필요없는 문자 지우기

            if (i % 2 == 0)  //셀 하나에 줄이 두개인 데이터가 있어 두번째 줄을 처리하기 위한 구분. 첫째줄일 경우
            {
                splited = jsonRow[i].Split(',');   // ,를 기준으로 분리

                if (splited.Length > 1 && splited[1] == "") splited[1] = POI.datalist.Last().Category();
                //카테고리의 셀이 병합된 상태라 ""로 나오는 때가 있어 바로 직전의 카테고리 셀로 적용되게끔 처리

            }

            else
            {
                string[] desAndEvent = jsonRow[i].Split(',');   // ,를 기준으로 분리


                if (int.TryParse(splited[0], out int splited_1)
                    &&double.TryParse(splited[5], out double splited_2)
                    && double.TryParse(splited[4], out double splited_3))   //문자열로 받아온 POI의 번호와 위경도를 변환
                {
                    POI.datalist.Add(new POIData(splited_1, splited[1], splited[3], splited[6], splited_2, splited_3, splited[7], desAndEvent[0], desAndEvent[1]));
                    //분리한 POI데이터를 알맞게 분배하여 datalist에 POI데이터 생성

                }
            }
        }


        yield break;

    }

}
