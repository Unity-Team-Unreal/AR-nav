using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathBoxScript : MonoBehaviour
{
    /// <summary>
    /// ��Ŀ�� ������ �� ������ ���� ������ �ϴ��� ��ã�� ��ư�� ������ ��, ��ܿ� �߰��Ǵ� �����/������ UI�� �����ϴ� ��ũ��Ʈ
    /// </summary>
    Animator _animator;     //�ִϸ��̼����� ������ ���̱� ����
    [HideInInspector] public bool isActivate;   //��ã�������� Ȯ��, �ڷΰ��� ��ư���� ������ ���̱� ������ public
    [Header("��ã�� �ڽ� �Ӽ�")]
    [SerializeField] InputField start;
    [SerializeField] InputField goal;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PathBoxActivate()   //��ã�� UI�� Ȱ��ȭ
    {
        //GPS ���ٱ����� ���Ǿ� �ִٸ�
        isActivate = true;
        _animator.Play("Play");
    }

    public void PathBoxDeactivate() //��ã�� UI ��Ȱ��ȭ
    {
        isActivate = false;
        _animator.Play("Reverse");
    }
}
