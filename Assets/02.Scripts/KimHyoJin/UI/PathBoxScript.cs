using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PathBoxScript : MonoBehaviour
{
    /// <summary>
    /// ��Ŀ�� ������ �� ������ ���� ������ �ϴ��� ��ã�� ��ư�� ������ ��, ��ܿ� �߰��Ǵ� �����/������ UI�� �����ϴ� ��ũ��Ʈ
    /// </summary>
    /// 

    [Header("��ã�� �ڽ� �Ӽ�")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;

    Animator _animator; //�ִϸ��̼����� UI�� �������� ������ ����


    double latitude = 0f;
    double longitude = 0f;

     GPS gps;

    void Awake()
    {
        gps = FindObjectOfType<GPS>().GetComponent<GPS>();
        _animator = GetComponent<Animator>();

    }

    public  void PathBoxActivate(POIData data)   //��ã�� UI�� Ȱ��ȭ
    {
        gps.Request();

        if (gps.GetMyLocation(ref latitude, ref longitude))
        {


            start.text = $"���� ��ġ";
            goal.text = data.Name();
            _animator.Play("Play");
        }

        else
        {
            start.text = $"���� ��ġ";
            goal.text = data.Name();
            _animator.Play("Play");

        }
        
    }


    public  void PathBoxDeactivate() //��ã�� UI ��Ȱ��ȭ
    {
        _animator.Play("Reverse");
    }

}
