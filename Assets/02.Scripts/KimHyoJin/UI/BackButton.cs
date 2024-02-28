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

    UIController uIController;  //ui를 움직이는 UI컨트롤러
    Button button;  //뒤로가기 버튼

    [SerializeField] string MainSceneName;  //메인 신으로 넘어가기 위한 씬 이름

    private void Awake()
    {
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        button = GetComponent<Button>();
        
        button.onClick.AddListener(BackbuttonEvnet);    //뒤로가기 버튼에 이벤트 추가
    }

    public void BackbuttonEvnet()
    {
        if (uIController.pathBoxIsActivate)     //경로 찾기 실행중일 때 뒤로가기 버튼을 누르면
        {
            uIController.PathBoxOff();      //경로찾기 박스 비활성화
            uIController.CategoryBoxOn();   //하단 카테고리 박스 활성화
        }

        else if (uIController.pointDesIsActivate) uIController.DescriptBoxOff();
        //상세설명 실행중일 때 뒤로가기 버튼을 누르면 상세설명 박스 비활성화

        else BackToHome();
        //기본 상태에서 뒤로가기 버튼을 누르면 메인 신으로 넘어가기


    }

    void BackToHome()   //길찾기 화면에서 메인화면으로 가는 메서드
    {
        SceneManager.LoadScene(MainSceneName);
    }   

}
