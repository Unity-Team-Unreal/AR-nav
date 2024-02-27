using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// POI �����͸� �����ϴ� �����̳ʿ� POI �����͸� �޾ƿͼ� �����̳ʿ� �����ϴ� ��ũ��Ʈ
/// </summary>
public struct POIData   //������ POI�� ���� ����ü

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


public static class POI   // POI�����ʹ� ������ ���̹Ƿ� static ����Ʈ�� ����
{
    public static List<POIData> datalist = new List<POIData>();
}



public class POI_Info : MonoBehaviour
{
    [Header("POI�����Ͱ� �ִ� �� �ּ�")]
    [SerializeField] string POIwebURL;
    [SerializeField] string POIsheetName;
    [SerializeField] string POIrange;

    private void Awake()
    {
        StartCoroutine(requestCoroutine());
    }
    IEnumerator requestCoroutine()  //���ͳݿ��� POI �����͸� �޾ƿ� POI.datalist�� �����ϴ� �޼���
    {
        UnityWebRequest WebData = UnityWebRequest.Get($"{POIwebURL}&{POIsheetName}&{POIrange}");

        

        yield return WebData.SendWebRequest();  //POI ������ �ִ� �ּҷκ��� ������ �޾ƿ��� ��û

        string json = WebData.downloadHandler.text;  //�޾ƿ� �����͸� ����.

        

        string[] jsonRow = json.Split('\n');    //POI �����͸� �� �ٸ��� �и�.


        string[] splited=new string[jsonRow[0].Length+5];

        for (int i = 0; i < jsonRow.Length; i++)
        {
            jsonRow[i] = jsonRow[i].Replace("\"", string.Empty);

            //Debug.Log(jsonRow[i]);
            if (i % 2 == 0)
            {
                splited = jsonRow[i].Split(',');   // ,�� �������� �и�
                if (splited.Length > 1 && splited[1] == "") splited[1] = POI.datalist.Last().Category();    //ī�װ��� ���� ���յ� ���¶� ""�� ������ ���� �־ ó��, ������ 2��° ����ó��

            }

            else
            {
                string[] desAndEvent = jsonRow[i].Split(',');   // ,�� �������� �и�


                if (int.TryParse(splited[0], out int splited_1)
                    &&double.TryParse(splited[5], out double splited_2)
                    && double.TryParse(splited[4], out double splited_3))   //POI ���� �����Ϳ� �̿��� ���� �ɷ����� ���� ���ǹ�
                {
                    POI.datalist.Add(new POIData(splited_1, splited[1], splited[3], splited[6], splited_2, splited_3, splited[7], desAndEvent[0], desAndEvent[1]));    //�и��� POI�����͸� �˸°� �й��Ͽ� datalist�� POI������ ����

                    //Debug.Log(POI.datalist.Last().Number());
                    //Debug.Log(POI.datalist.Last().Name());
                    //Debug.Log(POI.datalist.Last().Address());
                    //Debug.Log(POI.datalist.Last().Description());
                    //Debug.Log(POI.datalist.Last().Eventinformation());
                    //Debug.Log(POI.datalist.Last().Longitude());
                    //Debug.Log(POI.datalist.Last().Latitude());
                }
            }
        }


        yield break;

    }

}
