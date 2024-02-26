using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RestartAR : MonoBehaviour
{ 
    public ARSession session;

    private void Awake()
    {
        Restart();
    }

    public void Restart()
    {
        session.Reset();
    }
}

