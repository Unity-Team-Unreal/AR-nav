using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathBoxScript : MonoBehaviour
{
    Animator _animator;
    [HideInInspector] public bool isActivate;
    [Header("��ã�� �ڽ� �Ӽ�")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;
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
