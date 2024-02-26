using System.Collections;
using UnityEngine;

public class LifecycleHandler : MonoBehaviour
{
    private Animator animator;
    private bool firstAnimationDone = false;
    private float lastClickTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // ù ��° �ִϸ��̼��� ����մϴ�.
        animator.Play("FirstAnimation");
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��� �޼���
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        // idleTime�� üũ�ϴ� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(CheckIdleTime());
    }

    private IEnumerator CheckIdleTime()
    {
        while (true)
        {
            // ������ Ŭ�����κ��� 5�� ���� �߰� Ŭ���� �����ٸ� �� ��° �ִϸ��̼��� ����մϴ�.
            if (Time.time - lastClickTime >= 5f)
            {
                animator.Play("NextAnimation");
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
                ObjectPool.Instance.ReturnToPool(this.gameObject);
                yield break;
            }

            yield return null;
        }
    }

    // ������Ʈ�� Ŭ���Ǿ��� �� ȣ��˴ϴ�.
    private void OnMouseDown()
    {
        if (!firstAnimationDone)
        {
            // ù ��° �ִϸ��̼��� ���� ������ �ʾҴٸ� Ŭ���� �����մϴ�.
            return;
        }

        // ������ Ŭ�� �ð��� ������Ʈ�ϰ�, ������Ʈ�� Ŭ���Ǹ� ù ��° �ִϸ��̼��� ����մϴ�.
        lastClickTime = Time.time;
        animator.Play("OnClickAnimation");
    }
}