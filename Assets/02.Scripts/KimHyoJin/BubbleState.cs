using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    /// <summary>
    /// POI�� ������ ���� ��Ŀ�� ����� ���ϰ�, �ϴ� �ٿ��� ī�װ� ����� ������ ��Ŀ�� on, off�� �� �ִ� ��ũ��Ʈ
    /// </summary>
    enum Mark { Park, Hospital, ParkingPlace, Subway, Toildet, Dosent, Camera }     //������ �з�

    CategoryState thisState;   //POI������ � ī�װ��� ���ϴ���

    [SerializeField]Sprite[] MarkImage = new Sprite[7];     //��Ŀ�� �̹���
    Button button;
    Image image;

    [SerializeField] Mark thisMark; //�� ��Ŀ �ν��Ͻ��� ����

    PointDesBoxScript desBox;
    PathBoxScript pathBox;


    [Header("POI ������ ����")]

    [HideInInspector]public POIData thisData;

    CategoryState lastState;    //ī�װ� ���� ���θ� Ȯ���ϱ� ���ؼ�

    private void Awake()
    {
        desBox = GameObject.Find("PointDescriptionPage")
            .GetComponent<PointDesBoxScript>();

        pathBox = GameObject.Find("SerachPathBox")
            .GetComponent<PathBoxScript>();

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(giveData);
        button.onClick.AddListener(desBox.DescriptionBoxActivate);   //��Ŀ�� ������ POI �� ����â�� �ö�´�.

        lastState = SellectCategory.state;  //���� ���õ� ī�װ��� ������ ī�װ��� ����

    }

    void giveData()
    {
        pathBox.data = thisData;
        desBox.data = thisData;
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
            default: thisMark = Mark.Dosent; break;
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

        }

    }

    void Update()
    {
        if(SellectCategory.state!=lastState) bubbleOnOff();  //���� ī�װ��� ���������� ����� ī�װ��� �ٸ���
    }

    public void bubbleOnOff()  //POI ��Ŀ�� ǥ�� ���θ� ������ �޼���
    {
        lastState = SellectCategory.state;
        //�� POI�� ī�װ��� Ȱ��ȭ�� ī�װ��� ���� �Ǵ� ��� ���� ī�װ���� ��ư Ȱ��ȭ
        if (SellectCategory.state != CategoryState.All)
        {
            if (thisState != SellectCategory.state)
            {
                button.interactable = false;

            }

            else button.interactable = true;
        }

        //�ƴ϶�� ��ư ��Ȱ��ȭ
        else button.interactable = true;
    }

}
