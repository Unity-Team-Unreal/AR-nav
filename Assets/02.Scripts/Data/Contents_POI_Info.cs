using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Contents_POI_Info : MonoBehaviour
{
    private static Contents_POI_Info instance;
    public static Contents_POI_Info Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<Contents_POI_Info>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    return null;
                }
            }
            return instance;
        }
    } 


   
    [Header("POI 컨텐츠 링크")]
    [SerializeField] private string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:F6"; // 포토존 POI 데이터링크
    [SerializeField] private string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:F6"; // 도슨트 POI 데이터링크

    //데이터 배열
    [SerializeField] private string[] rowsData;  // 행 데이터를 저장하는 배열
    [SerializeField] private string[] columnsData;   // 열 데이터를 저장하는 배열
       
    [Header("컨텐츠 이미지 배열")]
    [SerializeField] private Texture2D[] Photozoneimage; 
    [SerializeField] private Texture2D[] Docentimage;

    [Header("컨텐츠 이미지 경로")]
    [SerializeField] private string Photo_floder = "Photoimage";
    [SerializeField] private string Docent_floder = "Docentimage";

    [Header("POI 데이터")]
    [SerializeField] private Contents_POI photozoneData;
    [SerializeField] private Contents_POI DocentData;

    [Header("컨텐츠 생성 오브젝트")]
    [SerializeField] private GameObject contentsDataPfb;

    [Header("컨텐츠 생성 위치")]
    [SerializeField] private Transform Photoscrolltransform;
    [SerializeField] private Transform Docentscrolltransform;


    private void Awake()
    {
        gameObject.SetActive(true);

        //싱글톤 설정
        var objs = FindObjectsOfType<Contents_POI_Info>();

        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        //이미지 로드
        Photozoneimage = Resources.LoadAll<Texture2D>(Photo_floder);
        Docentimage = Resources.LoadAll<Texture2D>(Docent_floder);

        //데이터 불러오기 시작
        StartCoroutine(requestCoroutine(photozoneData, PhotozonewebURL, Photozoneimage, Photoscrolltransform)); // 코루틴 시작 
        StartCoroutine(requestCoroutine(DocentData, DocentPOIwebURL, Docentimage, Docentscrolltransform));

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    /// <summary>
    /// 코루틴 : 웹 데이터 요청 및 처리
    /// </summary>
    /// <param name="contentspoi">POI 데이터 구조체</param>
    /// <param name="link">웹 데이터 링크</param>
    /// <param name="texture2D">이미지 배열</param>
    /// <param name="transform">스크롤 뷰</param>
    /// <returns></returns>
    IEnumerator requestCoroutine(Contents_POI contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        //웹 요청 생성
        UnityWebRequest WebData = UnityWebRequest.Get(link); 

        //웹 요청 응답 대기
        yield return WebData.SendWebRequest(); 


        // 웹 요층 결과에 따른 처리
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success:
                //성공 : 데이터 처리
                if (WebData.isDone) // 웹 요청이 완료되었는지 확인
                {
                    POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform);
                }
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError: 
            case UnityWebRequest.Result.DataProcessingError:
                //실패 : 에러 처리
                Debug.LogError(WebData.error);
                break;
        }
    }

    /// <summary>
    /// 데이터 파싱 및 저장
    /// </summary>
    /// <param name="tsv">데이터 문자열</param>
    /// <param name="poi">POI 데이터 구조체</param>
    /// <param name="texture2D">이미지 배열</param>
    /// <param name="transform">스크롤 뷰</param>
    void POIDB(string tsv, Contents_POI poi, Texture2D[] texture2D, Transform transform)
    {
        // 행 데이터 분리
        rowsData = tsv.Split('\n');
        
        // POI 데이터 구조체 할당
        poi.contentsdata = new ContentsData[rowsData.Length];
        

        for (int i = 0; i < rowsData.Length; i++)
        {
            // 열 데이터 분리
            columnsData = rowsData[i].Split('\t'); // 행 데이터를 탭('/t') 기준으로 나눠서 열 데이터로 저장

            // POI 프리팹 생성
            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);


            for(int j = 0;  j < columnsData.Length; j++)
            {
                //ContentsData에 열데이터 저장
                ContentsData contentsData = new ContentsData();

                contentsData.number = columnsData[0];
                contentsData.contentsname = columnsData[1];
                contentsData.description = columnsData[2];
                contentsData.latitude = columnsData[3];
                contentsData.longitude = columnsData[4];
                contentsData.guide = columnsData[5];
                contentsData.Image = texture2D[i];

                //poi.contentsdata 배열에 추가
                poi.contentsdata[i] = contentsData;
            }
            
            // POI 프리팹 초기화
            poiPrefabsIntance.GetComponent<POIPrefabs>().Init(poi.contentsdata[i]);


            //구글 스프레드 시트 데이터 디버깅
            /*foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }*/
        }
    }

    /// <summary>
    /// 씬 로딩 후 처리
    /// </summary>
    /// <param name="scene">로딩된 씬 정보</param>
    /// <param name="mode">씬 로딩 정보</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //씬 이름에 따라 게임 오브젝트 활성화 여부 설정
        if (scene.name == "1_3.AR_Contents_Page")
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}

