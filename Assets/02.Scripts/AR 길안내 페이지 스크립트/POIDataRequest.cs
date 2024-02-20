using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// POI 데이터 요청
/// </summary>
public class POIDataRequest : MonoBehaviour
{
    public string pOIUrl = "https://docs.google.com/spreadsheets/d/12Tqn4M1GYrbYsB8X-1fSUCQAk45D33LB5Rj7kckgG0g/A13:H17";

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
    }

    IEnumerator RequestPOI()
    {
        UnityWebRequest pOIData = UnityWebRequest.Get(pOIUrl);

        yield return pOIData.SendWebRequest();

        switch (pOIData.result)
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

        if (pOIData.isDone == true)
        {
            string json = pOIData.downloadHandler.text;

            string[] rows = json.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split('\t');

                foreach (string column in columns)
                    Debug.Log("line : " + i + " : " + column);
            }
        }
    }
}
