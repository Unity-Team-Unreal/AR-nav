using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 두 개의 토글 버튼과 그에 해당하는 UI요소 관리하는 스크립트
/// </summary>
public class SwitchingButton : MonoBehaviour
{
    [Header("토글그룹")]
    [SerializeField] private Toggle togglePhoto;
    [SerializeField] private Toggle toggleDocent;

    [Header("토글이미지")]
    [SerializeField] private GameObject imagePhoto;
    [SerializeField] private GameObject imageDocent;

    [Header("스크롤뷰")]
    [SerializeField] private GameObject scrollViewPhoto;
    [SerializeField] private GameObject scrollViewDocent;

    //현재 활성화된 토글 저장 변수
    private Toggle activeToggle;

    public Toggle ActiveToggle { get { return activeToggle; } private set { activeToggle = value; } }



    void Awake()
    {
        // 토글 그룹 설정
        togglePhoto.group = toggleDocent.group;

        // 토글 상태 변경 이벤트 등록
        togglePhoto.onValueChanged.AddListener(OnToggleValueChanged);
        toggleDocent.onValueChanged.AddListener(OnToggleValueChanged);

        // 초기설정
        togglePhoto.isOn = true;
        toggleDocent.isOn = false;

        // 초기 요소 활성화
        ToggleElements(togglePhoto, imagePhoto, imageDocent, scrollViewPhoto, scrollViewDocent);

        // 현재 활성화된 토글 설정
        activeToggle = togglePhoto;

    }

    // 토글 상태변화
    void OnToggleValueChanged(bool isOn)
    {
        // 토글이 켜진 경우
        if (isOn)
        {
            //현재 켜진 토글 확인
            if (togglePhoto.isOn)
            {
                // 토글A 활성화 시 처리
                ToggleElements(togglePhoto, imagePhoto, imageDocent, scrollViewPhoto, scrollViewDocent);
                activeToggle = togglePhoto;
            }
            else if (toggleDocent.isOn)
            {
                // 토글B 활성화 시 처리
                ToggleElements(toggleDocent, imageDocent, imagePhoto, scrollViewDocent, scrollViewPhoto);
                activeToggle = toggleDocent;
            }
        }
    }

    /// <summary>
    /// 이미지와 스크롤뷰를 활성화 또는 비활성화하는 함수
    /// </summary>
    /// <param name="toggle">활성화할 토글</param>
    /// <param name="activeElement">활성화 이미지</param>
    /// <param name="inactiveElement">비활성화 이미지 </param>
    /// <param name="activeScrollView">스크롤 뷰 활성화</param>
    /// <param name="inactiveScrollView">스크롤뷰 비활성화</param>
    void ToggleElements(Toggle toggle, GameObject activeElement, GameObject inactiveElement, GameObject activeScrollView, GameObject inactiveScrollView)
    {
        // 활성화 요소
        if (activeElement != null)
            activeElement.SetActive(toggle.isOn);
        // 비활성화 요소
        if (inactiveElement != null)
            inactiveElement.SetActive(!toggle.isOn);
        // 스크롤 뷰 활성화
        if (activeScrollView != null)
            activeScrollView.SetActive(toggle.isOn);
        // 스크롤뷰 비활성화
        if (inactiveScrollView != null)
            inactiveScrollView.SetActive(!toggle.isOn);
    }
}