using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGallery : MonoBehaviour
{
    public void Gallery()
    {
        NativeGallery.GetImageFromGallery(path => { }, "/storage/emulated/0/DCIM/AR-Nav");
    }
}
