using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PathBoxScript : MonoBehaviour
{
    /// <summary>
    /// 마커를 눌렀을 때 나오는 설명 페이지 하단의 길찾기 버튼을 눌렀을 때, 상단에 추가되는 출발지/목적지 UI를 구현하는 스크립트
    /// </summary>

    [Header("시작지점/끝지점")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;

    Animator _animator; //애니메이션으로 UI의 움직임을 구현할 예정


    double latitude = 0f;
    double longitude = 0f;
    //시작점의 경위도를 받아올 예정

     GPS gps;
    //GPS호출이 필요하여 GPS도 선언

    void Awake()
    {
        gps = FindObjectOfType<GPS>().GetComponent<GPS>();
        _animator = GetComponent<Animator>();

    }

    public  void PathBoxActivate(POIData data)   //길찾기 UI를 활성화
    {
        gps.Request();  //GPS 요청 및 수락시 실행

        if (gps.GetMyLocation(ref latitude, ref longitude))     //GPS를 받아오는데 성공했다면
        {
            start.text = $"현재 위치";
            goal.text = data.Name();
            _animator.Play("Play");
            //길찾기 박스를 띄우고 출발지와 목적지 기입
        }

        else
        {
            start.text = $"현재 위치";
            goal.text = data.Name();
            _animator.Play("Play");
            //길찾기 박스를 띄우고 출발지와 목적지 기입
        }

        // 현 위치 경위도에 따라 출발지를 표기하는 기능은 아직 미구현

    }


    public  void PathBoxDeactivate() //길찾기 UI 비활성화
    {
        _animator.Play("Reverse");
    }

}
