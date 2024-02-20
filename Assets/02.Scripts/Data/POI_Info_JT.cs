using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class POI_Info_JT : MonoBehaviour
{
    //싱글톤 인스턴스
    private static POI_Info_JT instance;
    public static POI_Info_JT Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<POI_Info_JT>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<POI_Info_JT>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    //POI 데이터 링크
    [Header("컨텐츠 링크")]
    string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:E6"; // POI 데이터를 불러올 웹 
    string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:E6"; // POI 데이터를 불러올 웹 
    //gid=1564857787

    //데이터 배열
    string[] rowsData;  // 행 데이터를 저장하는 배열
    string[] columnsData;   // 열 데이터를 저장하는 배열
    
    //이미지
    [Header("컨텐츠 이미지")]
    public Texture2D[] Photozoneimage;
    public Texture2D[] Docentimage;

    //이미지 경로
    [Header("컨텐츠 이미지 경로")]
    public string Photo_floder = "Photoimage";
    public string Docent_floder = "Docentimage";

    //POI 데이터
    [Header("POI 데이터")]
    public POIJT photozone;
    public POIJT Docent;

    //컨텐츠 프리팹
    [Header("컨텐츠 생성 오브젝트")]
    public GameObject contentsDataPfb; //POI 프리팹

    //컨텐츠 생성 위치
    [Header("컨텐츠 생성 위치")]
    public Transform PhotoscrollViewContent; //포토존 스크롤 뷰의 Contents
    public Transform Docentscrolltransform; //도슨트 스크롤 뷰의 Contents


    private void Awake()
    {
        //싱글톤 설정
        var objs = FindObjectsOfType<POI_Info_JT>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //이미지 로드
        Photozoneimage = Resources.LoadAll<Texture2D>("Photoimage");
        Docentimage = Resources.LoadAll<Texture2D>("Docentimage");

        //데이터 불러오기 시작
        StartCoroutine(requestCoroutine(photozone, PhotozonewebURL, Photozoneimage, PhotoscrollViewContent)); // 코루틴 시작 
        StartCoroutine(requestCoroutine(Docent, DocentPOIwebURL, Docentimage, Docentscrolltransform));
    }


    /// <summary>
    /// 코루틴 : 웹 데이터 요청 및 처리
    /// </summary>
    /// <param name="contentspoi">POI 데이터 구조체</param>
    /// <param name="link">웹 데이터 링크</param>
    /// <param name="texture2D">이미지 배열</param>
    /// <param name="transform">스크롤 뷰</param>
    /// <returns></returns>
    IEnumerator requestCoroutine(POIJT contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        //웹 요청 생성
        UnityWebRequest WebData = UnityWebRequest.Get(link); 

        //웹 요청 응답 대기
        yield return WebData.SendWebRequest(); 


        // 웹 요층 결과에 따른 처리
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success: //성공
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError: 
            case UnityWebRequest.Result.DataProcessingError:
                //실패: 에러 처리
                Debug.LogError(WebData.error);
                break;
        }

        if (WebData.isDone) // 웹 요청이 완료되었는지 확인
        {
            POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform); // 웹 요청의 결과를 문자열로    
        }
    }

    /// <summary>
    /// 데이터 파싱 및 저장
    /// </summary>
    /// <param name="tsv">데이터 문자열</param>
    /// <param name="poi">POI 데이터 구조체</param>
    /// <param name="texture2D">이미지 배열</param>
    /// <param name="transform">스크롤 뷰</param>
    void POIDB(string tsv, POIJT poi, Texture2D[] texture2D, Transform transform)
    {
        // 행 데이터 분리
        rowsData = tsv.Split('\n');

        // 열개수 확인
        int columnSize = rowsData[0].Split("\t").Length;

        //데이터 순회
        for (int i = 0; i < rowsData.Length; i++)
        {
            // 열 데이터 분리
            columnsData = rowsData[i].Split('\t'); // 행 데이터를 탭('/t') 기준으로 나눠서 열 데이터로 저장

            // POI 프리팹 생성
            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);

            // 데이터 저장 및 초기화
            for (int j = 0; j < columnSize; j++)
            {

                ContentsData contentsData = poi.contentsdata[i];
                contentsData.contentsname = columnsData[0];
                contentsData.description = columnsData[1];
                contentsData.latitude = columnsData[2];
                contentsData.longitude = columnsData[3];
                contentsData.guide = columnsData[4];
                contentsData.Image = texture2D[i];

                poiPrefabsIntance.GetComponent<POIPrefabs>().Init(poi.contentsdata[i]);
            }

            // 구글 스프레드 시트 데이터 디버그
            //foreach (var column in columnsData)
            //{
            //    Debug.Log("line: " + i + ": " + column);
            //}
        }
    }
}

