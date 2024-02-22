using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessege : MonoBehaviour
{
    /// <summary>
    /// 안드로이드 토스트 메세지를 띄우기 위한 스크립트이며 아직 미완성
    /// </summary>
    static ToastMessege instance;
    float fadeDurationTime;

    private void Awake()
    {
        if (instance==null) instance = this;
    }

    public static ToastMessege Toast
    {
        get 
        {
            if(instance == null) instance = FindObjectOfType<ToastMessege>();
            return instance;
        }
    }

    void showText()
    {

    }

}
