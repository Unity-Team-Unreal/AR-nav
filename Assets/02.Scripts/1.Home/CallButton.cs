using UnityEngine;

public class DialButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        Application.OpenURL("tel:112");
    }
}