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
    [SerializeField]Text ponintAdress;
    [SerializeField]Text pointName;
    [SerializeField]Text pointDescript;
    PathBoxScript pathbox;

    DesImageScript DesBoxImage;

    Button button;

    Animator _animator; //�ִϸ��̼����� UI�� �������� ������ ����


    public POIData data;

    [HideInInspector] public bool pointDesIsActivate;   //��ã�������� Ȯ��, �ڷΰ��� ��ư���� ������ ���̱� ������ public

     void Awake()
    {
        pointDesIsActivate = false;

        pathbox = FindObjectOfType<PathBoxScript>(). GetComponent<PathBoxScript>();

        DesBoxImage = FindObjectOfType<DesImageScript>().GetComponent<DesImageScript>();

        button = GetComponentInChildren<Button>();
        _animator = GetComponent<Animator>();
        button.onClick.AddListener(pathbox.PathBoxActivate);
    }
    public  void DescriptionBoxActivate()    //����UI Ȱ��ȭ
    {
        pointDesIsActivate = true;
       DesBoxImage. desBoxEneable();
        ponintAdress.text = data.Address();
        pointName.text = data.Name();
        pointDescript.text = data.Description();

       _animator.Play("Play"); 

    } 

    public void DescriptionBoxDeactivate()  //����UI ��Ȱ��ȭ
    {
       DesBoxImage. desBoxDisable();
        pointDesIsActivate = false;
        _animator.Play("Reverse");
    }
}
