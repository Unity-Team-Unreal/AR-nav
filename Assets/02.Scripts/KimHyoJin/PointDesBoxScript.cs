using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class PointDesBoxScript : MonoBehaviour
{
    /// <summary>
    /// ��Ŀ�� ������ �� ������ ����UI�� ���� ���� ��ũ��Ʈ
    /// </summary>
    [SerializeField]Text PointName;
    [SerializeField]Text PointDescript;
    Animator _animator;
    [HideInInspector]public bool isActivate;    //����UI Ȱ������. �ڷΰ��� ��ư���� ������ ���̱� ������ public

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void DescriptionBoxActivate()    //����UI Ȱ��ȭ
    {
        isActivate = true;

        PointName.text = POI.datalist[3].Name();
        PointDescript.text = POI.datalist[3].Description();
        //POI.datalist�� ���� �޾ƿ��� ���� �ӽ��̸� BubbleState��ũ��Ʈ���� �޾ƿ����� ������ ��.

       _animator.Play("Play"); 

    } 

    public void DescriptionBoxDeactivate()  //����UI ��Ȱ��ȭ
    {
        isActivate = false;

        _animator.Play("Reverse");
    }
}
