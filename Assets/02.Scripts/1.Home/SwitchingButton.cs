using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{
    public ToggleGroup toggleGroup; // Toggle Group�� Inspector���� �Ҵ����ּ���.
    public Toggle aToggle;
    public Toggle bToggle;
    public GameObject aImage;
    public GameObject bImage;

    void Start()
    {
        // �� ����� Toggle Group�� �����մϴ�.
        aToggle.group = toggleGroup;
        bToggle.group = toggleGroup;

        // �� ����� ���� ���� �̺�Ʈ�� ���� �����ʸ� ����մϴ�.
        aToggle.onValueChanged.AddListener(OnAToggleValueChanged);
        bToggle.onValueChanged.AddListener(OnBToggleValueChanged);

        // A ����� �ʱ⿡ Ȱ��ȭ�մϴ�.
        aToggle.isOn = true;
        ToggleImages(aToggle, aImage, bImage);
    }

    void OnAToggleValueChanged(bool isOn)
    {
        // A ����� ������ ���¶�� A �̹����� ���̰� �մϴ�.
        if (isOn)
        {
            ToggleImages(aToggle, aImage, bImage);
        }
    }

    void OnBToggleValueChanged(bool isOn)
    {
        // B ����� ������ ���¶�� B �̹����� ���̰� �մϴ�.
        if (isOn)
        {
            ToggleImages(bToggle, bImage, aImage);
        }
    }

    // �̹����� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
    void ToggleImages(Toggle toggle, GameObject activeImage, GameObject inactiveImage)
    {
        // Toggle�� ���� ���¿� ��ġ�ϸ� �̹����� �����մϴ�.
        if (toggle.isOn)
        {
            activeImage.SetActive(true);
            inactiveImage.SetActive(false);
        }
    }
}
