using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    [SerializeField] CategoryState thisState;   //POI������ � ī�װ��� ���ϴ���

    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(GameObject.Find("PointDescriptionPage")
            .GetComponent<PointDesAnimeScript>().DescriptionBoxActivate);   //POI ���� ������ POI ����â�� �ö�´�.
    }

    void Update()
    {
        bubbleOnOff();  //POI ������ ǥ�� ���θ� ������ �ڵ�
    }

    public void bubbleOnOff()
    {   
        //�� POI�� ī�װ��� Ȱ��ȭ�� ī�װ��� ���� �Ǵ� ��� ���� ī�װ���� ��ư Ȱ��ȭ
        if (SellectCategory.state != CategoryState.All)
        {
            if (thisState != SellectCategory.state) button.interactable = false;

            else button.interactable = true;
        }

        //�ƴ϶�� ��ư ��Ȱ��ȭ
        else button.interactable = true;
    }

}
