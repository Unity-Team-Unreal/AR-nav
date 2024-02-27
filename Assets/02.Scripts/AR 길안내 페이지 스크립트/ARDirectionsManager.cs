using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 특정 좌표 주위에 도달하면 꺼져있던 UI가 켜지는 기능 구현
/// </summary>
public class ARDirectionsManager : GPS
{
    double[] lats;
    double[] longs;
    static public (double[] lats, double[] longs)[] paths;
    ARDirectionUIManager arUIManager;

    public Text test_text; // 삭제 가능
    public Text test2_text; // 삭제 가능

    void Awake()
    {
        arUIManager = GetComponent<ARDirectionUIManager>();
        Request();
    }

    void Start()
    {
        StartCoroutine(RequestPOI());
    }

    double[] remainDistance;
    bool[] effectiveDistance;
    IEnumerator RequestPOI()
    {
        while (true)
        {
            if (POI.datalist.Count > 0) break;
            yield return null;
        }
        remainDistance = new double[POI.datalist.Count];
        effectiveDistance = new bool[POI.datalist.Count];
        lats = new double[POI.datalist.Count];
        longs = new double[POI.datalist.Count];
        {
            for (int i = 0; i < POI.datalist.Count; i++)
            {
                lats[i] = POI.datalist[i].Latitude();
                longs[i] = POI.datalist[i].Longitude();
            }
        }
        yield break;
    }

    double myLat = 0f;
    double myLong = 0f;
    void Update()
    {
        TurnOnUI();
    }
    // 좌표 값을 기준으로 일정한 거리에 다가오면 켜지는 UI기능을 구현할 것
    void TurnOnUI()
    {
        if (POI.datalist.Count > 0)
        {
            if (GetMyLocation(ref myLat, ref myLong))
            {
                test2_text.text = $"{myLat}, {myLong}";

                for (int i = 0; i < lats.Length; i++)
                {
                    remainDistance[i] = Distance(myLat, myLong, lats[i], longs[i]);

                    test_text.text = $"목표와의 거리 : {remainDistance[i]}";

                    effectiveDistance[i] = remainDistance[i] <= 10f; // 10미터
                    // ARDirectionUIManager에 함수를 만들어서 위 조건에 맞으면 UI가 켜지는 기능 추가
                    arUIManager.OnPOIButton(effectiveDistance);
                }
            }
        }
    }

    // 지표면 거리 계산 공식(하버사인 공식)
    double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;

        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);

        dist = Rad2Deg(dist);

        dist = dist * 60 * 1.1515;

        dist = dist * 1609.344; // 미터 변환
        return dist;
    }

    double Deg2Rad(double deg)
    {
        return (deg * Mathf.PI / 180.0f);
    }

    double Rad2Deg(double rad)
    {
        return (rad * 180.0f / Mathf.PI);
    }
}