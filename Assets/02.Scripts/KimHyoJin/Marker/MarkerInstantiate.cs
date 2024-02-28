using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class MarkerInstantiate : MonoBehaviour
{   
    /// <summary>
    /// ��Ŀ�� ��ġ�� ��ġ�� ���ϰ� ��Ŀ�� �����ϴ� ��ũ��Ʈ
    /// </summary>

    Vector2 point = new();  //��Ŀ�� ������ ��ġ

    [SerializeField] GameObject Marker;     // ��Ŀ ������

    BubbleState MarkersbubbleState;     //��Ŀ �������� ��ũ��Ʈ ������Ʈ



    

    public void MarkerMake(int width, int height, float Level, double latitude, double longitude, POIData poidata)    //    ��Ŀ�� �����ϴ� �޼���.
    {
            float Lv1size = 156_543;    //�� ���� 1�� �ȼ��� ����

            float perPixel = Lv1size / Mathf.Pow(2, Level + 1);     //1�ܰ踶�� ���ݾ� �پ���, openstreetmap �����̹Ƿ� ���̹��ʿ� �°� 1�ܰ� �� �ø���.

            float defaulte = 256f;      //�ȼ��� ���� ���� ���� �ػ󵵴� 256x256.

            float widthPer = defaulte / width;
            float heightPer = defaulte / height;        //�����ػ� ��� ������ �ػ� ����


            float inUnityPerPixel = widthPer * heightPer * perPixel;    //���� �ػ��� �ȼ��� ���Ͱ�


            float distance = (float)distanceInKilometerByHaversine(latitude, longitude, poidata.Latitude(), poidata.Longitude());     //�������� ��Ŀ���� ���� �Ÿ� ���

            float bearing = bearingP1toP2(latitude, longitude, poidata.Latitude(), poidata.Longitude());    //�������� ��Ŀ���� ������ ���


            distance = 1000 * distance / inUnityPerPixel;  // ���� �Ÿ��� ����Ƽ �����󿡼� �� �ȼ���ŭ �׷��� �ϴ��� ���


            Vector2 direction = new Vector2(Mathf.Sin(bearing * Mathf.Deg2Rad), Mathf.Cos(bearing * Mathf.Deg2Rad));    //�������� �̿��� ��Ŀ�� ���� ������ ���ϱ�


            point = new Vector2(0, 0) + direction.normalized *  distance;     //��ֶ������ ���⸸ ���� ��, distance��ŭ ������ �Ÿ��� ��Ŀ�� ����.


            if(GameObject.Find("Marker_" + poidata.Number()) == null)       //�� POI ��Ŀ�� ���� ���ٸ�
            {
                Marker = Instantiate(Marker, point, quaternion.identity);   //point ��ġ�� ��Ŀ�� �����Ѵ�.
                MarkersbubbleState = Marker.GetComponent<BubbleState>();    //��Ŀ�� BubbleState ������Ʈ�� �޾�
                MarkersbubbleState.thisData = poidata;                      //POI �����͸� ��Ŀ�� �ִ´�.
                Marker.name = "Marker_" + poidata.Number();                 //��Ŀ�� �̸��� �����Ѵ�.
                Marker.transform.SetParent(GameObject.Find("StaticMapImage").transform, false);     //������ ��ġ ��ȭ�� ���� �� �ֵ��� �ڽ����� �ִ´�.
                MarkersbubbleState.MarkerStart();   //��Ŀ�� ���� ����
            }

            else GameObject.Find("Marker_" + poidata.Number()).transform.localPosition = point;     //�̹� ��Ŀ�� �ִٸ� ��ġ�� �ٲ۴�.

    }



    /// <summary> ��������Ʈ 
    /// http://disq.us/t/3idn6p2
    /// </summary>
    double distanceInKilometerByHaversine(double x1, double y1, double x2, double y2)    //Ư�� �������κ��� ��Ŀ�� ��� ��ҿ��� ������ �����Ÿ� ��� �޼���
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

    /// <summary> ��������Ʈ  
    /// https://tttsss77.tistory.com/177 
    /// https://jini-story.com/204
    /// </summary>
    int bearingP1toP2(double x1, double y1, double x2, double y2)        //Ư�� �������κ��� ��Ŀ�� ��� ��ҿ��� ������ ��� �޼���
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