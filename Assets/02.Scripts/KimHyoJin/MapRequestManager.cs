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
    float latitude=0f;
    float longitude=0f;
    float latitude2 = 0f;
    float longitude2 = 0f;
    [SerializeField] int MapSizeLevel=17;

    [Header("GPS�� �ޱ� ���� ����")]
    GPS gps;


    [Header("��Ŀ��ġ �׽�Ʈ")]
    Vector2 point = new();


    [SerializeField] GameObject Marker;


    void Awake()
    {
       gps=GetComponent<GPS>();
        gps.Request();
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
    }


    private void Start()
    {
        MapSizeLevel = Mathf.Clamp(MapSizeLevel, 1, 20);
        StartCoroutine(MapAPIRequest());
    }
    IEnumerator MapAPIRequest()
    {
        

        yield return new WaitUntil(() => POI.datalist.Count > 0);

            if(!gps.GetMyLocation(ref latitude, ref longitude))
            {
                latitude = POI.datalist[1].Latitude();
                longitude = POI.datalist[1].Longitude();
            }




            /// ��Ŀ��ġ �׽�Ʈ///
            
         
        latitude = 37.466480f;
        longitude = 126.657566f;


        float C = (256 / (2 * Mathf.PI)) * 2 * (MapSizeLevel + 1);
        float x = C * (math.radians(longitude) + Mathf.PI);
        float y = C * (Mathf.PI - math.log(math.tan((Mathf.PI / 4) + math.radians(latitude) / 2)));

        Debug.Log($"{x}/{y}");





        //          256x256 �ȼ� ����
        //zoom lvl 20 �� �� 1�ȼ��� 0.0745m ��ô 1:250
        //     lvl 17�� �� 1�ȼ��� 0.597m   ��ô 1:2000
        //     lvl 13�� �� 1�ȼ��� 9.5545m  ��ô 1:35000
        //     lvl 10�� �� 1�ȼ��� 76.437m  ��ô 1:250000

        float Lv1size = 156_543;
        float perPixel = Lv1size/Mathf.Pow(2,MapSizeLevel+1);

        float defaulte = 256f;

        float widthPer = defaulte/width;
        float heightPer = defaulte/height;

        float inUnityPerPixel = widthPer*heightPer*perPixel;

        Debug.Log($"/{widthPer}/{heightPer}/{inUnityPerPixel}");


        Debug.Log($"{MapSizeLevel}/{perPixel}");


        //case 1
        latitude2 = 37.467262f;
        longitude2 = 126.657732f;

        float distance = (float)distanceInKilometerByHaversine(latitude, longitude, latitude2, longitude2);
        float bearing = bearingP1toP2(latitude, longitude, latitude2, longitude2);

        Debug.Log(distance);
        Debug.Log(bearing);

        distance = distance*1000/inUnityPerPixel;

        Debug.Log(distance);


        Vector2 direction = new Vector2(Mathf.Sin(bearing * Mathf.Deg2Rad), Mathf.Cos(bearing * Mathf.Deg2Rad));
        point = new Vector2(0,0)+ direction.normalized * distance;

        Debug.Log(point);

        Marker = Instantiate(Marker, point, quaternion.identity);
        Marker.transform.SetParent(GameObject.Find("Canvas").transform,false);


        ///�׽�Ʈ ��///




        string APIrequestURL = mapBaseURL + $"?w={width}&h={height}&center={longitude},{latitude}&level={MapSizeLevel}" +
            $"&markers=type:d|size:mid|pos:{longitude}%20{latitude}" +
            $"&markers=type:d|size:mid|color:red|pos:{longitude2}%20{latitude2}" +
            $"&scale=2&format=png";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API ���� �ؽ�ó�� �޾ƿ� �ν��Ͻ�
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);

        yield return req.SendWebRequest();  //API��û

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage�� �޾ƿ� ������ �ؽ�ó ������

        switch (req.result)
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Connection Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Protocol Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("DataProcessing Error"); yield break;
        }




    }//���̹� ���� API�� �޾ƿ� MapImage�� ǥ���ϴ� �޼���



    //http://disq.us/t/3idn6p2
    public double distanceInKilometerByHaversine(double x1, double y1, double x2, double y2)
    {
        double distance;
        double radius = 6371; // ���� ������(km)
        double toRadian = Math.PI / 180;

        double deltaLatitude = Math.Abs(x1 - x2) * toRadian;
        double deltaLongitude = Math.Abs(y1 - y2) * toRadian;

        double sinDeltaLat = Math.Sin(deltaLatitude / 2);
        double sinDeltaLng = Math.Sin(deltaLongitude / 2);
        double squareRoot = Math.Sqrt(
            sinDeltaLat * sinDeltaLat +
            Math.Cos(x1 * toRadian) * Math.Cos(x2 * toRadian) * sinDeltaLng * sinDeltaLng);

        distance = 2 * radius * Math.Asin(squareRoot);

        return distance;
    }

    //https://tttsss77.tistory.com/177
    //https://jini-story.com/204
    public int bearingP1toP2(double x1, double y1, double x2, double y2)
    {

        double x1Rad = Mathf.Deg2Rad * x1;
        double x2Rad = Mathf.Deg2Rad * x2;
        double y1rad = Mathf.Deg2Rad * y1;
        double y2rad = Mathf.Deg2Rad * y2;

        double y = Math.Sin(y2rad-y1rad)*Math.Cos(x2Rad);
        double x = Math.Cos(x1Rad)*Math.Sin(x2Rad)-Math.Sin(x1Rad)*Math.Cos(x2Rad)*Math.Cos(y2rad-y1rad);

        double z = Math.Atan2(y, x);

        double answer= Mathf.Rad2Deg*z;

        if(answer<0) answer = 180+(180+answer);

        return (int)answer;

    }

}
