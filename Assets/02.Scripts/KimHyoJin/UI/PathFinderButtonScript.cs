using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathFinderButtonScript : MonoBehaviour
{
    /// <summary>
    /// 마커를 눌렀을 때 나오는 설명 페이지 하단의 길찾기 버튼을 눌렀을 때, 하단의 UI를 띄우는 스크립트
    /// </summary>
    
    Animator animator;
    Button button;

    [SerializeField] string ARnaviSceneName;    //넘어갈 AR길찾기 씬

    private void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(PathFinderButtonEvent);  //AR길찾기로 넘어갈 버튼 이벤트
    }

    public void PathBoxActivate()   //UI활성화
    {
        animator.Play("start");
    }

    public void PathBoxDeActivate()     //UI 비활성화
    {
        animator.Play("Reverse");
    }

    void PathFinderButtonEvent()    //카메라 권한 요청
    {
        CameraRequest();
    }

    void CameraRequest(string permission = null)   //사용자에게 카메라 권한을 요청하고, 수락시 AR네비씬으로 넘어가는 메서드
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
