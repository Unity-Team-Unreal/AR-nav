using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] PointDesBoxScript pointDesBox;
    [SerializeField] DesImageScript desImage;
    [SerializeField] PathBoxScript pathBox;


    [HideInInspector] public bool pointDesIsActivate;   //��ã�������� Ȯ��, �ڷΰ��� ��ư���� ������ ���̱� ������ public
    [HideInInspector] public bool pathBoxIsActivate;    //����UI Ȱ������. �ڷΰ��� ��ư���� ������ ���̱� ������ public


    public void DescriptBoxOn(POIData data, bool isGetImage)
    {

        if (isGetImage)
        {
            desImage.desBoxEneable();
            pointDesBox.DescriptionBoxActivate(data);
        }

        else
        {
            desImage.desBoxEneableButNoImage();
            pointDesBox.DescriptionBoxActivate(data);
        }

        pointDesIsActivate = true;

    }

    public void PathBoxOn(POIData data)
    {
        DescriptBoxOff();

        desImage.desBoxEneableButNoImage();

        pathBox.PathBoxActivate(data);

        pathBoxIsActivate = true;
    }

    public void DescriptBoxOff()
    {
        desImage.desBoxDisable();

        pointDesBox.DescriptionBoxDeactivate();

        pointDesIsActivate = false;

    }


    public void PathBoxOff()
    {
        desImage.desBoxDisable();

        pathBox.PathBoxDeactivate();

        pathBoxIsActivate = false;

    }

    
}
