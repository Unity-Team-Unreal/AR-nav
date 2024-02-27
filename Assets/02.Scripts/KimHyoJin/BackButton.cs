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
    UIController uIController;
    Button button;
    [SerializeField] string MainSceneName;

    private void Awake()
    {
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        button = GetComponent<Button>();
        
        button.onClick.AddListener(BackbuttonEvnet);
    }

    public void BackbuttonEvnet()
    {
        if(uIController.pathBoxIsActivate) uIController.PathBoxOff();

        else if (uIController.pointDesIsActivate) uIController.DescriptBoxOff();

        else BackToHome();

    }  //설명상자가 올라와있을 때 누르면 설명상자 닫고 아니라면 홈화면으로 옮기기

    void BackToHome()   //길찾기 화면에서 메인화면으로 가는 메서드
    {
        SceneManager.LoadScene(MainSceneName);
    }   

}
