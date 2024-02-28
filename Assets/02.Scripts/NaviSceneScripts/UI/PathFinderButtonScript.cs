using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathFinderButtonScript : MonoBehaviour
{
    /// <summary>
    /// ��Ŀ�� ������ �� ������ ���� ������ �ϴ��� ��ã�� ��ư�� ������ ��, �ϴ��� UI�� ���� ��ũ��Ʈ
    /// </summary>
    
    Animator animator;
    Button button;

    [SerializeField] string ARnaviSceneName;    //�Ѿ AR��ã�� ��

    private void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(PathFinderButtonEvent);  //AR��ã��� �Ѿ ��ư �̺�Ʈ
    }

    public void PathBoxActivate()   //UIȰ��ȭ
    {
        animator.Play("start");
    }

    public void PathBoxDeActivate()     //UI ��Ȱ��ȭ
    {
        animator.Play("Reverse");
    }

    void PathFinderButtonEvent()    //ī�޶� ���� ��û
    {
        CameraRequest();
    }

    void CameraRequest(string permission = null)   //����ڿ��� ī�޶� ������ ��û�ϰ�, ������ AR�׺������ �Ѿ�� �޼���
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera)) 
        {
            SceneManager.LoadScene(ARnaviSceneName);  //������ �����Ǿ��ִٸ� �� �̵�
        }

        else
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();  //���� ��û ���信 ���� �ݹ�
            callbacks.PermissionGranted += CameraRequest;    //���� ��û ������ request ���
            callbacks.PermissionDeniedAndDontAskAgain += CameraRequest; //���� �̼�����...(������)
            Permission.RequestUserPermission(Permission.Camera, callbacks);   //�ݹ��� �߰��Ͽ� ī�޶� ���� ��û
        }
    }

}
