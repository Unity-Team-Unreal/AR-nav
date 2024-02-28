using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Cat Animation Scripts
/// </summary>
public class CatHandler : MonoBehaviour
{
    private Animator animator; // Ĺ������
    private Animator Stateanimator; // Ĺ����
    private bool firstAnimationDone = false; // ù ��° �ִϸ��̼� ��� �Ϸ� ����

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Stateanimator = animator.transform.Find("StateCat").GetComponent<Animator>();
    }

    private void Start()
    {
        // ������ �ִϸ��̼� ���
        animator.Play("FirstAnimation");
        // ���� �ִϸ��̼� ���
        Stateanimator.Play("Walk");
    }

    private void Update()
    {
        // ù ��° �ִϸ��̼� ���� && ���콺 Ŭ�� �Ǵ� ��ġ
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // ���� ������ �ִϸ��̼� ���
            animator.Play("NextAnimation");
            // ���� �ִϸ��̼� ���� Ʈ���� ���
            Stateanimator.SetTrigger("NextAnimation");
            // �ִϸ��̼� �������� �ı�
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    // ù ��° �ִϸ��̼� �Ϸ� �̺�Ʈ�Լ�
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        Stateanimator.SetTrigger("StateChange");
    }

    
    private IEnumerator DestroyAfterAnimation()
    {
        // ��� ���� �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // ���� �������� �ı�
        Destroy(this.gameObject);
    }
}