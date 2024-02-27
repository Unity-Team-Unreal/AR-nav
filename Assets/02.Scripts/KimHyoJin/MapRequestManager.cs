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
/// 네이버 지도 API를 사용하여 2D static 지도를 받아와 화면에 출력하는 스크립트
/// </summary>

public class MapRequestManager : MonoBehaviour
{
    [Header("네이버 API를 받기 위한 정보")]
    [SerializeField]string mapBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; 
    [SerializeField]string clientID = "r2kal6fto4";
    [SerializeField]string clientPW = "hEidFWsoN8dBnqFCreezcSC5HEE1NuYMNTmboVNz";

    [Header("지도 표시할 캔버스 이미지")]
    [SerializeField] RawImage MapImage;

    [Header("지도정보")]
    int width = 360;
    int height = 800;
    double latitude =0f;
    double longitude =0f;
    [SerializeField]int MapSizeLevel=17;


    [Header("GPS를 받기 위한 정보")]
     GPS gps;


    MarkerInstantiate markerInstantiate;

    void Awake()
    {
        gps=GetComponent<GPS>();
        markerInstantiate = GetComponent<MarkerInstantiate>();
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
        gps.Request();  //GPS 클래스의 request 메서드를 호출. 사용자에게서 GPS 권한을 받아오고 수락시 location service 실행
        MapSizeLevel = Mathf.Clamp(MapSizeLevel, 1, 20);    //지도의 확대 레벨을 1~20 사이로 제한

    }


    private void Start()
    {
        StartCoroutine(MapAPIRequest());
    }
    IEnumerator MapAPIRequest()     //네이버 지도 API를 받아와 MapImage에 표시하는 메서드
    {
        yield return new WaitUntil(() => POI.datalist.Count > 0);   //POI 데이터를 받아올 때 까지 대기


        if (!gps.GetMyLocation(ref latitude, ref longitude))     //GPS를 받아올 수 있다면 위도, 경도를 현재 위치로 설정
        {
            latitude = 37.466480f;
            longitude = 126.657566f;     //그렇지 않다면 재물포역으로

            latitude = 37.713670f;
            longitude = 126.743557f;    //테스트용 경인개
        }


        string markerRequestAPI = "";


        POI.datalist.Add(new POIData(0, "-", "MyLocation", "-", latitude, longitude, "-", "-", "-"));   //현재위치 POI에 추가

        for (int i = 0; i < POI.datalist.Count; i++)    //POI datalist 리스트를 불러와 마커 배치(markerRequestAPI 문자열은 스태틱맵의 마커이므로 네이버 마커를 안쓰면 필요없다.)
        {
            markerInstantiate.MarkerMake(width, height, MapSizeLevel, latitude, longitude, POI.datalist[i]) ;
            markerRequestAPI += $"&markers=type:d|size:mid|color:red|pos:{POI.datalist[i].Longitude()}%20{POI.datalist[i].Latitude()}";
        }



        string APIrequestURL = mapBaseURL + $"?w={width}&h={height}&center={longitude},{latitude}&level={MapSizeLevel}"+
            $"&scale=2&format=png";     //지도 API를 받아오기 위한 요청

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //요청한 API 지도 텍스처를 받아온다.
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);       //발급받은 ID
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);          //발급받은 PW

        yield return req.SendWebRequest();  //API요청

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage에 받아온 지도의 텍스처 입히기

        switch (req.result)     //받아오는데 성공시 종료. 실패할 경우 디버그로그(임시)로 실패원인 출력
        {
            case UnityWebRequest.Result.Success: yield break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Connection Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Protocol Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("DataProcessing Error"); yield break;
        }

    }


}
