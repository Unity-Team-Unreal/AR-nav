using UnityEngine;

/// <summary>
/// 버튼 누르면 112버튼 연결
/// </summary>
public class DialButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        Application.OpenURL("tel:112");
    }
}