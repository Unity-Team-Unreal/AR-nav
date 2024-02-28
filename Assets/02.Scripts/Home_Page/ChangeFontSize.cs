using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChangeFontSize : MonoBehaviour
{
    public float scaleFactor; // ��Ʈ ũ�⸦ �󸶳� Ȯ�������� �����ϴ� ����
    private Dictionary<TextMeshProUGUI, float> originalFontSizes = new Dictionary<TextMeshProUGUI, float>(); // �� �ؽ�Ʈ ������Ʈ�� ���� ��Ʈ ����� �����ϴ� ��ųʸ�
    private bool isEnlarged = false; // ��Ʈ ũ�Ⱑ Ȯ��Ǿ������� �����ϴ� ����
    private TextMeshProUGUI[] AutosizeText;

    void Awake()
    {
        List<TextMeshProUGUI> autoSizeTextComponents = new List<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textMeshPro in GetComponentsInChildren<TextMeshProUGUI>())
        {
            originalFontSizes[textMeshPro] = textMeshPro.fontSize;

            // AutoSizing�� ������ ������Ʈ���� Ȯ�� �� ����Ʈ�� �߰�
            if (textMeshPro.enableAutoSizing)
            {
                autoSizeTextComponents.Add(textMeshPro);
            }
        }

        // List�� �迭�� ��ȯ
        AutosizeText = autoSizeTextComponents.ToArray();
    }


    public void OnClickOrTouch()
    {
        // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ �߿��� TextMeshProUGUI ������Ʈ�� ������ �ִ� ������Ʈ�� ã�Ƽ� ��Ʈ ũ�⸦ ����
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

        // isEnlarged ������ ���� ����
        isEnlarged = !isEnlarged;

    }
}
