using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonHyo : MonoBehaviour
{
    [SerializeField]PointDesBoxScript pointdesBox;   //POI ������� ��ũ��Ʈ�� �޾ƿ´�.
    [SerializeField]PathBoxScript pathbox;  //��ã�� ���� ��ũ��Ʈ�� �޾ƿ´�.
    //test
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void BackbuttonEvnet()
    {
        if (pathbox.isActivate)
        {
            CancelSearching();
        }
        if(pointdesBox.isActivate)  BackToNav();
        else BackToHome();
    }  //������ڰ� �ö������ �� ������ ������� �ݰ� �ƴ϶�� Ȩȭ������ �ű��

    void BackToHome()
    {
        Debug.Log("Ȩ ȭ������ �̵��ϴ� �� ��ȯ �ڵ�");
    }

    void BackToNav()
    {
        pointdesBox.DescriptionBoxDeactivate();
    }

    void CancelSearching()
    {
        pathbox.PathBoxDeactivate();
    }
}
