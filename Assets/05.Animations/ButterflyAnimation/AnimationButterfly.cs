using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AR 오브젝트의 애니메이션을 관리하는 스크립트
public class AnimationButterfly : MonoBehaviour
{
    private Animator animator; 
    private bool isFirstAnimationDone = false;

    // 오브젝트의 Animator 컴포넌트를 참조하는 함수
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // AR 이미지 매칭으로 오브젝트가 생성될 때 호출되는 함수
    // (ImageTrackingObjectManager에서 실행할 수 있습니다)
    public void OnObjectCreated()
    {
        // 첫 번째 애니메이션을 실행합니다.
        PlayFirstAnimation();
    }

    // 첫 번째 애니메이션을 재생하는 함수
    private void PlayFirstAnimation()
    {
        // 첫 번째 애니메이션 트리거를 설정합니다.
        animator.SetTrigger("FirstAnimation");
    }

    // Update 함수는 사용자 입력을 감지하기 위해 계속해서 호출됩니다.
    void Update()
    {
        // 사용자가 화면을 터치하거나 클릭했는지 감지
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // 첫 번째 애니메이션이 끝났다면 다음 애니메이션을 실행
            if (isFirstAnimationDone)
            {
                Debug.Log("클릭");
                AnimationTrigger animationTrigger = GetComponent<AnimationTrigger>();
                animationTrigger.OnAnimationEnd();
            }
        }
    }

    // 첫 번째 애니메이션이 끝났음을 알리는 함수 (애니메이션 이벤트에 의해 호출될 수 있습니다)
    public void OnFirstAnimationDone()
    {
        isFirstAnimationDone = true;
    }

    // 마지막 애니메이션에서 호출될 함수 (애니메이션 이벤트에 의해 호출됩니다)
    private void DestroyObject()
    {
        // GameObject를 파괴합니다.
        Destroy(gameObject);
    }
}
