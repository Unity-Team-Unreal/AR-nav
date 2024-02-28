using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] PathFinderButtonScript pathFinderBox;
    [SerializeField] PointDesBoxScript pointDesBox;
    [SerializeField] DesImageScript desImage;
    [SerializeField] PathBoxScript pathBox;
    [SerializeField] GameObject categoryBox;


    [HideInInspector] public bool pointDesIsActivate;   //길찾기중인지 확인, 뒤로가기 버튼에서 참조할 것이기 때문에 public
    [HideInInspector] public bool pathBoxIsActivate;    //설명UI 활성여부. 뒤로가기 버튼에서 참조할 것이기 때문에 public


    public void CategoryBoxOn()
    {
        categoryBox.SetActive(true);
    }
    public void DescriptBoxOn(POIData data, bool isGetImage)
    {

        if (isGetImage)
        {
            desImageBoxOn(data);
            pointDesBox.DescriptionBoxActivate(data);
        }

        else
        {
            desImageBoxOn();
            pointDesBox.DescriptionBoxActivate(data);
        }

        pointDesIsActivate = true;

    }
    public void desImageBoxOn(POIData data)
    {
        desImage.desBoxEneable(data);
    }
    public void desImageBoxOn()
    {
        desImage.desBoxEneableButNoImage();
    }
    public void PathBoxOn(POIData data)
    {
        DescriptBoxOff();

        CategoryBoxOff();

        desImageBoxOn();

        pathBox.PathBoxActivate(data);

        PathFinderBoxOn();

        pathBoxIsActivate = true;
    }
    public void PathFinderBoxOn()
    {
        pathFinderBox.PathBoxActivate();
    }
    public void DescriptBoxOff()
    {
        desImageBoxOff();

        pointDesBox.DescriptionBoxDeactivate();

        pointDesIsActivate = false;

    }
    public void desImageBoxOff()
    {
        desImage.desBoxDisable();
    }
    public void PathBoxOff()
    {
        desImageBoxOff();

        pathBox.PathBoxDeactivate();

        PathFinderBoxOff();

        pathBoxIsActivate = false;

    }
    public void CategoryBoxOff()
    {
        categoryBox.SetActive(false);
    }
    public void PathFinderBoxOff()
    {
        pathFinderBox.PathBoxDeActivate();
    }

}
