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

    // ���� ��û�� �����ϴ� �Լ�
    public void RequestCameraPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // ����ڰ� ���� ������ ������� �ʾҴٸ� ���̾�α׸� Ȱ��ȭ�Ͽ� ������
            permissionDialog.SetActive(true);
        }
    }

    // ����ڰ� ������ �������� �� ȣ��Ǵ� �Լ�
    public void OnPermissionGranted()
    {
        // ���̾�α׸� ��Ȱ��ȭ�ϰ� ī�޶� ������ ��û
        permissionDialog.SetActive(false);
        Permission.RequestUserPermission(Permission.Camera);
    }

    // ����ڰ� ������ �ź����� �� ȣ��Ǵ� �Լ�
    public void OnPermissionDenied()
    {
        // ���̾�α׸� ��Ȱ��ȭ�ϰ� ���� �ź� ó��
        permissionDialog.SetActive(false);
        // ���� �źο� ���� �߰� ó���� ���⿡ �߰��� �� ����
    }
}
