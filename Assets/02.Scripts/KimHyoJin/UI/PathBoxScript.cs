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
    /// 

    [Header("길찾기 박스 속성")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;

    Animator _animator; //애니메이션으로 UI의 움직임을 구현할 예정


    double latitude = 0f;
    double longitude = 0f;

     GPS gps;

    void Awake()
    {
        gps = FindObjectOfType<GPS>().GetComponent<GPS>();
        _animator = GetComponent<Animator>();

    }

    public  void PathBoxActivate(POIData data)   //길찾기 UI를 활성화
    {
        gps.Request();

        if (gps.GetMyLocation(ref latitude, ref longitude))
        {


            start.text = $"현재 위치";
            goal.text = data.Name();
            _animator.Play("Play");
        }

        else
        {
            start.text = $"현재 위치";
            goal.text = data.Name();
            _animator.Play("Play");

        }
        
    }


    public  void PathBoxDeactivate() //길찾기 UI 비활성화
    {
        _animator.Play("Reverse");
    }

}
