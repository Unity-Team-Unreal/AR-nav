using UnityEngine;
using TMPro;

public class TextmeshController : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshes;  // 크기를 변경할 텍스트 메시들
    public float increaseAmount = 1f;  // 텍스트 크기 증가량

    // 버튼 클릭 시 호출될 메서드
    public void OnButtonClick()
    {
        // 배열의 모든 텍스트 메시에 대해
        foreach (var textMeshProUGUI in textMeshes)
        {
            textMeshProUGUI.fontSize = textMeshProUGUI.fontSize + increaseAmount;  // 텍스트 크기 증가
        }
        Debug.Log("눌림");
    }
}

