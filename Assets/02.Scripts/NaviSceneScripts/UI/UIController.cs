using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{   

    /// <summary>
    /// UI 조작을 총괄하는 스크립트
    /// </summary>

    [Header("컨트롤할 UI들")]
    [SerializeField] PathFinderButtonScript pathFinderBox;
    [SerializeField] PointDesBoxScript pointDesBox;
    [SerializeField] DesImageScript desImage;
    [SerializeField] PathBoxScript pathBox;
    [SerializeField] GameObject categoryBox;

    public int ClickedNum = 0;

    [HideInInspector] public bool pointDesIsActivate;   //길찾기중인지 확인, 뒤로가기 버튼에서 참조할 것이기 때문에 public
    [HideInInspector] public bool pathBoxIsActivate;    //설명UI 활성여부. 뒤로가기 버튼에서 참조할 것이기 때문에 public


    public void CategoryBoxOn()   //지도 화면 하단의 카테고리 바 활성화
    {
        categoryBox.SetActive(true);
    }
    public void CategoryBoxOff()    //지도 화면 하단의 카테고리 바 비활성화
    {
        categoryBox.SetActive(false);
    }
    public void DescriptBoxOn(POIData data, bool isGetImage)    //POI 정보 및 이미지 여부를 전달받아 상세설명 박스 활성화
    {

        if (isGetImage) //이미지를 띄운다면
        {
            desImageBoxOn(data);    //이미지 활성화
            pointDesBox.DescriptionBoxActivate(data);   //상세설명 박스 활성화
        }

        else        //아니라면
        {
            desImageBoxOn();    //빈 이미지 활성화
            pointDesBox.DescriptionBoxActivate(data);   //상세설명 박스 활성화
        }

        pointDesIsActivate = true;  //상세설명중 여부 true로

    }
    public void DescriptBoxOff()    //상세설명 박스 비활성화
    {
        desImageBoxOff();   //상세설명 이미지 끄기

        pointDesBox.DescriptionBoxDeactivate();     //상세설명 박스 끄기

        pointDesIsActivate = false;  //상세설명중 여부 false

    }
    public void desImageBoxOn(POIData data)     //상세설명 이미지 띄우기
    {
        desImage.desBoxEneable(data);
    }
    public void desImageBoxOn()     //상세설명 빈 이미지 띄우기
    {
        desImage.desBoxEneableButNoImage();
    }
    public void desImageBoxOff()    //상세설명 이미지 끄기
    {
        desImage.desBoxDisable();
    }
    public void PathBoxOn(POIData data)     //길찾기 박스 활성화
    {
        Debug.Log(data.Name());

        DescriptBoxOff();   //상세설명 박스 끄기

        CategoryBoxOff();   //하단 카테고리바 비활성화

        desImageBoxOn();    //빈 이미지 띄우기

        pathBox.PathBoxActivate(data);   //길찾기 상단 박스 활성화

        pathFinderBox.PathBoxActivate();      //길찾기 하단 박스 활성화

        pathBoxIsActivate = true;   //길찾기 중 여부 true
    }
    public void PathBoxOff()    //길찾기 박스 비활성화
    {
        desImageBoxOff();   //빈 이미지 비활성화

        pathBox.PathBoxDeactivate();    //길찾기 상단 박스 비활성화

        pathFinderBox.PathBoxDeActivate();      //길찾기 하단 박스 비활성화

        pathBoxIsActivate = false;   //길찾기 중 여부 false

    }


}
