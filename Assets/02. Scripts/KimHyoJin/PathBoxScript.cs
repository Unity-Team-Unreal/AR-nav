using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBoxScript : MonoBehaviour
{
    Animator _animator;
    [HideInInspector] public bool isActivate;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PathBoxActivate()
    {
        //GPS 접근권한이 허용되어 있다면
        isActivate = true;
        _animator.Play("Play");
    }

    public void PathBoxDeactivate()
    {
        isActivate = false;
        _animator.Play("Reverse");
    }
}
