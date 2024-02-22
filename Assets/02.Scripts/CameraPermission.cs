using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class CameraPermission : MonoBehaviour
{
    [SerializeField] private string previousScene;

    private void Start()
    {
        // ī�޶� ���� ��û
        StartCoroutine(CheckCameraPermission());
    }

    IEnumerator CheckCameraPermission()
    {
        // ī�޶� ������ ������ �ʾҴٸ� ���� ��û
        if (Permission.HasUserAuthorizedPermission("android.permission.CAMERA"))
        {
            yield break;
        }
        else
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // �������ٿ� ������ ������ ���
        yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.Camera)
        || !Permission.HasUserAuthorizedPermission(Permission.Camera));

        // ������ �������ʾ��� ���� ó��
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            SceneManager.LoadScene(previousScene);
            yield break;
        }
    }
}
