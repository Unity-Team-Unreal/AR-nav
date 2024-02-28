using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChangeFontSize : MonoBehaviour
{
    public float scaleFactor; // 폰트 크기를 얼마나 확대할지를 결정하는 변수
    private Dictionary<TextMeshProUGUI, float> originalFontSizes = new Dictionary<TextMeshProUGUI, float>(); // 각 텍스트 오브젝트의 원래 폰트 사이즈를 저장하는 딕셔너리
    private bool isEnlarged = false; // 폰트 크기가 확대되었는지를 저장하는 변수
    private TextMeshProUGUI[] AutosizeText;

    void Awake()
    {
        List<TextMeshProUGUI> autoSizeTextComponents = new List<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textMeshPro in GetComponentsInChildren<TextMeshProUGUI>())
        {
            originalFontSizes[textMeshPro] = textMeshPro.fontSize;

            // AutoSizing이 가능한 컴포넌트인지 확인 후 리스트에 추가
            if (textMeshPro.enableAutoSizing)
            {
                autoSizeTextComponents.Add(textMeshPro);
            }
        }

        // List를 배열로 변환
        AutosizeText = autoSizeTextComponents.ToArray();
    }


    public void OnClickOrTouch()
    {
        // 부모 오브젝트의 모든 자식 오브젝트 중에서 TextMeshProUGUI 컴포넌트를 가지고 있는 오브젝트를 찾아서 폰트 크기를 변경
        foreach (TextMeshProUGUI textMeshPro in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (isEnlarged)
            {
                textMeshPro.fontSize = originalFontSizes[textMeshPro];
            }
            else
            {
                textMeshPro.fontSize = (int)(originalFontSizes[textMeshPro] + scaleFactor);
            }
        }

        foreach (var textMeshPro in AutosizeText)
        {
            if (isEnlarged)
            {
                textMeshPro.enableAutoSizing = false;
            }
            else
            {
                textMeshPro.enableAutoSizing = true;
            }
            
        }

        // isEnlarged 변수의 값을 반전
        isEnlarged = !isEnlarged;

    }
}
