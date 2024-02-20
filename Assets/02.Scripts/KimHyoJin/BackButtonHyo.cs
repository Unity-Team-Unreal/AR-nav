using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonHyo : MonoBehaviour
{
    [SerializeField]PointDesBoxScript pointdesBox;   //POI 설명상자 스크립트를 받아온다.
    [SerializeField]PathBoxScript pathbox;  //길찾기 상자 스크립트를 받아온다.
    //test
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void BackbuttonEvnet()
    {
        if (pathbox.isActivate)
        {
            CancelSearching();
        }
        if(pointdesBox.isActivate)  BackToNav();
        else BackToHome();
    }  //설명상자가 올라와있을 때 누르면 설명상자 닫고 아니라면 홈화면으로 옮기기

    void BackToHome()
    {
        Debug.Log("홈 화면으로 이동하는 씬 전환 코드");
    }

    void BackToNav()
    {
        pointdesBox.DescriptionBoxDeactivate();
    }

    void CancelSearching()
    {
        pathbox.PathBoxDeactivate();
    }
}
