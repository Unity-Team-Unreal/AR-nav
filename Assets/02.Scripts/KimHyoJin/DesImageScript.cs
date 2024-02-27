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

   public void desBoxEneable()
    {
        DesBoxImage.enabled = true;
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
