using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// POI 데이터 요청 및 받은 데이터로 UI조작
/// </summary>
public class POIDataRequest : MonoBehaviour
{
    const string pOIUrl1 = "https://docs.google.com/spreadsheets/d/1rDtc2JqJ6ZSvvy6dujlc-bsF-dbw2E5rZJjGxmAryfA/export?format=tsv&range=B5:H9";
    const string pOIUrl2 = "https://docs.google.com/spreadsheets/d/1rDtc2JqJ6ZSvvy6dujlc-bsF-dbw2E5rZJjGxmAryfA/export?format=tsv&range=B13:H17";
    static string[] datas1;
    static string[] datas2;

    [SerializeField] TextMeshProUGUI businessName;
    [SerializeField] TextMeshProUGUI branchName;
    [SerializeField] TextMeshProUGUI getCoupon;

    [SerializeField] TextMeshProUGUI popupBusinessName;
    [SerializeField] TextMeshProUGUI address;
    [SerializeField] TextMeshProUGUI openingHours;
    [SerializeField] TextMeshProUGUI couponEventInformationText;

    [SerializeField] Image classification;
    [SerializeField] Image couponIcon;

    [SerializeField] Image popupClassification;
    [SerializeField] Image popupCouponIcon;
    [SerializeField] Image barcode;

    void Start()
    {
        StartCoroutine(RequestPOI());
        TestServer();
    }

    IEnumerator RequestPOI()
    {
        UnityWebRequest pOIData1 = UnityWebRequest.Get(pOIUrl1);
        UnityWebRequest pOIData2 = UnityWebRequest.Get(pOIUrl2);

        yield return pOIData1.SendWebRequest();
        yield return pOIData2.SendWebRequest();

        switch (pOIData1.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
        }

        if (pOIData1.isDone == true)
        {
            string json = pOIData1.downloadHandler.text;

            string[] rows = json.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split('\t');

                foreach (string column in columns)
                {
                    Debug.Log("line : " + i + " : " + column);
                }
                datas1 = columns;
            }
        }
        
        switch (pOIData2.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
        }

        if (pOIData2.isDone == true)
        {
            string json = pOIData2.downloadHandler.text;

            string[] rows = json.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split('\t');

                foreach (string column in columns)
                {
                    Debug.Log("line : " + i + " : " + column);
                }
                datas2 = columns;
            }
        }
    }

    void TestServer()
    {
        businessName.text = datas1[3];
        branchName.text = datas1[4];

        popupBusinessName.text = datas2[3];
        address.text = datas2[5];
        couponEventInformationText.text = datas2[6];
    }
}
