using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    /// <summary>
    /// 2D����, ��ã�� ȭ�鿡�� ����� �ڷΰ��� ��ư�� ���� ��ũ��Ʈ
    /// </summary>

    [SerializeField]PointDesBoxScript pointdesBox;   //POI ������� ��ũ��Ʈ�� �޾ƿ´�.
    [SerializeField] PathBoxScript pathBox;   //POI ��ã�� ���� ��ũ��Ʈ�� �޾ƿ´�.
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
      button.onClick.AddListener(BackbuttonEvnet);
    }

    public void BackbuttonEvnet()
    {
        if (pathBox.pathBoxIsActivate)
        {
            CancelSearching();
        }
        if(pointdesBox.pointDesIsActivate)  BackToNav();
        else BackToHome();
    }  //������ڰ� �ö������ �� ������ ������� �ݰ� �ƴ϶�� Ȩȭ������ �ű��

    void BackToHome()   //��ã�� ȭ�鿡�� ����ȭ������ ���� �޼���
    {
        SceneManager.LoadScene("1.Home Page");
    }   

    void BackToNav()    //�󼼼��� ���ڿ��� ��Ŀ ������ ������ ���ư��� �޼���
    {
        pointdesBox.DescriptionBoxDeactivate();
    }

    void CancelSearching()  //�����&������ UI ���� �󼼼������� ���ư��� �޼���
    {
        pathBox.PathBoxDeactivate();
    }
}
