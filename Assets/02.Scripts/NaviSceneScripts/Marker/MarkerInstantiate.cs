using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class MarkerInstantiate : MonoBehaviour
{   
    /// <summary>
    /// 마커가 배치될 위치를 정하고 마커를 생성하는 스크립트
    /// </summary>

    Vector2 point = new();  //마커가 생성될 위치

    [SerializeField] GameObject Marker;     // 마커 프리팹

    BubbleState MarkersbubbleState;     //마커 프리팹의 스크립트 컴포넌트



    

    public void MarkerMake(int width, int height, float Level, double latitude, double longitude, POIData poidata)    //    마커를 생성하는 메서드.
    {
            float Lv1size = 156_543;    //줌 레벨 1의 픽셀당 미터

            float perPixel = Lv1size / Mathf.Pow(2, Level + 1);     //1단계마다 절반씩 줄어들며, openstreetmap 기준이므로 네이버맵에 맞게 1단계 더 올린다.

            float defaulte = 256f;      //픽셀당 미터 값의 기준 해상도는 256x256.

            float widthPer = defaulte / width;
            float heightPer = defaulte / height;        //기준해상도 대비 지도의 해상도 비율


            float inUnityPerPixel = widthPer * heightPer * perPixel;    //지도 해상도의 픽셀당 미터값


            float distance = (float)distanceInKilometerByHaversine(latitude, longitude, poidata.Latitude(), poidata.Longitude());     //기준점과 마커와의 실제 거리 재기

            float bearing = bearingP1toP2(latitude, longitude, poidata.Latitude(), poidata.Longitude());    //기준점과 마커와의 방위각 재기


            distance = 1000 * distance / inUnityPerPixel;  // 실제 거리를 유니티 지도상에서 몇 픽셀만큼 그려야 하는지 계산


            Vector2 direction = new Vector2(Mathf.Sin(bearing * Mathf.Deg2Rad), Mathf.Cos(bearing * Mathf.Deg2Rad));    //방위각을 이용해 마커가 놓일 방향을 정하기


            point = new Vector2(0, 0) + direction.normalized *  distance;     //노멀라이즈로 방향만 정한 뒤, distance만큼 떨어진 거리에 마커를 띄운다.


            if(GameObject.Find("Marker_" + poidata.Number()) == null)       //이 POI 마커가 아직 없다면
            {
                Marker = Instantiate(Marker, point, quaternion.identity);   //point 위치에 마커를 생성한다.
                MarkersbubbleState = Marker.GetComponent<BubbleState>();    //마커의 BubbleState 컴포넌트를 받아
                MarkersbubbleState.thisData = poidata;                      //POI 데이터를 마커에 넣는다.
                Marker.name = "Marker_" + poidata.Number();                 //마커의 이름을 설정한다.
                Marker.transform.SetParent(GameObject.Find("StaticMapImage").transform, false);     //지도의 위치 변화에 따라갈 수 있도록 자식으로 넣는다.
                MarkersbubbleState.MarkerStart();   //마커의 설정 시작
            }

            else GameObject.Find("Marker_" + poidata.Number()).transform.localPosition = point;     //이미 마커가 있다면 위치만 바꾼다.

    }



    /// <summary> 참고사이트 
    /// http://disq.us/t/3idn6p2
    /// </summary>
    double distanceInKilometerByHaversine(double x1, double y1, double x2, double y2)    //특정 지점으로부터 마커를 띄울 장소와의 현실의 직선거리 계산 메서드
    {
        double distance;
        double radius = 6371; // 지구 반지름(km)
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

    /// <summary> 참고사이트  
    /// https://tttsss77.tistory.com/177 
    /// https://jini-story.com/204
    /// </summary>
    int bearingP1toP2(double x1, double y1, double x2, double y2)        //특정 지점으로부터 마커를 띄울 장소와의 방위각 계산 메서드
    {

        double x1Rad = Mathf.Deg2Rad * x1;
        double x2Rad = Mathf.Deg2Rad * x2;
        double y1rad = Mathf.Deg2Rad * y1;
        double y2rad = Mathf.Deg2Rad * y2;

        double y = Math.Sin(y2rad - y1rad) * Math.Cos(x2Rad);
        double x = Math.Cos(x1Rad) * Math.Sin(x2Rad) - Math.Sin(x1Rad) * Math.Cos(x2Rad) * Math.Cos(y2rad - y1rad);

        double z = Math.Atan2(y, x);

        double answer = Mathf.Rad2Deg * z;

        if (answer < 0) answer = 180 + (180 + answer);

        return (int)answer;

    }




}
