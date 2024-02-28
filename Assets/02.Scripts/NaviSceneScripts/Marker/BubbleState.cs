using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    /// <summary>
    /// POI�� ������ ���� ��Ŀ�� ����� ���ϰ�, �ϴ� �ٿ��� ī�װ� ����� ������ ��Ŀ�� on, off�� �� �ִ� ��ũ��Ʈ
    /// </summary>
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera, myLocation }     //������ �з�

    CategoryState thisState;   //POI������ � ī�װ��� ���ϴ���

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //��Ŀ�� �̹���

    [Header("POI������ ���޹��� �̺�Ʈ�� �ִ� ��ư��")]
    Button markerButton;
    Button descriptBoxButton;

    
    Image image;    //��Ŀ�� �̺�Ʈ

    Mark thisMark;     //�� ��Ŀ �ν��Ͻ��� ����

    [Header("����")]
    RawImage Map;


    [Header("POI ������ ����")]
    [HideInInspector]public POIData thisData;

    CategoryState lastState;    //ī�װ� ���� ���θ� Ȯ���ϱ� ���ؼ�

    [Header("UI��Ʈ�ѷ�")]
    UIController uIController;

    private void Awake()
    {

        Map = GameObject.Find("StaticMapImage")
            .GetComponent<RawImage>();  
        image = GetComponent<Image>();
        markerButton = GetComponent<Button>();
        descriptBoxButton = GameObject.Find("NavButton").GetComponent<Button>();
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();
        //������Ʈ ����

        lastState = SellectCategory.state;  //���� ���õ� ī�װ��� ������ ī�װ��� ����


    }



    public void MarkerStart()
    {
        MarkerCategorySellect(thisData.Category()); //MarkerStartȣ��� POI�������� ī�װ��� �Ű������� ����(MarkerInstantiate���� POIdata���� �� ȣ����)
    }

    void MarkerCategorySellect(string category) //POIdata ���� ī�װ��� ���� ��Ŀ�� ī�װ��� ���ϴ� �޼���
    {

        switch (category)
        {
            case "ī��":
                thisMark = Mark.Camera; break;
            case "�Ĵ�":
                thisMark = Mark.Park; break;
            case "�������":
                thisMark= Mark.Subway; break;
            case "-":
                thisMark= Mark.myLocation; break;

        }


        MarkImageSellect();     //��Ŀ�� ������ ���� �� ��Ŀ�� �̹����� ����
    }

    void MarkImageSellect()     //��Ŀ�� ������ ���� �̹����� �����ϴ� �޼���
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


        markerButton.onClick.AddListener(onClickEventOpenDesBox);  //��Ŀ�� ������ �� ����â�� �ö���� �̺�Ʈ�� �߰�

        descriptBoxButton.onClick.AddListener(onClickEventOpenPathBox); //�� ���� â���� ��ư�� ������ ���ã�� â�� �ߴ� �̺�Ʈ�߰�

    }

    void onClickEventOpenDesBox()
    {

        if (thisData.Number() == 0) return; //�� ��ġ ��Ŀ�� ����

        uIController.DescriptBoxOn(thisData, thisMark == Mark.Camera || thisMark == Mark.Dosent);   // ī�޶� �Ǵ� ����Ʈ ��Ŀ���� ���ο� �Բ� ����

    }
    void onClickEventOpenPathBox()
    {
        if (thisData.Number() == 0) return; //�� ��ġ ��Ŀ�� ����

        uIController.PathBoxOn(thisData);

    }
    void Update()
    {
        if(SellectCategory.state!=lastState) bubbleOnOff();  //���� ī�װ��� ���������� ����� ī�װ��� �ٸ���

        transform.localScale = new Vector3( 1/Map.transform.localScale.x, 1 / Map.transform.localScale.y,1);
        //������ ũ�⿡ ���� ��Ŀ ũ�⵵ ���ϰ� ����
    }

    public void bubbleOnOff()  //POI ��Ŀ�� ǥ�� ���θ� ������ �޼���
    {
        if (thisMark == Mark.myLocation) return;    //�� ��ġ ��Ŀ�� ����

        lastState = SellectCategory.state;  //������ ī�װ��� ���� ī�װ��� ����

        if (SellectCategory.state != CategoryState.All) //���� ī�װ��� "��� ����"�� �ƴ� ���
        {
            if (thisState != SellectCategory.state)     //�׷��鼭 �� ��Ŀ�� ī�װ� �ٸ���
            {
                markerButton.interactable = false;  //��ư ��Ȱ��ȭ
            }

            else
            {
                markerButton.interactable = true;   //ī�װ��� ���ٸ� ��ư Ȱ��ȭ
                markerButton.gameObject.transform.SetAsLastSibling();   //Ȱ��ȭ�� ��Ŀ�� ��Ȱ�� ��Ŀ�� �������� �ʵ��� ����

            }
        }


        else markerButton.interactable = true;        //ī�װ��� "��� ����"��� ��ư ��Ȱ��ȭ
    }

}
