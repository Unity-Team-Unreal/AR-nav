using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;

/// <summary>
/// 도슨트 상세 설명 페이지 내에서 버튼기능 구현
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
        //SceneManager.LoadScene("Test Scene"); // ""내에 도슨트 리스트 페이지 씬의 이름을 적으면 된다.
    }

    /// <summary>
    /// 위치보기 버튼을 눌렀을 때 일어나는 이벤트
    /// </summary>
    void OnViewLocation()
    {
        // 핸드폰에 위치접근 허용이 꺼져있으면 팝업창 켜짐
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            SceneManager.LoadScene("위치접근 허용 팝업창"); // 필요 없을 가능성이 높다.

            Permission.RequestUserPermission(Permission.FineLocation); // 위치접근 허용 요청
        }

        // 핸드폰에 위치접근 허용이 켜져있으면 바로 길 안내 서비스로 이동
        else
        {
            SceneManager.LoadScene("임시 길 안내 서비스"); // ""내에 길 안내 서비스 씬의 이름을 적으면 된다.
        }
    }
    /// <summary>
    /// 도슨트 시작 버튼을 눌렀을 때 일어나는 이벤트
    /// </summary>
    void OnDocentStart()
    {
        // 핸드폰에 카메라접근 허용이 꺼져있으면 팝업창 켜짐
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // 핸드폰에 카메라접근 허용이 켜져있으면 바로 카메라로 이동
        else
        {
            SceneManager.LoadScene("도슨트 체험 페이지"); // ""내에 카메라 촬영 씬의 이름을 적으면 된다.
        }
    }
}
