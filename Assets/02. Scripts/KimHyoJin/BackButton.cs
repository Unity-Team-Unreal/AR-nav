using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]PointDesAnimeScript pointdes;   //POI 설명상자 스크립트를 받아온다.
    [SerializeField]PassBoxScript passbox;  //길찾기 상자 스크립트를 받아온다.

    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        pointdes = pointdes.GetComponent<PointDesAnimeScript>();

    }

    public void BackbuttonEvnet()
    {
        if (passbox.isSearching)
        {
            CancelSearching();
            BackToNav();
        }
        else if(pointdes.isActivate)  BackToNav();
        else BackToHome();
    }  //설명상자가 올라와있을 때 누르면 설명상자 닫고 아니라면 홈화면으로 옮기기

    void BackToHome()
    {
        Debug.Log("홈 화면으로 이동하는 씬 전환 코드");
    }

    void BackToNav()
    {
        pointdes.DescriptionBoxDeactivate();
    }

    void CancelSearching()
    {
        passbox.PassBoxDeactivate();
    }
}
