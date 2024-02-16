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
        //GPS ���ٱ����� ���Ǿ� �ִٸ�
        isActivate = true;
        _animator.Play("Play");
    }

    public void PathBoxDeactivate()
    {
        isActivate = false;
        _animator.Play("Reverse");
    }
}
