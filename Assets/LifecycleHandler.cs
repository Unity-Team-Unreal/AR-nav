using System.Collections;
using UnityEngine;

public class LifecycleHandler : MonoBehaviour
{
    private Animator animator;
    private bool firstAnimationDone = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
        StartCoroutine(ClickNextAnimation());
    }

    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
    }

    private IEnumerator ClickNextAnimation()
    {
        while (true)
        {
            // ���콺 Ŭ���̳� ��ġ �Է��� ���� ���� ���� �ִϸ��̼� Ʈ���Ÿ� �۵��մϴ�.
            if (firstAnimationDone && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Ŭ��");
                animator.Play("NextAnimation");
                StartCoroutine(DestroyAfterAnimation());
            }
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}