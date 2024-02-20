using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera }

    CategoryState thisState;   //POI버블이 어떤 카테고리에 속하는지

    [SerializeField]Sprite[] MarkImage = new Sprite[7];
    Button button;
    Image image;

    [SerializeField] Mark thisMark;

    string pointName;
    string pointDescription;
    float pointLatituede;
    float pointLongituede;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(GameObject.Find("PointDescriptionPage")
            .GetComponent<PointDesBoxScript>().DescriptionBoxActivate);   //POI 버블 누르면 POI 설명창이 올라온다.


    }

    void MarkImageSellect()
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
        bubbleOnOff();  //POI 버블의 표시 여부를 가리는 메서드
    }

    public void bubbleOnOff()
    {   
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
