using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]PointDesAnimeScript pointdes;   //POI ������� ��ũ��Ʈ�� �޾ƿ´�.
    [SerializeField]PassBoxScript passbox;  //��ã�� ���� ��ũ��Ʈ�� �޾ƿ´�.

    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        pointdes = pointdes.GetComponent<PointDesAnimeScript>();

    }

    public void BackbuttonEvnet()
    {
        if (passbox.isSearching)
        {
            CancelSearching();
            BackToNav();
        }
        else if(pointdes.isActivate)  BackToNav();
        else BackToHome();
    }  //������ڰ� �ö������ �� ������ ������� �ݰ� �ƴ϶�� Ȩȭ������ �ű��

    void BackToHome()
    {
        Debug.Log("Ȩ ȭ������ �̵��ϴ� �� ��ȯ �ڵ�");
    }

    void BackToNav()
    {
        pointdes.DescriptionBoxDeactivate();
    }

    void CancelSearching()
    {
        passbox.PassBoxDeactivate();
    }
}
