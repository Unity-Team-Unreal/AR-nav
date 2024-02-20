using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessege : MonoBehaviour
{
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
