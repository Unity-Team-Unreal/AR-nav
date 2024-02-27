using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesImageScript : MonoBehaviour
{
     RawImage DesBoxImage;
    void Awake()
    {
        DesBoxImage = GetComponent<RawImage>();
        DesBoxImage.enabled = false;
    }

   public void desBoxEneable(POIData data)
    {
        string a;

        if (data.Number() == 1)  a = "������1";
        else if (data.Number() == 2)  a = "������2";
        else if (data.Number() == 3) a = "������3";
        else if (data.Number() == 4)  a = "������4";
        else a = "������5";
        DesBoxImage.enabled = true;
        DesBoxImage.texture = Resources.Load<Texture2D>($"Photoimage/{a}");
        DesBoxImage.color = new Color(1, 1, 1, 1);
    }

    public void desBoxEneableButNoImage()
    {
        DesBoxImage.enabled = true;
        DesBoxImage.color = new Color(1,1,1,0);
    }

    public  void desBoxDisable()
    {
        DesBoxImage.enabled = false;
    }

}
