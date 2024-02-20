using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

public class ARDirectionsManager : MonoBehaviour
{
    [SerializeField] string point;

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {

        }
    }
}
