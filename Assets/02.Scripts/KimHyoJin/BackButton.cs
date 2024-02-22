using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    /// <summary>
    /// 2D����, ��ã�� ȭ�鿡�� ����� �ڷΰ��� ��ư�� ���� ��ũ��Ʈ
    /// </summary>

    [SerializeField]PointDesBoxScript pointdesBox;   //POI ������� ��ũ��Ʈ�� �޾ƿ´�.
    [SerializeField]PathBoxScript pathbox;  //��ã�� ���� ��ũ��Ʈ�� �޾ƿ´�.

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

    void BackToHome()   //��ã�� ȭ�鿡�� ����ȭ������ ���� �޼���
    {
        Debug.Log("Ȩ ȭ������ �̵��ϴ� �� ��ȯ �ڵ�");
    }   

    void BackToNav()    //�󼼼��� ���ڿ��� ��Ŀ ������ ������ ���ư��� �޼���
    {
        pointdesBox.DescriptionBoxDeactivate();
    }

    void CancelSearching()  //�󼼼��� -> �����&������ UI ���� ��Ŀ ������ ������ ���ư��� �޼���
    {
        pathbox.PathBoxDeactivate();
    }
}
