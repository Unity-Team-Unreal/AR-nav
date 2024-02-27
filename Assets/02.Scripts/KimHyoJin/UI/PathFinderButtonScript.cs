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

    void CameraRequest(string permission = null)   //사용자에게 카메라 권한을 요청하고, 수락시 AR네비씬으로 넘어가는 스크립트
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera)) 
        {
            SceneManager.LoadScene(ARnaviSceneName);  //권한이 수락되어있다면 씬 이동
        }

        else
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();  //권한 요청 응답에 따른 콜백
            callbacks.PermissionGranted += CameraRequest;    //권한 요청 수락시 request 재귀
            callbacks.PermissionDeniedAndDontAskAgain += CameraRequest; //권한 미수락시...(구현중)
            Permission.RequestUserPermission(Permission.Camera, callbacks);   //콜백을 추가하여 카메라 권한 요청
        }
    }

}
