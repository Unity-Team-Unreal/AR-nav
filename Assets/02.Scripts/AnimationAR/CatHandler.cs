using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Cat Animation Scripts
/// </summary>
public class CatHandler : MonoBehaviour
{
    private Animator animator; // 캣움직임
    private Animator Stateanimator; // 캣상태
    private bool firstAnimationDone = false; // 첫 번째 애니메이션 재생 완료 상태

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Stateanimator = animator.transform.Find("StateCat").GetComponent<Animator>();
    }

    private void Start()
    {
        // 움직임 애니메이션 재생
        animator.Play("FirstAnimation");
        // 상태 애니메이션 재생
        Stateanimator.Play("Walk");
    }

    private void Update()
    {
        // 첫 번째 애니메이션 종료 && 마우스 클릭 또는 터치
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // 다음 움직임 애니메이션 재생
            animator.Play("NextAnimation");
            // 상태 애니메이션 다음 트리거 재생
            Stateanimator.SetTrigger("NextAnimation");
            // 애니메이션 오브젝터 파괴
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    // 첫 번째 애니메이션 완료 이벤트함수
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        Stateanimator.SetTrigger("StateChange");
    }

    
    private IEnumerator DestroyAfterAnimation()
    {
        // 재생 중인 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // 게임 오브젝터 파괴
        Destroy(this.gameObject);
    }
}