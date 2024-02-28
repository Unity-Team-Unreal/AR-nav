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

    [Header("�󼼼��� �ڽ��� ��� ����")]
    [SerializeField]Text ponintAdress;
    [SerializeField]Text pointName;
    [SerializeField]Text pointDescript;

    Animator _animator; //�ִϸ��̼����� UI�� �������� ������ ����




     void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public  void DescriptionBoxActivate(POIData data)    //����UI Ȱ��ȭ
    {
        ponintAdress.text = data.Address();
        pointName.text = data.Name();
        pointDescript.text = data.Description();
        //�� �������� ���޹��� POI �����Ϳ� �°� ���

       _animator.Play("Play");  //UI ����

    }

    public void DescriptionBoxDeactivate()  //����UI ��Ȱ��ȭ
    {
        _animator.Play("Reverse");  //UI �ݱ�
    }
}
