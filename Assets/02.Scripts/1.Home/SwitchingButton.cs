using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{
    public ToggleGroup toggleGroup; // Toggle Group을 Inspector에서 할당해주세요.
    public Toggle aToggle;
    public Toggle bToggle;
    public GameObject aImage;
    public GameObject bImage;

    void Start()
    {
        // 각 토글의 Toggle Group을 설정합니다.
        aToggle.group = toggleGroup;
        bToggle.group = toggleGroup;

        // 각 토글의 상태 변경 이벤트에 대한 리스너를 등록합니다.
        aToggle.onValueChanged.AddListener(OnAToggleValueChanged);
        bToggle.onValueChanged.AddListener(OnBToggleValueChanged);

        // A 토글을 초기에 활성화합니다.
        aToggle.isOn = true;
        ToggleImages(aToggle, aImage, bImage);
    }

    void OnAToggleValueChanged(bool isOn)
    {
        // A 토글이 눌려진 상태라면 A 이미지를 보이게 합니다.
        if (isOn)
        {
            ToggleImages(aToggle, aImage, bImage);
        }
    }

    void OnBToggleValueChanged(bool isOn)
    {
        // B 토글이 눌려진 상태라면 B 이미지를 보이게 합니다.
        if (isOn)
        {
            ToggleImages(bToggle, bImage, aImage);
        }
    }

    // 이미지를 활성화 또는 비활성화하는 함수
    void ToggleImages(Toggle toggle, GameObject activeImage, GameObject inactiveImage)
    {
        // Toggle이 현재 상태와 일치하면 이미지를 조작합니다.
        if (toggle.isOn)
        {
            activeImage.SetActive(true);
            inactiveImage.SetActive(false);
        }
    }
}
