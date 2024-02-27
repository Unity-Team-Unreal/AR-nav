using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathFinderButtonScript : MonoBehaviour
{
    Animator animator;
    Button button;

    [SerializeField] string ARnaviSceneName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(PathFinderButtonEvent);
    }

    public void PathBoxActivate()
    {
        animator.Play("start");
    }

    public void PathBoxDeActivate()
    {
        animator.Play("Reverse");
    }

    void PathFinderButtonEvent()
    {
        CameraRequest();
    }

    void CameraRequest(string permission = null)   //����ڿ��� ī�޶� ������ ��û�ϰ�, ������ AR�׺������ �Ѿ�� ��ũ��Ʈ
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
