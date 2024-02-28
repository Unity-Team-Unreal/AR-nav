using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


/// <summary>
/// ���̹� ���� API�� ����Ͽ� 2D static ������ �޾ƿ� ȭ�鿡 ����ϴ� ��ũ��Ʈ
/// </summary>

public class MapRequestManager : MonoBehaviour
{
    [Header("���̹� API�� �ޱ� ���� ����")]
    [SerializeField]string mapBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; 
    [SerializeField]string clientID = "r2kal6fto4";
    [SerializeField]string clientPW = "hEidFWsoN8dBnqFCreezcSC5HEE1NuYMNTmboVNz";

    [Header("���� ǥ���� ĵ���� �̹���")]
    [SerializeField] RawImage MapImage;

    [Header("��������")]
    int width = 360;
    int height = 800;
    double latitude =0f;
    double longitude =0f;
    [SerializeField]int MapSizeLevel=17;


    [Header("GPS�� �ޱ� ���� ����")]
     GPS gps;

    [Header("��Ŀ�� �����ϴ� ��ũ��Ʈ")]
    MarkerInstantiate markerInstantiate;

    void Awake()
    {
        gps=GetComponent<GPS>();
        markerInstantiate = GetComponent<MarkerInstantiate>();
        MapImage = MapImage.gameObject.GetComponent<RawImage>();

        gps.Request();  //GPS Ŭ������ request �޼��带 ȣ��. ����ڿ��Լ� GPS ������ �޾ƿ��� ������ location service ����

        MapSizeLevel = Mathf.Clamp(MapSizeLevel, 1, 20);    //������ Ȯ�� ������ 1~20 ���̷� ����

    }


    private void Start()
    {
        StartCoroutine(MapAPIRequest());    //������ ���� ��Ŀ������ ��û�ϴ� �ڷ�ƾ
    }
    IEnumerator MapAPIRequest()     //���̹� ���� API�� �޾ƿ� MapImage�� ǥ���ϴ� �޼���
    {
        yield return new WaitUntil(() => POI.datalist.Count > 0);   //POI �����͸� �޾ƿ� �� ���� ���


        if (!gps.GetMyLocation(ref latitude, ref longitude))     //GPS�� �޾ƿ� �� �ִٸ� ����, �浵�� ���� ��ġ�� ����
        {
            latitude = 37.466480f;
            longitude = 126.657566f;     //�׷��� �ʴٸ� �繰��������
        }


        POI.datalist.Add(new POIData(0, "-", "MyLocation", "-", latitude, longitude, "-", "-", "-"));   //��Ŀ ������ ���� ������ġ �����͸� POI�� �߰�

        for (int i = 0; i < POI.datalist.Count; i++)    //POI datalist ����Ʈ�� �ҷ��� ��Ŀ ��ġ
        {
            markerInstantiate.MarkerMake(width, height, MapSizeLevel, latitude, longitude, POI.datalist[i]);
        }



        string APIrequestURL = mapBaseURL + $"?w={width}&h={height}&center={longitude},{latitude}&level={MapSizeLevel}"+
            $"&scale=2&format=png";     //���� API�� �޾ƿ��� ���� ��û


        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API��� ���� �ؽ�ó�� �޾ƿ´�.
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);       //�߱޹��� ID
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);          //�߱޹��� PW


        yield return req.SendWebRequest();  //API��û


        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage�� �޾ƿ� ������ �ؽ�ó ������

        switch (req.result)     //�޾ƿ��µ� ������ ����. ������ ��� ����׷α�(�ӽ�)�� ���п��� ���
        {
            case UnityWebRequest.Result.Success: yield break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Connection Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Protocol Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("DataProcessing Error"); yield break;
        }

    }


}
