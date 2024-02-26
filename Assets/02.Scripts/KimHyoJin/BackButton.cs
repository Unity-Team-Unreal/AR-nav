using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    /// <summary>
    /// 2D지도, 길찾기 화면에서 사용할 뒤로가기 버튼에 대한 스크립트
    /// </summary>

    [SerializeField]PointDesBoxScript pointdesBox;   //POI 설명상자 스크립트를 받아온다.
    [SerializeField] PathBoxScript pathBox;   //POI 길찾기 상자 스크립트를 받아온다.
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
      button.onClick.AddListener(BackbuttonEvnet);
    }

    public void BackbuttonEvnet()
    {
        if (pathBox.pathBoxIsActivate)
        {
            CancelSearching();
        }
        if(pointdesBox.pointDesIsActivate)  BackToNav();
        else BackToHome();
    }  //설명상자가 올라와있을 때 누르면 설명상자 닫고 아니라면 홈화면으로 옮기기

    void BackToHome()   //길찾기 화면에서 메인화면으로 가는 메서드
    {
        SceneManager.LoadScene("1.Home Page");
    }   

    void BackToNav()    //상세설명 상자에서 마커 누르기 전으로 돌아가는 메서드
    {
        pointdesBox.DescriptionBoxDeactivate();
    }

    void CancelSearching()  //출발지&목적지 UI 에서 상세설명으로 돌아가는 메서드
    {
        pathBox.PathBoxDeactivate();
    }
}
