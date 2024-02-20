using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class PointDesBoxScript : MonoBehaviour
{
    [SerializeField]Text PointName;
    [SerializeField]Text PointDescript;
    Animator _animator;
    [HideInInspector]public bool isActivate;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void DescriptionBoxActivate()
    {
        PointName.text = POI.datalist[4].Name();
        PointDescript.text = POI.datalist[4].Description();
        if (!isActivate) { isActivate = true; _animator.Play("Play"); }

    }   //POI 설명상자 ON(POI버블 버튼에서 작동)

    public void DescriptionBoxDeactivate()
    {
        isActivate = false;
        _animator.Play("Reverse");
    }   //POI 설명상자 OFF(뒤로가기 버튼에서 작동)
}
