using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class LocationAccessManager : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button locationAccessAllowedButton;
    [SerializeField] Button locationAccessNotAllowedButton;

    [SerializeField] GameObject locationAccessPanel;

    void Start()
    {
        backButton.onClick.AddListener(OnBackPage);
        locationAccessAllowedButton.onClick.AddListener(OnLocationAccess);
        locationAccessNotAllowedButton.onClick.AddListener(DontLocationAccess);
    }

    void OnBackPage()
    {
        SceneManager.LoadScene("����Ʈ �󼼼��� ������");
    }
    void OnLocationAccess()
    {
        SceneManager.LoadScene("�ӽ� �� �ȳ� ����"); // ""���� �� �ȳ� ���� ���� �̸��� ������ �ȴ�. �Ǵ� Panel�� ��Ȱ��ȭ�Ѵ�.
    }
    void DontLocationAccess()
    {
        SceneManager.LoadScene("����Ʈ �󼼼��� ������");
    }
}
