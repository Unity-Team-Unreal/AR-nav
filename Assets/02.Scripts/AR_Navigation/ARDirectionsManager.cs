using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ư�� ��ǥ ������ �����ϸ� �����ִ� UI�� ������ ��� ����
/// </summary>
public class ARDirectionsManager : GPS // GPS�� ���
{
    double[] lats; // ����
    double[] longs; // �浵
    static public (double[] lats, double[] longs)[] paths;
    ARDirectionUIManager arUIManager;

    void Awake()
    {
        arUIManager = GetComponent<ARDirectionUIManager>();
        Request(); // ����ڿ��� GPS ���� ��û
    }

    void Start()
    {
        StartCoroutine(RequestPOI());
    }

    double[] remainDistance;
    bool[] effectiveDistance;
    IEnumerator RequestPOI() // POI �������� ������ �浵�� ���� ���ϰ� ����
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
        
        for (int i = 0; i < POI.datalist.Count; i++)
        {
            lats[i] = POI.datalist[i].Latitude();
            longs[i] = POI.datalist[i].Longitude();
        }
        
        yield break;
    }

    double myLat = 0f;
    double myLong = 0f;
    void Update()
    {
        TurnOnUI();
    }
    double lastDistanceUpdateTime = 0f;

    // ��ǥ ���� �������� ������ �Ÿ��� �ٰ����� ������ UI����� ������ ��
    void TurnOnUI()
    {
        if (POI.datalist.Count > 0)
        {
            if (GetMyLocation(ref myLat, ref myLong)) // ���� ������ �浵�� �޾ƿ��鼭 �������� ��
            {
                // �ð� ������ �����Ͽ� ���� �ֱ⸶�� UI�� ������Ʈ�ϵ��� ��
                if (Time.time - lastDistanceUpdateTime >= 5f)
                {
                    for (int i = 0; i < lats.Length; i++)
                    {
                        remainDistance[i] = Distance(myLat, myLong, lats[i], longs[i]); // ���� ��ǥ�� POI ��ǥ ������ �Ÿ�

                        effectiveDistance[i] = remainDistance[i] <= 30f; // 30����
                    }
                    // ARDirectionUIManager�� �Լ��� ���� �� ���ǿ� ������ UI�� ������ ��� �߰�
                    arUIManager.OnPOIButton(effectiveDistance);
                }
            }
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