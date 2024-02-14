using UnityEngine;

public class CallButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        Application.OpenURL("telprompt://112");
    }
}
