using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class POI_Info : MonoBehaviour
{
    string POIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI 데이터를 불러올 웹 

    [Header("img데이터 주소")]
    public Texture2D[] image;
    //Texture2D image;

    public POI poi;
    public string[] rowsData;  // 행 데이터를 저장하는 배열
    public string[] columnsData;   // 열 데이터를 저장하는 배열


    private void Awake()
    {
        image = Resources.LoadAll<Texture2D>("Potoimage");
        StartCoroutine(requestCoroutine()); // 코루틴 시작 
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
            case UnityWebRequest.Result.ProtocolError: // 프로토콜 오류
                yield break;
            case UnityWebRequest.Result.DataProcessingError: // 데이터 처리
                yield break; // 코루틴 종료
        }

        if (WebData.isDone) // 웹 요청이 완료되었는지 확인
        {
            POIDB(WebData.downloadHandler.text); // 웹 요청의 결과를 문자열로    
        }
    }

    void POIDB(string tsv)
    {
        rowsData = tsv.Split('\n'); // 문자열을 줄바꿈('n') 기준으로 나눠서 행 데이터로 저장
        int rowsSize = rowsData.Length;
        int columnSize = rowsData[0].Split("\t").Length;

        for (int i = 0; i < rowsSize; i++)
        {
            columnsData = rowsData[i].Split('\t'); // 행 데이터를 탭('/t') 기준으로 나눠서 열 데이터로 저장

            for (int j = 0; j < columnSize; j++)
            {
                ContentsData contentsData = poi.contentsdata[i];

                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = image[i];
            }

            // 구글 스프레드 시트 데이터 디버그
            foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }
        }
    }
}


