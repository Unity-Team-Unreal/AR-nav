using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CameraPermission : MonoBehaviour
{
    [SerializeField] GameObject permissionDialog;

    // Start is called before the first frame update
    void Start()
    {
        RequestCameraPermission();
    }

    // 권한 요청을 시작하는 함수
    public void RequestCameraPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // 사용자가 아직 권한을 허용하지 않았다면 다이얼로그를 활성화하여 보여줌
            permissionDialog.SetActive(true);
        }
    }

    // 사용자가 권한을 수락했을 때 호출되는 함수
    public void OnPermissionGranted()
    {
        // 다이얼로그를 비활성화하고 카메라 권한을 요청
        permissionDialog.SetActive(false);
        Permission.RequestUserPermission(Permission.Camera);
    }

    // 사용자가 권한을 거부했을 때 호출되는 함수
    public void OnPermissionDenied()
    {
        // 다이얼로그를 비활성화하고 권한 거부 처리
        permissionDialog.SetActive(false);
        // 권한 거부에 대한 추가 처리를 여기에 추가할 수 있음
    }
}
