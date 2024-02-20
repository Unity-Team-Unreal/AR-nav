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
        activedToggle = group.GetFirstActiveToggle();   //���� �����ִ� ����� Ȱ��ȭ�� ��۷� ����
    }
    public void TapCategory()  //���� ��ۿ� ���� ���°� ���ϴ� �޼���
    {
        if (activedToggle == group.GetFirstActiveToggle()) return;   //����� OnValueChanged ������ �޼��尡 �ι� ȣ��Ǵ� ���� ���� ����

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
            } //������ ī�װ��� ���� ī�װ� CategoryState ����
    }


}
