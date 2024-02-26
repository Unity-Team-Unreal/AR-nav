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


    public POIData data;

    Animator _animator; //�ִϸ��̼����� UI�� �������� ������ ����

    DesImageScript DesBoxImage;

    [HideInInspector] public bool pathBoxIsActivate;    //����UI Ȱ������. �ڷΰ��� ��ư���� ������ ���̱� ������ public

    float latitude=0f;
    float longitude=0f;


     GPS gps;
    void Awake()
    {
        pathBoxIsActivate = false;
        gps = FindObjectOfType<GPS>().GetComponent<GPS>();
        _animator = GetComponent<Animator>();
        DesBoxImage = FindObjectOfType<DesImageScript>().GetComponent<DesImageScript>();

    }

    public  void PathBoxActivate()   //��ã�� UI�� Ȱ��ȭ
    {
        gps.Request();

        if (!gps.GetMyLocation(ref latitude, ref longitude))
        {
            DesBoxImage.desBoxDisable();
            pathBoxIsActivate = true;
            start.text = $"{latitude},{longitude}";
            goal.text = data.Name();
            _animator.Play("Play");
        }
        
    }

    public  void PathBoxDeactivate() //��ã�� UI ��Ȱ��ȭ
    {
        DesBoxImage. desBoxEneable();
        pathBoxIsActivate = false;
        _animator.Play("Reverse");
    }

}
