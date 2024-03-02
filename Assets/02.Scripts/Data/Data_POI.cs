using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class Data_POI : MonoBehaviour
{
    public Contents_POI POIcontainer;
    public GameObject POIprefabs;

    public string WebRink = "";
    
    public void GetData()
    {
        StartCoroutine(requestCoroutine(WebRink));
    }

    IEnumerator requestCoroutine(string POIlink)
    {
        //웹 요청 생성
        UnityWebRequest WebData = UnityWebRequest.Get(POIlink);

        //웹 요청 응답 대기
        yield return WebData.SendWebRequest();


        // 웹 요층 결과에 따른 처리
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success:
                //성공 : 데이터 처리
                if (WebData.isDone) // 웹 요청이 완료되었는지 확인
                {
                    SpreadSpilt(WebData.downloadHandler.text);
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

    private void SpreadSpilt(string text)
    {
        string[] rows;
        string[] columns;

        rows = text.Split('\n');

        POIcontainerLength(rows.Length);

        for (int i = 0; i < rows.Length; i++)
        {
            columns = rows[i].Split("\t");

            for (int j = 0; j < columns.Length; j++)
            {
                //ContentsData에 열데이터 저장
                ContentsData contentsData = new ContentsData();

                contentsData.number = columns[0];
                contentsData.contentsname = columns[1];
                contentsData.description = columns[2];
                contentsData.latitude = columns[3];
                contentsData.longitude = columns[4];
                contentsData.guide = columns[5];

                //poi.contentsdata 배열에 추가
                POIcontainer.contentsdata[i] = contentsData;
            }
/*            foreach (var column in columns)
            {
                Debug.Log("line: " + i + " " + column);
            }*/
        }

    }


    private void POIcontainerLength(int length)
    {
        if (POIcontainer == null || POIcontainer.contentsdata.Length != length)
        {
            POIcontainer.contentsdata = new ContentsData[length];
        }
        else
        {
            Debug.Log("데이터 배열의 크기가 할당되어 있습니다.");
            return;
        }
    }

}
