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
    PathBoxScript pathbox;

    DesImageScript DesBoxImage;

    Button button;

    Animator _animator; //애니메이션으로 UI의 움직임을 구현할 예정


    public POIData data;

    [HideInInspector] public bool pointDesIsActivate;   //길찾기중인지 확인, 뒤로가기 버튼에서 참조할 것이기 때문에 public

     void Awake()
    {
        pointDesIsActivate = false;

        pathbox = FindObjectOfType<PathBoxScript>(). GetComponent<PathBoxScript>();

        DesBoxImage = FindObjectOfType<DesImageScript>().GetComponent<DesImageScript>();

        button = GetComponentInChildren<Button>();
        _animator = GetComponent<Animator>();
        button.onClick.AddListener(pathbox.PathBoxActivate);
    }
    public  void DescriptionBoxActivate()    //설명UI 활성화
    {
        pointDesIsActivate = true;
       DesBoxImage. desBoxEneable();
        ponintAdress.text = data.Address();
        pointName.text = data.Name();
        pointDescript.text = data.Description();

       _animator.Play("Play"); 

    } 

    public void DescriptionBoxDeactivate()  //설명UI 비활성화
    {
       DesBoxImage. desBoxDisable();
        pointDesIsActivate = false;
        _animator.Play("Reverse");
    }
}
