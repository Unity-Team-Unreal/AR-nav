using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AR 길찾기 페이지에서 POI 데이터 요청 및 받은 데이터로 UI조작
/// </summary>
public class ARDirectionUIManager : MonoBehaviour
{
    [Header("POI대로 변경될 텍스트")]
    [SerializeField] TextMeshProUGUI businessName;
    [SerializeField] TextMeshProUGUI branchName;
    [SerializeField] TextMeshProUGUI getCoupon;

    [SerializeField] TextMeshProUGUI popupBusinessName;
    [SerializeField] TextMeshProUGUI address;
    [SerializeField] TextMeshProUGUI openingHours;
    [SerializeField] TextMeshProUGUI couponEventInformationText;

    [Header("POI대로 변경될 이미지")]
    [SerializeField] Image classification;
    [SerializeField] Image couponIcon;

    [SerializeField] Image popupClassification;
    [SerializeField] Image popupCouponIcon;
    [SerializeField] Image barcode;

    [Header("아이콘 및 바코드")]
    [SerializeField] Sprite icon_cafe;
    [SerializeField] Sprite icon_restaurant;
    [SerializeField] Sprite icon_Publicplaces;
    [SerializeField] Sprite icon_coupon;
    [SerializeField] Sprite icon_note;
    [SerializeField] Sprite icon_barcode;

    [Header("버튼 및 판넬")]
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
    /// POI 데이터들을 쓰기 편하게 저장
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
    /// 운행 종료 버튼을 눌렀을 때
    /// </summary>
    void OnOperationEnds()
    {
        SceneManager.LoadScene("1_1.Navigation");
    }
    /// <summary>
    /// POI 장소안내 버튼을 눌렀을 때
    /// </summary>
    void OnPOILocationInformation()
    {
        couponInformationPopupPanel.SetActive(true);
        pOILocationInformationButton.gameObject.SetActive(false);
    }
    /// <summary>
    /// 팝업창 닫기 버튼을 눌렀을 때
    /// </summary>
    void OnPopupEscape()
    {
        couponInformationPopupPanel.SetActive(false);
        pOILocationInformationButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// POI 데이터로 그 좌표에 일정거리 다가왔을 시 그에 맞는 정보를 갱신하고 버튼 UI를 띄운다.
    /// </summary>
    /// <param name="remainDistance"></param>
    public void OnPOIButton(bool[] remainDistance)
    {
        for (int i = 0; i < POI.datalist.Count; i++)
        {
            // UI 띄우기 및 좌표에 맞는 정보 갱신
            if (remainDistance[i])
            {
                pOILocationInformationButton.gameObject.SetActive(true);

                businessName.text = names[i];
                branchName.text = branchs[i];
                getCoupon.text = (categorys[i] == "공공장소")? "-" : "쿠폰받기";
                popupBusinessName.text = names[i];
                address.text = addresses[i];
                openingHours.text = descriptions[i];
                couponEventInformationText.text = eventinformations[i];

                classification.sprite = (categorys[i] == "카페") ? icon_cafe : (categorys[i] == "식당") ? icon_restaurant : icon_Publicplaces;
                couponIcon.sprite = (categorys[i] == "공공장소") ? icon_note : icon_coupon;
                popupClassification.sprite = (categorys[i] == "카페") ? icon_cafe : (categorys[i] == "식당") ? icon_restaurant : icon_Publicplaces;
                popupCouponIcon.sprite = (categorys[i] == "공공장소") ? icon_note : icon_coupon;
                barcode.sprite = (categorys[i] == "공공장소") ? null : icon_barcode;
            }
            else
            {
                // 해당 POI에 대한 거리가 30미터 이상이면 UI 비활성화
                pOILocationInformationButton.gameObject.SetActive(false);
            }
        }
    }
}
