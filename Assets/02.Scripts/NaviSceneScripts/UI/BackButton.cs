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

    UIController uIController;  //ui�� �����̴� UI��Ʈ�ѷ�
    Button button;  //�ڷΰ��� ��ư

    [SerializeField] string MainSceneName;  //���� ������ �Ѿ�� ���� �� �̸�

    private void Awake()
    {
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        button = GetComponent<Button>();
        
        button.onClick.AddListener(BackbuttonEvnet);    //�ڷΰ��� ��ư�� �̺�Ʈ �߰�
    }

    public void BackbuttonEvnet()
    {
        if (uIController.pathBoxIsActivate)     //��� ã�� �������� �� �ڷΰ��� ��ư�� ������
        {
            uIController.PathBoxOff();      //���ã�� �ڽ� ��Ȱ��ȭ
            uIController.CategoryBoxOn();   //�ϴ� ī�װ� �ڽ� Ȱ��ȭ
        }

        else if (uIController.pointDesIsActivate) uIController.DescriptBoxOff();
        //�󼼼��� �������� �� �ڷΰ��� ��ư�� ������ �󼼼��� �ڽ� ��Ȱ��ȭ

        else BackToHome();
        //�⺻ ���¿��� �ڷΰ��� ��ư�� ������ ���� ������ �Ѿ��


    }

    void BackToHome()   //��ã�� ȭ�鿡�� ����ȭ������ ���� �޼���
    {
        SceneManager.LoadScene(MainSceneName);
    }   

}
