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

    [Header("지도 표시할 캔버스 이미지")]
    [SerializeField] RawImage MapImage; 
    void Start()
    {
        MapImage = MapImage.gameObject.GetComponent<RawImage>();
        StartCoroutine(NaverMapAPIRequest());
    }

    [Header("지도정보")]
    [SerializeField] int width = 900;
    [SerializeField] int hight = 2000;
    [SerializeField] string latitude = "35.813306";
    [SerializeField] string longitude = "127.149251";
    [SerializeField] int MapLevel = 15;
    IEnumerator NaverMapAPIRequest()
    {

        string APIrequestURL = baseURL + $"?w={width}&h={hight}&center={longitude},{latitude}&level={MapLevel}";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(APIrequestURL);     //요청한 API 지도 텍스처를 받아올 객체
        req.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        req.SetRequestHeader("X-NCP-APIGW-API-KEY", clientPW);

        yield return req.SendWebRequest();  //API요청

        MapImage.texture = DownloadHandlerTexture.GetContent(req);  //MapImage에 받아온 지도의 텍스처 입히기

        switch (req.result)
        {
            case UnityWebRequest.Result.Success: break;
            case UnityWebRequest.Result.ConnectionError: Debug.Log("Error"); yield break;
            case UnityWebRequest.Result.ProtocolError: Debug.Log("Error"); yield break;
            case UnityWebRequest.Result.DataProcessingError: Debug.Log("Error"); yield break;
        }
    }//네이버 지도 API를 받아와 MapImage에 표시하는 메서드
}
