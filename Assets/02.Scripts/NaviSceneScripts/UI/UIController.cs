using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{   

    /// <summary>
    /// UI ������ �Ѱ��ϴ� ��ũ��Ʈ
    /// </summary>

    [Header("��Ʈ���� UI��")]
    [SerializeField] PathFinderButtonScript pathFinderBox;
    [SerializeField] PointDesBoxScript pointDesBox;
    [SerializeField] DesImageScript desImage;
    [SerializeField] PathBoxScript pathBox;
    [SerializeField] GameObject categoryBox;

    public int ClickedNum = 0;

    [HideInInspector] public bool pointDesIsActivate;   //��ã�������� Ȯ��, �ڷΰ��� ��ư���� ������ ���̱� ������ public
    [HideInInspector] public bool pathBoxIsActivate;    //����UI Ȱ������. �ڷΰ��� ��ư���� ������ ���̱� ������ public


    public void CategoryBoxOn()   //���� ȭ�� �ϴ��� ī�װ� �� Ȱ��ȭ
    {
        categoryBox.SetActive(true);
    }
    public void CategoryBoxOff()    //���� ȭ�� �ϴ��� ī�װ� �� ��Ȱ��ȭ
    {
        categoryBox.SetActive(false);
    }
    public void DescriptBoxOn(POIData data, bool isGetImage)    //POI ���� �� �̹��� ���θ� ���޹޾� �󼼼��� �ڽ� Ȱ��ȭ
    {

        if (isGetImage) //�̹����� ���ٸ�
        {
            desImageBoxOn(data);    //�̹��� Ȱ��ȭ
            pointDesBox.DescriptionBoxActivate(data);   //�󼼼��� �ڽ� Ȱ��ȭ
        }

        else        //�ƴ϶��
        {
            desImageBoxOn();    //�� �̹��� Ȱ��ȭ
            pointDesBox.DescriptionBoxActivate(data);   //�󼼼��� �ڽ� Ȱ��ȭ
        }

        pointDesIsActivate = true;  //�󼼼����� ���� true��

    }
    public void DescriptBoxOff()    //�󼼼��� �ڽ� ��Ȱ��ȭ
    {
        desImageBoxOff();   //�󼼼��� �̹��� ����

        pointDesBox.DescriptionBoxDeactivate();     //�󼼼��� �ڽ� ����

        pointDesIsActivate = false;  //�󼼼����� ���� false

    }
    public void desImageBoxOn(POIData data)     //�󼼼��� �̹��� ����
    {
        desImage.desBoxEneable(data);
    }
    public void desImageBoxOn()     //�󼼼��� �� �̹��� ����
    {
        desImage.desBoxEneableButNoImage();
    }
    public void desImageBoxOff()    //�󼼼��� �̹��� ����
    {
        desImage.desBoxDisable();
    }
    public void PathBoxOn(POIData data)     //��ã�� �ڽ� Ȱ��ȭ
    {
        Debug.Log(data.Name());

        DescriptBoxOff();   //�󼼼��� �ڽ� ����

        CategoryBoxOff();   //�ϴ� ī�װ��� ��Ȱ��ȭ

        desImageBoxOn();    //�� �̹��� ����

        pathBox.PathBoxActivate(data);   //��ã�� ��� �ڽ� Ȱ��ȭ

        pathFinderBox.PathBoxActivate();      //��ã�� �ϴ� �ڽ� Ȱ��ȭ

        pathBoxIsActivate = true;   //��ã�� �� ���� true
    }
    public void PathBoxOff()    //��ã�� �ڽ� ��Ȱ��ȭ
    {
        desImageBoxOff();   //�� �̹��� ��Ȱ��ȭ

        pathBox.PathBoxDeactivate();    //��ã�� ��� �ڽ� ��Ȱ��ȭ

        pathFinderBox.PathBoxDeActivate();      //��ã�� �ϴ� �ڽ� ��Ȱ��ȭ

        pathBoxIsActivate = false;   //��ã�� �� ���� false

    }


}
