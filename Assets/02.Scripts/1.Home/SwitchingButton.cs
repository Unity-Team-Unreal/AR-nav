using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{
    [SerializeField] private Toggle toggleA;
    [SerializeField] private Toggle toggleB;
    [SerializeField] private GameObject imageA;
    [SerializeField] private GameObject imageB;
    [SerializeField] private GameObject scrollViewA;
    [SerializeField] private GameObject scrollViewB;

    private Toggle activeToggle;

    void Awake()
    {
        toggleA.group = toggleB.group; // 토글 그룹 설정

        // 상태 변경 이벤트에 대한 리스너 등록
        toggleA.onValueChanged.AddListener(OnToggleValueChanged);
        toggleB.onValueChanged.AddListener(OnToggleValueChanged);

        // toggleA를 초기에 활성화
        toggleA.isOn = true;

        // toggleB를 초기에 비활성화
        toggleB.isOn = false;

        ToggleElements(toggleA, imageA, imageB, scrollViewA, scrollViewB);

        activeToggle = toggleA;
    }

    void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            if (activeToggle == toggleA)
            {
                ToggleElements(toggleA, imageA, imageB, scrollViewA, scrollViewB);
                activeToggle = toggleA;
            }
            else if (activeToggle == toggleB)
            {
                ToggleElements(toggleB, imageB, imageA, scrollViewB, scrollViewA);
                activeToggle = toggleB;
            }
        }
    }

    // 이미지와 스크롤뷰를 활성화 또는 비활성화하는 함수
    void ToggleElements(Toggle toggle, GameObject activeElement, GameObject inactiveElement, GameObject activeScrollView, GameObject inactiveScrollView)
    {
        if (activeElement != null)
            activeElement.SetActive(toggle.isOn);

        if (inactiveElement != null)
            inactiveElement.SetActive(!toggle.isOn);

        if (activeScrollView != null)
            activeScrollView.SetActive(toggle.isOn);

        if (inactiveScrollView != null)
            inactiveScrollView.SetActive(!toggle.isOn);
    }
}
