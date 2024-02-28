using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 기차 오브젝트 애니메이션 움직임 구현 스크립트
/// </summary>
public class TrainLifeSycle : MonoBehaviour
{
    private Animator animator;
    private bool firstAnimationDone = false; // 첫 번째 애니메이션 재생 완료 여부 플래그

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
    }

    // 첫 번째 애니메이션 완료 알림 함수
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
    }

    private void Update()
    {
        // 첫 번째 애니메이션 종료 && 마우스 클릭 또는 터치
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("클릭");
            //다음 움직임 애니메이션 재생
            animator.Play("NextAnimation");
            //애니메이션 끝나고 오브젝터 제거
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // 현재 재생 중인 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}