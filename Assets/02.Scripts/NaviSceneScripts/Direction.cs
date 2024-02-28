using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Direction : MonoBehaviour
{
    /// <summary>
    /// ��ã�� ����� �����ϴ� ��ũ��Ʈ�̸� �̿ϼ�
    /// </summary>
    [Header("���̹� API�� �ޱ� ���� ����")]
    [SerializeField] string directionBaseURL = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string clientID = "r2kal6fto4";
    [SerializeField] string clientPW = "hEidFWsoN8dBnqFCreezcSC5HEE1NuYMNTmboVNz";


    string myPoint_longitude = "127.149251";
    string myPoint_latitude = "35.813306";
    string destination_longitude = "126.977222";
    string destination_latitude = "37.578611";
    string optionCode = "traoptimal";

    void FindDirection()
    {
        StartCoroutine(NaverMapDirectionAPIRequest());
    }

    IEnumerator NaverMapDirectionAPIRequest()
    {

        string APIrequestURL = directionBaseURL + $"?start={myPoint_longitude},{myPoint_latitude}&goal={destination_longitude},{destination_latitude}&option={optionCode}";

        UnityWebRequest req = UnityWebRequest.Get(APIrequestURL);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);

        yield return req.SendWebRequest();

        Debug.Log(req.downloadHandler.text);
        switch (req.result)
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: yield break;
            case UnityWebRequest.Result.ProtocolError: yield break;
            case UnityWebRequest.Result.DataProcessingError: yield break;
        }


    }
}
