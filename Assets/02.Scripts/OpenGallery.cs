using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGallery : MonoBehaviour
{
    public void Gallery()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");

        intent.Call<AndroidJavaObject>("setAction", "android.intent.action.MAIN");
        intent.Call<AndroidJavaObject>("addCategory", "android.intent.category.LAUNCHER");
        intent.Call<AndroidJavaObject>("setPackage", "com.sec.android.gallery3d");

        currentActivity.Call("startActivity", intent);

    }
}
