using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapRequestManager : MonoBehaviour
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
    int width=Screen.width/2;
    int hight=Screen.height/2;
    float latitude;
    float longitude;
    [SerializeField] int MapSizeLevel;

    private void Start()
    {
        MapSizeLevel = Mathf.Clamp(MapSizeLevel, 1, 20);
        StartCoroutine(MapAPIRequest());
    }
    IEnumerator MapAPIRequest()
    {


        while(true)
        {
            if (POI.datalist.Count > 0)   { Debug.Log("Load Complete"); break; }
            else yield return null; 
        }

        latitude = POI.datalist[1].Latitude();
        longitude = POI.datalist[1].Longitude();

        string APIrequestURL = mapBaseURL + $"?w={width}&h={hight}&center={longitude},{latitude}&level={MapSizeLevel}" +
            $"&scale=2&format=png";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //��û�� API ���� �ؽ�ó�� �޾ƿ� �ν��Ͻ�
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);

        yield return req.SendWebRequest();  //API��û

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage�� �޾ƿ� ������ �ؽ�ó ������

        switch (req.result)
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Connection Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Protocol Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("DataProcessing Error"); yield break;
        }
    }//���̹� ���� API�� �޾ƿ� MapImage�� ǥ���ϴ� �޼���
}
