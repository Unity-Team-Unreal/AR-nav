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
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera, none }     //종류별 분류

    CategoryState thisState;   //POI버블이 어떤 카테고리에 속하는지

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //마커별 이미지
    Button markerButton;
    Button descriptBoxButton;
    Image image;

    [SerializeField] Mark thisMark; //이 마커 인스턴스의 종류

    [Header("지도")]
    RawImage Map;


    [Header("POI 데이터 정보")]
    [HideInInspector]public POIData thisData;

    CategoryState lastState;    //카테고리 변경 여부를 확인하기 위해서

    [Header("UI컨트롤러")] UIController uIController;

    private void Awake()
    {

        Map = GameObject.Find("StaticMapImage")
            .GetComponent<RawImage>();  

        image = GetComponent<Image>();
        markerButton = GetComponent<Button>();
        descriptBoxButton = GameObject.Find("NavButton").GetComponent<Button>();

        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        lastState = SellectCategory.state;  //현재 선택된 카테고리를 마지막 카테고리로 지정


    }



    public void MarkerStart()
    {
        MarkerCategorySellect(thisData.Category());
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
                thisMark= Mark.none; break;

        }


        MarkImageSellect();
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
            case Mark.none:
                image.sprite = MarkImage[7]; break;

        }


        markerButton.onClick.AddListener(onClickEventOpenDesBox);  //마커를 누르면 POI 상세 설명창이 올라온다.
        descriptBoxButton.onClick.AddListener(onClickEventOpenPathBox);

    }

    void onClickEventOpenDesBox()
    {
        if (thisData.Number() == 0) return;

        uIController.DescriptBoxOn(thisData, thisMark == Mark.Camera || thisMark == Mark.Dosent);

    }
    void onClickEventOpenPathBox()
    {
        if (thisData.Number() == 0) return;

        uIController.PathBoxOn(thisData);

    }
    void Update()
    {
        if(SellectCategory.state!=lastState) bubbleOnOff();  //현재 카테고리와 마지막으로 저장된 카테고리가 다르면

        transform.localScale = new Vector3( 1/Map.transform.localScale.x, 1 / Map.transform.localScale.y,1);
    }

    public void bubbleOnOff()  //POI 마커의 표시 여부를 가리는 메서드
    {
        if (thisMark == Mark.none) return;
        lastState = SellectCategory.state;
        //이 POI의 카테고리와 활성화된 카테고리가 동일 또는 모두 보기 카테고리라면 버튼 활성화
        if (SellectCategory.state != CategoryState.All)
        {
            if (thisState != SellectCategory.state)
            {
                markerButton.interactable = false;
            }

            else
            {
                markerButton.interactable = true;
                markerButton.gameObject.transform.SetAsLastSibling();

            }
        }

        //아니라면 버튼 비활성화
        else 
        {
            markerButton.interactable = true;
        }
    }

}
