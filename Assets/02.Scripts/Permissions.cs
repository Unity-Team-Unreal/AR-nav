using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class Permissions : MonoBehaviour
{
    public enum PermissionType { Camera, Microphone, Location };

    [SerializeField]
    private PermissionType permissionType;

    [SerializeField] string SceneName;

    /// <summary>
    /// 사용자에게 권한 요청
    /// </summary>
    public void RequestPermission()
    {
        string permissionName = GetPermissionName(permissionType);
        if (!Permission.HasUserAuthorizedPermission(permissionName))
        {
            Permission.RequestUserPermission(permissionName);
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    /// <summary>
    /// 권한 유형에 따른 권한 이름 가져오기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetPermissionName(PermissionType type)
    {
        switch (type)
        {
            case PermissionType.Camera:
                return Permission.Camera;
            case PermissionType.Microphone:
                return Permission.Microphone;
            case PermissionType.Location:
                return Permission.FineLocation;
            default:
                return "";
        }
    }
}
