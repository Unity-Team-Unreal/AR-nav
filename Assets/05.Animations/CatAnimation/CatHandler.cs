using System.Collections;
using UnityEngine;

public class CatHandler : MonoBehaviour
{
    private Animator animator;
    private Animator Stateanimator;
    private bool firstAnimationDone = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Stateanimator = animator.transform.Find("StateCat").GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
        Stateanimator.Play("Walk");
    }

    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        Stateanimator.SetTrigger("StateChange");
    }

    private void Update()
    {
        // 마우스 클릭이나 터치 입력이 있을 때만 다음 애니메이션 트리거를 작동합니다.
        if (firstAnimationDone && Input.GetMouseButtonDown(0))
        {
            Debug.Log("클릭");
            animator.Play("NextAnimation");
            Stateanimator.SetTrigger("NextAnimation");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}