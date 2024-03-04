using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AR ��ã�� ���������� POI ������ ��û �� ���� �����ͷ� UI����
/// </summary>
public class ARDirectionUIManager : MonoBehaviour
{
    [Header("POI��� ����� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI businessName;
    [SerializeField] TextMeshProUGUI branchName;
    [SerializeField] TextMeshProUGUI getCoupon;

    [SerializeField] TextMeshProUGUI popupBusinessName;
    [SerializeField] TextMeshProUGUI address;
    [SerializeField] TextMeshProUGUI openingHours;
    [SerializeField] TextMeshProUGUI couponEventInformationText;

    [Header("POI��� ����� �̹���")]
    [SerializeField] Image classification;
    [SerializeField] Image couponIcon;

    [SerializeField] Image popupClassification;
    [SerializeField] Image popupCouponIcon;
    [SerializeField] Image barcode;

    [Header("������ �� ���ڵ�")]
    [SerializeField] Sprite icon_cafe;
    [SerializeField] Sprite icon_restaurant;
    [SerializeField] Sprite icon_Publicplaces;
    [SerializeField] Sprite icon_coupon;
    [SerializeField] Sprite icon_note;
    [SerializeField] Sprite icon_barcode;

    [Header("��ư �� �ǳ�")]
    [SerializeField] Button operationEndsButton;
    [SerializeField] Button pOILocationInformationButton;
    [SerializeField] Button popupEscapeButton;

    [SerializeField] GameObject couponInformationPopupPanel;

    string[] categorys;
    string[] names;
    string[] branchs;
    string[] addresses;
    string[] descriptions;
    string[] eventinformations;

    void Start()
    {
        operationEndsButton.onClick.AddListener(OnOperationEnds);
        pOILocationInformationButton.onClick.AddListener(OnPOILocationInformation);
        popupEscapeButton.onClick.AddListener(OnPopupEscape);

        pOILocationInformationButton.gameObject.SetActive(false);

        StartCoroutine(RequestPOI());
    }
    /// <summary>
    /// POI �����͵��� ���� ���ϰ� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator RequestPOI()
    {
        while (true)
        {
            if (POI.datalist.Count > 0) break;
            yield return null;
        }
        categorys = new string[POI.datalist.Count];
        names = new string[POI.datalist.Count];
        branchs = new string[POI.datalist.Count];
        addresses = new string[POI.datalist.Count];
        descriptions = new string[POI.datalist.Count];
        eventinformations = new string[POI.datalist.Count];
        
        for (int i = 0; i < POI.datalist.Count; i++)
        {
            categorys[i] = POI.datalist[i].Category();
            names[i] = POI.datalist[i].Name();
            branchs[i] = POI.datalist[i].Branch();
            addresses[i] = POI.datalist[i].Address();
            descriptions[i] = POI.datalist[i].Description();
            eventinformations[i] = POI.datalist[i].Eventinformation();
        }
        
        yield break;
    }
    /// <summary>
    /// ���� ���� ��ư�� ������ ��
    /// </summary>
    void OnOperationEnds()
    {
        SceneManager.LoadScene("1_1.Navigation");
    }
    /// <summary>
    /// POI ��Ҿȳ� ��ư�� ������ ��
    /// </summary>
    void OnPOILocationInformation()
    {
        couponInformationPopupPanel.SetActive(true);
        pOILocationInformationButton.gameObject.SetActive(false);
    }
    /// <summary>
    /// �˾�â �ݱ� ��ư�� ������ ��
    /// </summary>
    void OnPopupEscape()
    {
        couponInformationPopupPanel.SetActive(false);
        pOILocationInformationButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// POI �����ͷ� �� ��ǥ�� �����Ÿ� �ٰ����� �� �׿� �´� ������ �����ϰ� ��ư UI�� ����.
    /// </summary>
    /// <param name="remainDistance"></param>
    public void OnPOIButton(bool[] remainDistance)
    {
        for (int i = 0; i < POI.datalist.Count; i++)
        {
            // UI ���� �� ��ǥ�� �´� ���� ����
            if (remainDistance[i])
            {
                pOILocationInformationButton.gameObject.SetActive(true);

                businessName.text = names[i];
                branchName.text = branchs[i];
                getCoupon.text = (categorys[i] == "�������")? "-" : "�����ޱ�";
                popupBusinessName.text = names[i];
                address.text = addresses[i];
                openingHours.text = descriptions[i];
                couponEventInformationText.text = eventinformations[i];

                classification.sprite = (categorys[i] == "ī��") ? icon_cafe : (categorys[i] == "�Ĵ�") ? icon_restaurant : icon_Publicplaces;
                couponIcon.sprite = (categorys[i] == "�������") ? icon_note : icon_coupon;
                popupClassification.sprite = (categorys[i] == "ī��") ? icon_cafe : (categorys[i] == "�Ĵ�") ? icon_restaurant : icon_Publicplaces;
                popupCouponIcon.sprite = (categorys[i] == "�������") ? icon_note : icon_coupon;
                barcode.sprite = (categorys[i] == "�������") ? null : icon_barcode;
            }
            else
            {
                // �ش� POI�� ���� �Ÿ��� 30���� �̻��̸� UI ��Ȱ��ȭ
                pOILocationInformationButton.gameObject.SetActive(false);
            }
        }
    }
}
