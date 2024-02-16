using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleState : MonoBehaviour
{
    [SerializeField] CategoryState thisState;   //POI버블이 어떤 카테고리에 속하는지

    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(GameObject.Find("PointDescriptionPage")
            .GetComponent<PointDesAnimeScript>().DescriptionBoxActivate);   //POI 버블 누르면 POI 설명창이 올라온다.
    }

    void Update()
    {
        bubbleOnOff();  //POI 버블의 표시 여부를 가리는 코드
    }

    public void bubbleOnOff()
    {   
        //이 POI의 카테고리와 활성화된 카테고리가 동일 또는 모두 보기 카테고리라면 버튼 활성화
        if (SellectCategory.state != CategoryState.All)
        {
            if (thisState != SellectCategory.state) button.interactable = false;

            else button.interactable = true;
        }

        //아니라면 버튼 비활성화
        else button.interactable = true;
    }

}
