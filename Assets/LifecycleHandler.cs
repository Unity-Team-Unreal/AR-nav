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
            // 마우스 클릭이나 터치 입력이 있을 때만 다음 애니메이션 트리거를 작동합니다.
            if (firstAnimationDone && Input.GetMouseButtonDown(0))
            {
                Debug.Log("클릭");
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