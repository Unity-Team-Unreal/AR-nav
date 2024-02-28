using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGallery : MonoBehaviour
{
    /// <summary>
    /// 갤러리 앱 실행(삼성폰 기준)
    /// </summary>
    public void Gallery()
    {
        //유니티 플레이어 활성 객체 가져오기
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        //Intent 객체 생성(갤러리 실행)
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("setAction", "android.intent.action.MAIN");
        intent.Call<AndroidJavaObject>("addCategory", "android.intent.category.LAUNCHER");
        intent.Call<AndroidJavaObject>("setPackage", "com.sec.android.gallery3d");

        //갤러리 앱 실행
        currentActivity.Call("startActivity", intent);

    }
}
