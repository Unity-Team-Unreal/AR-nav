using UnityEngine;

/// <summary>
/// ��ư ������ 112��ư ����
/// </summary>
public class DialButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        Application.OpenURL("tel:112");
    }
}