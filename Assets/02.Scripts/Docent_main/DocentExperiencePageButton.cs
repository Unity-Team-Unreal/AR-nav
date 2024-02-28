using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 도슨트 체험 페이지의 버튼 및 AR 영상 슬라이드 구현
/// </summary>
public class DocentExperiencePageButton : MonoBehaviour
{
    static string previousSceneName;

    public static Animator arDocent;

    [SerializeField] Button backButton;
    [SerializeField] Button returnButton;

    [SerializeField] Slider docentTimeLine;

    void Start()
    {
        previousSceneName = PlayerPrefs.GetString("PreviousScene");

        backButton.onClick.AddListener(OnBackPage);
        returnButton.onClick.AddListener(OnTimeLineReturn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Esc 버튼을 눌렀을 때 폰으로는 뒤로가기 버튼을 눌렀을 때
        {
            OnBackPage();
        }

        if (DocentExperiencePageManager.obj != null)
        {
            // 현재 애니메이션 상태 정보 가져오기
            AnimatorStateInfo stateInfo = arDocent.GetCurrentAnimatorStateInfo(0);

            // 애니메이션의 정규화된 시간을 사용하여 슬라이더 값 설정
            float normalizedTime = stateInfo.normalizedTime;

            docentTimeLine.value = normalizedTime;
        }
    }
    void OnBackPage()
    {
        // 바로 전에 있던 씬으로 이동 => 바로 전에 있던 씬의 이름을 가져와야 한다.
        SceneManager.LoadScene(previousSceneName);
        PlayerPrefs.DeleteKey("PreviousScene");
    }

    void OnTimeLineReturn()
    {
        // 애니메이션과 슬라이드 값 초기화
        arDocent.Play("metarig|Walk", -1, 0f);
        docentTimeLine.value = 0;
    }
}
