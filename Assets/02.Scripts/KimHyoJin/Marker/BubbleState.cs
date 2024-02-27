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
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera, none }     //������ �з�

    CategoryState thisState;   //POI������ � ī�װ��� ���ϴ���

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //��Ŀ�� �̹���
    Button markerButton;
    Button descriptBoxButton;
    Image image;

    [SerializeField] Mark thisMark; //�� ��Ŀ �ν��Ͻ��� ����

    [Header("����")]
    RawImage Map;


    [Header("POI ������ ����")]
    [HideInInspector]public POIData thisData;

    CategoryState lastState;    //ī�װ� ���� ���θ� Ȯ���ϱ� ���ؼ�

    [Header("UI��Ʈ�ѷ�")] UIController uIController;

    private void Awake()
    {

        Map = GameObject.Find("StaticMapImage")
            .GetComponent<RawImage>();  

        image = GetComponent<Image>();
        markerButton = GetComponent<Button>();
        descriptBoxButton = GameObject.Find("NavButton").GetComponent<Button>();

        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        lastState = SellectCategory.state;  //���� ���õ� ī�װ��� ������ ī�װ��� ����


    }



    public void MarkerStart()
    {
        MarkerCategorySellect(thisData.Category());
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
                thisMark= Mark.none; break;

        }


        MarkImageSellect();
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
            case Mark.none:
                image.sprite = MarkImage[7]; break;

        }


        markerButton.onClick.AddListener(onClickEventOpenDesBox);  //��Ŀ�� ������ POI �� ����â�� �ö�´�.
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
        if(SellectCategory.state!=lastState) bubbleOnOff();  //���� ī�װ��� ���������� ����� ī�װ��� �ٸ���

        transform.localScale = new Vector3( 1/Map.transform.localScale.x, 1 / Map.transform.localScale.y,1);
    }

    public void bubbleOnOff()  //POI ��Ŀ�� ǥ�� ���θ� ������ �޼���
    {
        if (thisMark == Mark.none) return;
        lastState = SellectCategory.state;
        //�� POI�� ī�װ��� Ȱ��ȭ�� ī�װ��� ���� �Ǵ� ��� ���� ī�װ���� ��ư Ȱ��ȭ
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

        //�ƴ϶�� ��ư ��Ȱ��ȭ
        else 
        {
            markerButton.interactable = true;
        }
    }

}
