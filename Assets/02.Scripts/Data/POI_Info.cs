using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class POI_Info : MonoBehaviour
{
    string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI 데이터를 불러올 웹 
    string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:E6"; // POI 데이터를 불러올 웹 
    //gid=1564857787

    [Header("컨텐츠 이미지")]
    public Texture2D[] Photozoneimage;
    public Texture2D[] Docentimage;

    [Header("컨텐츠 이미지 경로")]
    public string Photo_floder = "Photoimage";
    public string Docent_floder = "Docentimage";


    public POI poi;
    public POI Docent;
    
    public string[] rowsData;  // 행 데이터를 저장하는 배열
    public string[] columnsData;   // 열 데이터를 저장하는 배열

    public GameObject contentsDataPfb; //POI 프리팹
    public Transform PhotoscrollViewContent; //스크롤 뷰의 Contents를 가리키는 변수
    public Transform Docentscrolltransform; //스크롤 뷰의 Contents를 가리키는 변수


    private void Awake()
    {
        Photozoneimage = Resources.LoadAll<Texture2D>("Photoimage");
        Docentimage = Resources.LoadAll<Texture2D>("Docentimage");

        //poi.contentsdata = new ContentsData[.Length];

        //StartCoroutine(requestCoroutine(PhotozonewebURL)); // 코루틴 시작 

        StartCoroutine(requestCoroutine(poi, PhotozonewebURL, Photozoneimage, PhotoscrollViewContent)); // 코루틴 시작 
        StartCoroutine(requestCoroutine(Docent, DocentPOIwebURL, Docentimage, Docentscrolltransform));
    }


    IEnumerator requestCoroutine(POI contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        UnityWebRequest WebData = UnityWebRequest.Get(link); // 웹 요청 생성

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
            POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform); // 웹 요청의 결과를 문자열로    
        }
    }

    void POIDB(string tsv, POI poi, Texture2D[] texture2D, Transform transform)
    {
        rowsData = tsv.Split('\n'); // 문자열을 줄바꿈('n') 기준으로 나눠서 행 데이터로 저장
        int rowsSize = rowsData.Length;
        int columnSize = rowsData[0].Split("\t").Length;

        

        for (int i = 0; i < rowsSize; i++)
        {
            columnsData = rowsData[i].Split('\t'); // 행 데이터를 탭('/t') 기준으로 나눠서 열 데이터로 저장

            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);
            

            for (int j = 0; j < columnSize; j++)
            {
                ContentsData contentsData = poi.contentsdata[i];

                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = texture2D[i];
            }

            poiPrefabsIntance.GetComponent<POIPrefab>().Init(poi.contentsdata[i]);

            // 구글 스프레드 시트 데이터 디버그
            foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }
        }
    }
}


