using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;
using UnityEngine.UI;

/// <summary>
/// Ư�� ��ǥ ������ �����ϸ� �����ִ� UI�� ������ ��� ����
/// </summary>
public class ARDirectionsManager : GPS
{
    [SerializeField] double[] lats;
    [SerializeField] double[] longs;

    ARDirectionUIManager arUIManager;

    public Text test_text; // ���� ����
    public Text test2_text; // ���� ����

    void Awake()
    {
        arUIManager = GetComponent<ARDirectionUIManager>();
        Request();
    }

    double myLat = 0f;
    double myLong = 0f;
    void Update()
    {
        test();
    }

    void test()
    {
        if (GetMyLocation(ref myLat, ref myLong))
        {
            test2_text.text = $"{myLat}, {myLong}";

            double remainDistance = Distance(myLat, myLong, lats[0], longs[0]);

            test_text.text = $"��ǥ���� �Ÿ� : {remainDistance}";

            arUIManager.OnPOIButton(remainDistance <= 30f) ;
        }
    }

    // ��ǥ�� �Ÿ� ��� ����(�Ϲ����� ����)
    double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;

        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);

        dist = Rad2Deg(dist);

        dist = dist * 60 * 1.1515;

        dist = dist * 1609.344; // ���� ��ȯ
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