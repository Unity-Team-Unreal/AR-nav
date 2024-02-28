using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    /// <summary>
    /// POI의 종류에 따라 마커의 모양을 정하고, 하단 바에서 카테고리 토글을 누르면 마커를 on, off할 수 있는 스크립트
    /// </summary>
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera, myLocation }     //종류별 분류

    CategoryState thisState;   //POI버블이 어떤 카테고리에 속하는지

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //마커별 이미지

    [Header("POI정보를 전달받을 이벤트가 있는 버튼들")]
    Button markerButton;
    Button descriptBoxButton;

    
    Image image;    //마커의 이벤트

    Mark thisMark;     //이 마커 인스턴스의 종류

    [Header("지도")]
    RawImage Map;


    [Header("POI 데이터 정보")]
    [HideInInspector]public POIData thisData;

    CategoryState lastState;    //카테고리 변경 여부를 확인하기 위해서

    [Header("UI컨트롤러")]
    UIController uIController;

    private void Awake()
    {

        Map = GameObject.Find("StaticMapImage")
            .GetComponent<RawImage>();  
        image = GetComponent<Image>();
        markerButton = GetComponent<Button>();
        descriptBoxButton = GameObject.Find("NavButton").GetComponent<Button>();
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();
        //컴포넌트 연결

        lastState = SellectCategory.state;  //현재 선택된 카테고리를 마지막 카테고리로 지정


    }



    public void MarkerStart()
    {
        MarkerCategorySellect(thisData.Category()); //MarkerStart호출시 POI데이터의 카테고리를 매개변수로 시작(MarkerInstantiate에서 POIdata전달 후 호출중)
    }

    void MarkerCategorySellect(string category) //POIdata 내의 카테고리에 따라 마커의 카테고리를 정하는 메서드
    {

        switch (category)
        {
            case "카페":
                thisMark = Mark.Camera; break;
            case "식당":
                thisMark = Mark.Park; break;
            case "공공장소":
                thisMark= Mark.Subway; break;
            case "-":
                thisMark= Mark.myLocation; break;

        }


        MarkImageSellect();     //마커의 종류를 정한 뒤 마커의 이미지를 선택
    }

    void MarkImageSellect()     //마커의 종류에 따라 이미지를 결정하는 메서드
    {

        switch (thisMark)
        {
            case Mark.Park:
                thisState = CategoryState.PublicBuilding; image.sprite = MarkImage[0];
                break;
            case Mark.Hospital:
                thisState = CategoryState.Hospital; image.sprite = MarkImage[1];
                break;
            case Mark.ParkingPlace:
                thisState = CategoryState.PublicBuilding; image.sprite = MarkImage[2];
                break;
            case Mark.Subway:
                thisState = CategoryState.PublicBuilding; image.sprite = MarkImage[3];
                break;
            case Mark.Toildet:
                thisState = CategoryState.PublicBuilding; image.sprite = MarkImage[4];
                break;
            case Mark.Dosent:
                thisState = CategoryState.Photozone; image.sprite = MarkImage[5];
                break;
            case Mark.Camera:
                thisState = CategoryState.Photozone; image.sprite = MarkImage[6];
                break;
            case Mark.myLocation:
                image.sprite = MarkImage[7]; break;

        }


        markerButton.onClick.AddListener(onClickEventOpenDesBox);  //마커를 누르면 상세 설명창이 올라오는 이벤트를 추가

        descriptBoxButton.onClick.AddListener(onClickEventOpenPathBox); //상세 설명 창에서 버튼을 누르면 경로찾기 창이 뜨는 이벤트추가

    }

    void onClickEventOpenDesBox()
    {

        if (thisData.Number() == 0) return; //내 위치 마커는 제외

        uIController.DescriptBoxOn(thisData, thisMark == Mark.Camera || thisMark == Mark.Dosent);   // 카메라 또는 도슨트 마커인지 여부와 함께 전달

    }
    void onClickEventOpenPathBox()
    {
        if (thisData.Number() == 0) return; //내 위치 마커는 제외

        uIController.PathBoxOn(thisData);

    }
    void Update()
    {
        if(SellectCategory.state!=lastState) bubbleOnOff();  //현재 카테고리와 마지막으로 저장된 카테고리가 다르면

        transform.localScale = new Vector3( 1/Map.transform.localScale.x, 1 / Map.transform.localScale.y,1);
        //지도의 크기에 따라 마커 크기도 변하게 설정
    }

    public void bubbleOnOff()  //POI 마커의 표시 여부를 가리는 메서드
    {
        if (thisMark == Mark.myLocation) return;    //내 위치 마커는 제외

        lastState = SellectCategory.state;  //마지막 카테고리를 현재 카테고리로 설정

        if (SellectCategory.state != CategoryState.All) //현재 카테고리가 "모두 보기"가 아닐 경우
        {
            if (thisState != SellectCategory.state)     //그러면서 이 마커와 카테고리 다르면
            {
                markerButton.interactable = false;  //버튼 비활성화
            }

            else
            {
                markerButton.interactable = true;   //카테고리가 같다면 버튼 활성화
                markerButton.gameObject.transform.SetAsLastSibling();   //활성화된 마커가 비활성 마커에 가려지지 않도록 서정

            }
        }


        else markerButton.interactable = true;        //카테고리가 "모두 보기"라면 버튼 비활성화
    }

}
