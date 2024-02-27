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
    UIController uIController;
    Button button;
    [SerializeField] string MainSceneName;

    private void Awake()
    {
        uIController = GameObject.Find("NavManager").GetComponent<UIController>();

        button = GetComponent<Button>();
        
        button.onClick.AddListener(BackbuttonEvnet);
    }

    public void BackbuttonEvnet()
    {
        if(uIController.pathBoxIsActivate) uIController.PathBoxOff();

        else if (uIController.pointDesIsActivate) uIController.DescriptBoxOff();

        else BackToHome();

    }  //������ڰ� �ö������ �� ������ ������� �ݰ� �ƴ϶�� Ȩȭ������ �ű��

    void BackToHome()   //��ã�� ȭ�鿡�� ����ȭ������ ���� �޼���
    {
        SceneManager.LoadScene(MainSceneName);
    }   

}
