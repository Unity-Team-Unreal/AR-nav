using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticMapManager : MonoBehaviour
{
    string baseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster"; 
    string clientID = "r2kal6fto4";
    string clientPW = "hEidFWsoN8dBnqFCreezcSC5HEE1NuYMNTmboVNz";

    [Header("���� ǥ���� ĵ���� �̹���")]
    [SerializeField] RawImage MapImage; 
    void Start()
    {
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
        StartCoroutine(NaverMapAPIRequest());
    }

    [Header("��������")]
    [SerializeField] int width = 900;
    [SerializeField] int hight = 2000;
    [SerializeField] string latitude = "35.813306";
    [SerializeField] string longitude = "127.149251";
    [SerializeField] int MapLevel = 15;
    IEnumerator NaverMapAPIRequest()
    {

        string APIrequestURL = baseURL + $"?w={width}&h={hight}&center={longitude},{latitude}&level={MapLevel}";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API ���� �ؽ�ó�� �޾ƿ� ��ü
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
