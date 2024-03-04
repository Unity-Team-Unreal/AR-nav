using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;

/// <summary>
/// ����Ʈ �� ���� ������ ������ ��ư��� ����
/// </summary>
public class DetailedDescriptionPageManager : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button viewLocationButton;
    [SerializeField] Button docentStartButton;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", sceneName);

        backButton.onClick.AddListener(OnBackPage);
        viewLocationButton.onClick.AddListener(OnViewLocation);
        docentStartButton.onClick.AddListener(OnDocentStart);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackPage();
        }
    }

    void OnBackPage()
    {
        //SceneManager.LoadScene("Test Scene"); // ""���� ����Ʈ ����Ʈ ������ ���� �̸��� ������ �ȴ�.
    }

    /// <summary>
    /// ��ġ���� ��ư�� ������ �� �Ͼ�� �̺�Ʈ
    /// </summary>
    void OnViewLocation()
    {
        // �ڵ����� ��ġ���� ����� ���������� �˾�â ����
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            SceneManager.LoadScene("��ġ���� ��� �˾�â"); // �ʿ� ���� ���ɼ��� ����.

            Permission.RequestUserPermission(Permission.FineLocation); // ��ġ���� ��� ��û
        }

        // �ڵ����� ��ġ���� ����� ���������� �ٷ� �� �ȳ� ���񽺷� �̵�
        else
        {
            SceneManager.LoadScene("�ӽ� �� �ȳ� ����"); // ""���� �� �ȳ� ���� ���� �̸��� ������ �ȴ�.
        }
    }
    /// <summary>
    /// ����Ʈ ���� ��ư�� ������ �� �Ͼ�� �̺�Ʈ
    /// </summary>
    void OnDocentStart()
    {
        // �ڵ����� ī�޶����� ����� ���������� �˾�â ����
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // �ڵ����� ī�޶����� ����� ���������� �ٷ� ī�޶�� �̵�
        else
        {
            SceneManager.LoadScene("����Ʈ ü�� ������"); // ""���� ī�޶� �Կ� ���� �̸��� ������ �ȴ�.
        }
    }
}
