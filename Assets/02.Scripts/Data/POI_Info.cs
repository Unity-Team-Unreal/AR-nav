using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class POI_Info : MonoBehaviour
{
    //public POI poi;
    //ContentsData contents;

    [Header("POI데이터가 있는 웹 주소")]
    [SerializeField] string POIwebURL; // POI 데이터를 불러올 웹 

    public string[] rowsData;  // 행 데이터를 저장하는 배열
    public string[] columnsData;   // 열 데이터를 저장하는 배열


    private void Awake()
    {
        StartCoroutine(requestCoroutine()); // 코루틴 시작
        GameObject POIDataGO = new GameObject("POIDataDB");
        POIDataGO.transform.position = transform.position;
        POIDataGO.transform.parent = transform;
    }
    IEnumerator requestCoroutine()
    {
        UnityWebRequest WebData = UnityWebRequest.Get(POIwebURL); // 웹 요청 생성

        yield return WebData.SendWebRequest(); // 웹 요청 보내기 및 응답 대기

        // 웹 요층 결과에 따른 처리
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success: //성공
                break;
            case UnityWebRequest.Result.ConnectionError: // 연결 오류
                yield break;
                break;
            case UnityWebRequest.Result.ProtocolError: // 프로토콜 오류
                yield break;
                break;
            case UnityWebRequest.Result.DataProcessingError: // 데이터 처리
                yield break; // 코루틴 종료
                break;
        }

        if (WebData.isDone) // 웹 요청이 완료되었는지 확인
        {
            string json = WebData.downloadHandler.text; // 웹 요청의 결과를 문자열로 받아옴
            rowsData = json.Split('\n'); // 문자열을 줄바꿈('n') 기준으로 나눠서 행 데이터로 저장
            ContentsData[] contentsdataArray = new ContentsData[rowsData.Length - 1];

            for (int i = 1; i < rowsData.Length; i++)
            {
                columnsData = rowsData[i].Split('\t'); // 행 데이터를 탭('/t') 기준으로 나눠서 열 데이터로 저장

                // 구글 스프레드 시트 데이터 디버그
                foreach (var column in columnsData)
                {
                    Debug.Log("line: " + i + ": " + column);
                }
            }

            /*POI poi = ScriptableObject.CreateInstance<POI>();
            poi.contentsData = contentsdataArray;

            Debug.Log("ScriptableObeject created: " + poi);*/

            // 스크립터블 오브젝트 내용 출력
            //foreach (var contentsData in poi.contentsData)
            //{
            //    Debug.Log(contentsData);
            //}
        }
    }
}


