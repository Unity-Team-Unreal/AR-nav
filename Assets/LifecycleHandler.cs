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
        // 첫 번째 애니메이션을 재생합니다.
        animator.Play("FirstAnimation");
    }

    // 애니메이션 이벤트로 호출될 메서드
    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        // idleTime을 체크하는 코루틴을 시작합니다.
        StartCoroutine(CheckIdleTime());
    }

    private IEnumerator CheckIdleTime()
    {
        while (true)
        {
            // 마지막 클릭으로부터 5초 동안 추가 클릭이 없었다면 두 번째 애니메이션을 재생합니다.
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

    // 오브젝트가 클릭되었을 때 호출됩니다.
    private void OnMouseDown()
    {
        if (!firstAnimationDone)
        {
            // 첫 번째 애니메이션이 아직 끝나지 않았다면 클릭을 무시합니다.
            return;
        }

        // 마지막 클릭 시간을 업데이트하고, 오브젝트가 클릭되면 첫 번째 애니메이션을 재생합니다.
        lastClickTime = Time.time;
        animator.Play("OnClickAnimation");
    }
}