using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{

    [SerializeField] public Toggle toggleA;
    [SerializeField] public Toggle toggleB;
    [SerializeField] private GameObject imageA;
    [SerializeField] private GameObject imageB;
    [SerializeField] private GameObject scrollViewA;
    [SerializeField] private GameObject scrollViewB;


    private Toggle activeToggle;

    public Toggle ActiveToggle { get { return activeToggle; } private set { activeToggle = value; } }



    void Awake()
    {
        toggleA.group = toggleB.group; // ��� �׷� ����

        // ���� ���� �̺�Ʈ�� ���� ������ ���
        toggleA.onValueChanged.AddListener(OnToggleValueChanged);
        toggleB.onValueChanged.AddListener(OnToggleValueChanged);

        // toggleA�� �ʱ⿡ Ȱ��ȭ
        toggleA.isOn = true;


        // toggleB�� �ʱ⿡ ��Ȱ��ȭ
        toggleB.isOn = false;

        ToggleElements(toggleA, imageA, imageB, scrollViewA, scrollViewB);

        activeToggle = toggleA;

    }

    void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            if (toggleA.isOn)
            {


                ToggleElements(toggleA, imageA, imageB, scrollViewA, scrollViewB);
                activeToggle = toggleA;


            }
            else if (toggleB.isOn)
            {


                ToggleElements(toggleB, imageB, imageA, scrollViewB, scrollViewA);
                activeToggle = toggleB;


            }
        }
    }

    // �̹����� ��ũ�Ѻ並 Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
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