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
        SceneManager.LoadScene("도슨트 상세설명 페이지");
    }
    void OnLocationAccess()
    {
        SceneManager.LoadScene("임시 길 안내 서비스"); // ""내에 길 안내 서비스 씬의 이름을 적으면 된다. 또는 Panel을 비활성화한다.
    }
    void DontLocationAccess()
    {
        SceneManager.LoadScene("도슨트 상세설명 페이지");
    }
}
