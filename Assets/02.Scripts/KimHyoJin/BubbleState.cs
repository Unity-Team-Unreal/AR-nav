using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    /// <summary>
    /// POI의 종류에 따라 마커의 모양을 정하고, 하단 바에서 카테고리 토글을 누르면 마커를 on, off할 수 있는 스크립트
    /// </summary>
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera }     //종류별 분류

    CategoryState thisState;   //POI버블이 어떤 카테고리에 속하는지

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //마커별 이미지
    Button button;
    Image image;

    [SerializeField] Mark thisMark; //이 마커 인스턴스의 종류

    [Header("POI 데이터 정보")]
    string pointName;
    string pointDescription;
    float pointLatituede;
    float pointLongituede;

    CategoryState lastState;    //카테고리 변경 여부를 확인하기 위해서

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(GameObject.Find("PointDescriptionPage")
            .GetComponent<PointDesBoxScript>().DescriptionBoxActivate);   //마커를 누르면 POI 상세 설명창이 올라온다.

        lastState = SellectCategory.state;  //현재 선택된 카테고리를 마지막 카테고리로 지정

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
        }
    }

    private void Start()
    {
        MarkImageSellect();
    }

    void Update()
    {
        if(SellectCategory.state!=lastState) bubbleOnOff();  //현재 카테고리와 마지막으로 저장된 카테고리가 다르면
    }

    public void bubbleOnOff()  //POI 마커의 표시 여부를 가리는 메서드
    {
        lastState = SellectCategory.state;
        //이 POI의 카테고리와 활성화된 카테고리가 동일 또는 모두 보기 카테고리라면 버튼 활성화
        if (SellectCategory.state != CategoryState.All)
        {
            if (thisState != SellectCategory.state) button.interactable = false;

            else button.interactable = true;
        }

        //아니라면 버튼 비활성화
        else button.interactable = true;
    }

}
