using UnityEngine;
using TMPro;

public class TextmeshController : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshes;  // ũ�⸦ ������ �ؽ�Ʈ �޽õ�
    public float increaseAmount = 1f;  // �ؽ�Ʈ ũ�� ������

    // ��ư Ŭ�� �� ȣ��� �޼���
    public void OnButtonClick()
    {
        // �迭�� ��� �ؽ�Ʈ �޽ÿ� ����
        foreach (var textMeshProUGUI in textMeshes)
        {
            textMeshProUGUI.fontSize = textMeshProUGUI.fontSize + increaseAmount;  // �ؽ�Ʈ ũ�� ����
        }
        Debug.Log("����");
    }
}

