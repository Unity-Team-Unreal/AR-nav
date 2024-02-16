using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticMapManager : MonoBehaviour
{
    [Header("���̹� API�� �ޱ� ���� ����")]
    [SerializeField]string mapBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; 
    [SerializeField]string clientID = "r2kal6fto4";
    [SerializeField]string clientPW = "hEidFWsoN8dBnqFCreezcSC5HEE1NuYMNTmboVNz";

    [Header("���� ǥ���� ĵ���� �̹���")]
    [SerializeField] RawImage MapImage; 
    void Awake()
    {
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
    }

    [Header("��������")]
    [SerializeField] int width = 900;
    [SerializeField] int hight = 2000;
    string latitude;
    string longitude;
    [SerializeField] int MapLevel = 20;

    private void Start()
    {
        StartCoroutine(waitForGetPOI());
    }

    IEnumerator waitForGetPOI()
    {
        while (true)
        {
            if (POI.datalist[0].Name()==null) yield return null;

            else break;
        }

        latitude = POI.datalist[0].Latitude();
        longitude = POI.datalist[0].Longitude();


        StartCoroutine(NaverMapAPIRequest());
    }
    IEnumerator NaverMapAPIRequest()
    {

        string APIrequestURL = mapBaseURL + $"?w={width}&h={hight}" +
            $"&center={longitude},{latitude}&level={MapLevel}" +
            $"&scale=2&format=png";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API ���� �ؽ�ó�� �޾ƿ� �ν��Ͻ�
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);

        yield return req.SendWebRequest();  //API��û

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage�� �޾ƿ� ������ �ؽ�ó ������

        switch (req.result)
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("Error"); yield break;
        }
    }//���̹� ���� API�� �޾ƿ� MapImage�� ǥ���ϴ� �޼���
}
