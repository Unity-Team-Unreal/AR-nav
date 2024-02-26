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
    }

    public  void desBoxDisable()
    {
        DesBoxImage.enabled = false;
    }

}
