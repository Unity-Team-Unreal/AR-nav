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

       _animator.Play("Play");

    }

    public void DescriptionBoxDeactivate()  //설명UI 비활성화
    {
        _animator.Play("Reverse");
    }
}
