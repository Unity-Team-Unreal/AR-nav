using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBoxScript : MonoBehaviour
{
    Animator _animator;
    [HideInInspector] public bool isSearching;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PassBoxActivate()
    {
        //GPS 접근권한이 허용되어 있다면
        isSearching = true;
        _animator.Play("Play");
    }

    public void PassBoxDeactivate()
    {
        isSearching = false;
        _animator.Play("Reverse");
    }
}
