using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ���� ������Ʈ �ִϸ��̼� ������ ���� ��ũ��Ʈ
/// </summary>
public class TrainLifeSycle : MonoBehaviour
{
    private Animator animator;
    private bool firstAnimationDone = false; // ù ��° �ִϸ��̼� ��� �Ϸ� ���� �÷���

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
    }

    // ù ��° �ִϸ��̼� �Ϸ� �˸� �Լ�
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
    }

    private void Update()
    {
        // ù ��° �ִϸ��̼� ���� && ���콺 Ŭ�� �Ǵ� ��ġ
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Ŭ��");
            //���� ������ �ִϸ��̼� ���
            animator.Play("NextAnimation");
            //�ִϸ��̼� ������ �������� ����
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // ���� ��� ���� �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}