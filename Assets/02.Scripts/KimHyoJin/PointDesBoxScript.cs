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
    [SerializeField]Text PointName;
    [SerializeField]Text PointDescript;
    Animator _animator;
    [HideInInspector]public bool isActivate;    //설명UI 활성여부. 뒤로가기 버튼에서 참조할 것이기 때문에 public

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void DescriptionBoxActivate()    //설명UI 활성화
    {
        isActivate = true;

        PointName.text = POI.datalist[3].Name();
        PointDescript.text = POI.datalist[3].Description();
        //POI.datalist를 직접 받아오는 것은 임시이며 BubbleState스크립트에서 받아오도록 수정할 것.

       _animator.Play("Play"); 

    } 

    public void DescriptionBoxDeactivate()  //설명UI 비활성화
    {
        isActivate = false;

        _animator.Play("Reverse");
    }
}
