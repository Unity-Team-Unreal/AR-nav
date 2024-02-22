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
    float []latitude;
    float []longitude;
    int MapSizeLevel=17;

    [Header("GPS�� �ޱ� ���� ����")]
    GPS gps;


    MarkerInstantiate markerInstantiate;

    void Awake()
    {
        gps=GetComponent<GPS>();
        markerInstantiate = GetComponent<MarkerInstantiate>();
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
        gps.Request();  //GPS Ŭ������ request �޼��带 ȣ��. ����ڿ��Լ� GPS ������ �޾ƿ��� ������ location service ����
        MapSizeLevel = Mathf.Clamp(MapSizeLevel, 1, 20);    //������ Ȯ�� ������ 1~20 ���̷� ����

        latitude = new float[POI.datalist.Count];
        longitude = new float[POI.datalist.Count];
    }


    private void Start()
    {
        StartCoroutine(MapAPIRequest());
    }
    IEnumerator MapAPIRequest()     //���̹� ���� API�� �޾ƿ� MapImage�� ǥ���ϴ� �޼���
    {
        yield return new WaitUntil(() => POI.datalist.Count > 0);   //POI �����͸� �޾ƿ� �� ���� ���


        latitude = new float[POI.datalist.Count];
        longitude = new float[POI.datalist.Count];

        if (!gps.GetMyLocation(ref latitude[0], ref longitude[0]))     //GPS�� �޾ƿ� �� �ִٸ� ����, �浵�� ���� ��ġ�� ����
        {
            latitude[0] = 37.466480f;
            longitude[0] = 126.657566f;     //�׷��� �ʴٸ� �繰��������
        }




        /// �׽�Ʈ�� ��,�浵 ///

        latitude[0] = 37.466480f;
        longitude[0] = 126.657566f;

        latitude[1] = 37.467262f;
        longitude[1] = 126.657732f;

        latitude[2] = 37.466177f;
        longitude[2] = 126.657768f;

        /// �׽�Ʈ�� ��,�浵 ///

        for (int i = 0; i < POI.datalist.Count; i++)    //POI datalist ����Ʈ�� ���� �浵�� �ҷ��� ��Ŀ ��ġ
        {
            if (latitude[i] == 0) continue;
            markerInstantiate.MarkerMake(width, height, MapSizeLevel, latitude[0], longitude[0], latitude[i], longitude[i]);
        }




        string APIrequestURL = mapBaseURL + $"?w={width}&h={height}&center={longitude[0]},{latitude[0]}&level={MapSizeLevel}" +
            $"&markers=type:d|size:mid|pos:{longitude[0]}%20{latitude[0]}" +
            $"&markers=type:d|size:mid|color:red|pos:{longitude[1]}%20{latitude[1]}" +
            $"&markers=type:d|size:mid|color:red|pos:{longitude[2]}%20{latitude[2]}" +
            $"&scale=2&format=png";     //���� API�� �޾ƿ��� ���� ��û

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API ���� �ؽ�ó�� �޾ƿ� �ν��Ͻ�
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);       //�߱޹��� ID
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);          //�߱޹��� PW

        yield return req.SendWebRequest();  //API��û

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage�� �޾ƿ� ������ �ؽ�ó ������

        switch (req.result)     //�޾ƿ��µ� ������ ����. ������ ��� ����׷α�(�ӽ�)�� ���п��� ���
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Connection Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Protocol Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("DataProcessing Error"); yield break;
        }

    }


}
