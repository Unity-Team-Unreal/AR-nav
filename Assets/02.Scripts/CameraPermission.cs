using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class CameraPermission : MonoBehaviour
{
    [SerializeField] private string previousScene;

    void Start()
    {
        // 카메라 권한 요청
        StartCoroutine(CheckCameraPermission());
    }

    IEnumerator CheckCameraPermission()
    {
        // 카메라 권한이 허용되지 않았다면 권한 요청
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // 권한이 허용될 때까지 대기
        yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.Camera));

        // 권한이 허용되지않았을 때의 처리
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            SceneManager.LoadScene(previousScene);
            yield break;
        }
    }
}
