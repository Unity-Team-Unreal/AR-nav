using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARDirectionUIManager : MonoBehaviour
{
    [SerializeField] Button operationEndsButton;
    [SerializeField] Button pOILocationInformationButton;
    [SerializeField] Button popupEscapeButton;

    [SerializeField] GameObject couponInformationPopupPanel;

    void Start()
    {
        operationEndsButton.onClick.AddListener(OnOperationEnds);
        pOILocationInformationButton.onClick.AddListener(OnPOILocationInformation);
        popupEscapeButton.onClick.AddListener(OnPopupEscape);
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

}
