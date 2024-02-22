using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathBoxScript : MonoBehaviour
{
    /// <summary>
    /// 마커를 눌렀을 때 나오는 설명 페이지 하단의 길찾기 버튼을 눌렀을 때, 상단에 추가되는 출발지/목적지 UI를 구현하는 스크립트
    /// </summary>
    Animator _animator;     //애니메이션으로 구현할 것이기 때문
    [HideInInspector] public bool isActivate;   //길찾기중인지 확인, 뒤로가기 버튼에서 참조할 것이기 때문에 public
    [Header("길찾기 박스 속성")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PathBoxActivate()   //길찾기 UI를 활성화
    {
        //GPS 접근권한이 허용되어 있다면
        isActivate = true;
        _animator.Play("Play");
    }

    public void PathBoxDeactivate() //길찾기 UI 비활성화
    {
        isActivate = false;
        _animator.Play("Reverse");
    }
}
