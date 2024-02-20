using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class CategoryToggle : MonoBehaviour
{
    ToggleGroup group;
    Toggle activedToggle;
    private void Start()
    {
        group = gameObject.GetComponent<ToggleGroup>();
        activedToggle = group.GetFirstActiveToggle();   //현재 켜져있는 토글을 활성화된 토글로 지정
    }
    public void TapCategory()  //누른 토글에 따라 상태값 정하는 메서드
    {
        if (activedToggle == group.GetFirstActiveToggle()) return;   //토글의 OnValueChanged 때문에 메서드가 두번 호출되는 것을 막기 위함

        activedToggle = group.GetFirstActiveToggle();

        switch (group.GetFirstActiveToggle().name) 
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
            } //선택한 카테고리에 따라 카테고리 CategoryState 변경
    }


}
