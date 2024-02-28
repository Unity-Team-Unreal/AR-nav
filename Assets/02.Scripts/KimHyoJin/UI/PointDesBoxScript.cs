using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class PointDesBoxScript : MonoBehaviour
{
    /// <summary>
    /// 마커를 눌렀을 때 나오는 설명UI를 띄우기 위한 스크립트
    /// </summary>

    [Header("상세설명 박스에 띄울 정보")]
    [SerializeField]Text ponintAdress;
    [SerializeField]Text pointName;
    [SerializeField]Text pointDescript;

    Animator _animator; //애니메이션으로 UI의 움직임을 구현할 예정




     void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public  void DescriptionBoxActivate(POIData data)    //설명UI 활성화
    {
        ponintAdress.text = data.Address();
        pointName.text = data.Name();
        pointDescript.text = data.Description();
        //각 정보들을 전달받은 POI 데이터에 맞게 기록

       _animator.Play("Play");  //UI 띄우기

    }

    public void DescriptionBoxDeactivate()  //설명UI 비활성화
    {
        _animator.Play("Reverse");  //UI 닫기
    }
}
