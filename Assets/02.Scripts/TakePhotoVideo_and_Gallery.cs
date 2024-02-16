using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoVideo_and_Gallery : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGallery()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass intentStaticClass = new AndroidJavaClass("android.content.Intent");
        string actionView = intentStaticClass.GetStatic<string>("ACTION_VIEW");
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/external/images/media");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", actionView, uriObject);
        unityActivity.Call("startActivity", intent);
    }
}
