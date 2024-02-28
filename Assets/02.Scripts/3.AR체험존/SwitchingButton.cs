using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ���� ��� ��ư�� �׿� �ش��ϴ� UI��� �����ϴ� ��ũ��Ʈ
/// </summary>
public class SwitchingButton : MonoBehaviour
{
    [Header("��۱׷�")]
    [SerializeField] private Toggle togglePhoto;
    [SerializeField] private Toggle toggleDocent;

    [Header("����̹���")]
    [SerializeField] private GameObject imagePhoto;
    [SerializeField] private GameObject imageDocent;

    [Header("��ũ�Ѻ�")]
    [SerializeField] private GameObject scrollViewPhoto;
    [SerializeField] private GameObject scrollViewDocent;

    //���� Ȱ��ȭ�� ��� ���� ����
    private Toggle activeToggle;

    public Toggle ActiveToggle { get { return activeToggle; } private set { activeToggle = value; } }



    void Awake()
    {
        // ��� �׷� ����
        togglePhoto.group = toggleDocent.group;

        // ��� ���� ���� �̺�Ʈ ���
        togglePhoto.onValueChanged.AddListener(OnToggleValueChanged);
        toggleDocent.onValueChanged.AddListener(OnToggleValueChanged);

        // �ʱ⼳��
        togglePhoto.isOn = true;
        toggleDocent.isOn = false;

        // �ʱ� ��� Ȱ��ȭ
        ToggleElements(togglePhoto, imagePhoto, imageDocent, scrollViewPhoto, scrollViewDocent);

        // ���� Ȱ��ȭ�� ��� ����
        activeToggle = togglePhoto;

    }

    // ��� ���º�ȭ
    void OnToggleValueChanged(bool isOn)
    {
        // ����� ���� ���
        if (isOn)
        {
            //���� ���� ��� Ȯ��
            if (togglePhoto.isOn)
            {
                // ���A Ȱ��ȭ �� ó��
                ToggleElements(togglePhoto, imagePhoto, imageDocent, scrollViewPhoto, scrollViewDocent);
                activeToggle = togglePhoto;
            }
            else if (toggleDocent.isOn)
            {
                // ���B Ȱ��ȭ �� ó��
                ToggleElements(toggleDocent, imageDocent, imagePhoto, scrollViewDocent, scrollViewPhoto);
                activeToggle = toggleDocent;
            }
        }
    }

    /// <summary>
    /// �̹����� ��ũ�Ѻ並 Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="toggle">Ȱ��ȭ�� ���</param>
    /// <param name="activeElement">Ȱ��ȭ �̹���</param>
    /// <param name="inactiveElement">��Ȱ��ȭ �̹��� </param>
    /// <param name="activeScrollView">��ũ�� �� Ȱ��ȭ</param>
    /// <param name="inactiveScrollView">��ũ�Ѻ� ��Ȱ��ȭ</param>
    void ToggleElements(Toggle toggle, GameObject activeElement, GameObject inactiveElement, GameObject activeScrollView, GameObject inactiveScrollView)
    {
        // Ȱ��ȭ ���
        if (activeElement != null)
            activeElement.SetActive(toggle.isOn);
        // ��Ȱ��ȭ ���
        if (inactiveElement != null)
            inactiveElement.SetActive(!toggle.isOn);
        // ��ũ�� �� Ȱ��ȭ
        if (activeScrollView != null)
            activeScrollView.SetActive(toggle.isOn);
        // ��ũ�Ѻ� ��Ȱ��ȭ
        if (inactiveScrollView != null)
            inactiveScrollView.SetActive(!toggle.isOn);
    }
}