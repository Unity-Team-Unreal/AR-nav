using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class CategoryToggle : MonoBehaviour
{
    /// <summary>
    /// 화면 하단의 카테고리 바를 눌렀을 때 카테고리 상태(SellectCategory.state)를 변경하는 스크립트
    /// </summary>
    ToggleGroup group;
    Toggle activedToggle;   //현재 켜져있는 토글
    private void Start()
    {
        group = gameObject.GetComponent<ToggleGroup>();
        activedToggle = group.GetFirstActiveToggle();   //현재 켜져있는 토글을 활성화된 토글로 지정
    }
    public void TapCategory()  //누른 토글에 따라 상태값 정하는 메서드
    {
        if (activedToggle == group.GetFirstActiveToggle()) return;   //토글의 OnValueChanged 때문에 메서드가 두번 호출되는 것을 막기 위함

        activedToggle = group.GetFirstActiveToggle();   //현재 켜져있는 토글을 토글 그룹에서 활성화된 토글로 지정

        switch (group.GetFirstActiveToggle().name) //선택한 토글에 따라 CategoryState 변경
        {
                case "Bottom_All":
                    SellectCategory.state = CategoryState.All;
                    break;
                case "Bottom_Public":
                    SellectCategory.state = CategoryState.PublicBuilding;
                    break;
                case "Bottom_Hospital":
                    SellectCategory.state = CategoryState.Hospital;
                    break;
                case "Bottom_Photozone":
                    SellectCategory.state = CategoryState.Photozone;
                    break;
            } 
    }


}
