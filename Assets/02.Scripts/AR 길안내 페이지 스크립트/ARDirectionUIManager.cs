using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AR ��ã�� ���������� UI ����
/// </summary>
public class ARDirectionUIManager : MonoBehaviour
{
    [SerializeField] Button operationEndsButton;
    [SerializeField] Button pOILocationInformationButton;
    [SerializeField] Button popupEscapeButton;

    [SerializeField] GameObject couponInformationPopupPanel;

    public Text test3_text; // ���� ����

    void Start()
    {
        operationEndsButton.onClick.AddListener(OnOperationEnds);
        pOILocationInformationButton.onClick.AddListener(OnPOILocationInformation);
        popupEscapeButton.onClick.AddListener(OnPopupEscape);

        pOILocationInformationButton.gameObject.SetActive(false);
    }

    void OnOperationEnds()
    {
        SceneManager.LoadScene("KimHyoJin");
    }

    void OnPOILocationInformation()
    {
        couponInformationPopupPanel.SetActive(true);
        pOILocationInformationButton.gameObject.SetActive(false);
    }

    void OnPopupEscape()
    {
        couponInformationPopupPanel.SetActive(false);
        pOILocationInformationButton.gameObject.SetActive(true);
    }

    public void OnPOIButton(bool[] remainDistance)
    {
        for (int i = 0; i < remainDistance.Length; i++)
        {
            if (remainDistance[i])
            {
                test3_text.text = "�ȴ�";
                pOILocationInformationButton.gameObject.SetActive(true);
            }
            else
            {
                test3_text.text = "�� �ȴ�";
                pOILocationInformationButton.gameObject.SetActive(false);
            }
        }
    }
}
