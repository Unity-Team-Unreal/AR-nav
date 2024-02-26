using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AR 오브젝트의 애니메이션을 관리하는 스크립트
public class AnimationCat : MonoBehaviour
{
    private Animator Catmoveani; // Animator 컴포넌트에 대한 참조
    private Animator Stateanimator;
    private bool isFirstAnimationDone = false; // 첫 번째 애니메이션이 끝났는지의 여부

    // 오브젝트의 Animator 컴포넌트를 참조하는 함수
    void Awake()
    {
        Catmoveani = GetComponent<Animator>();
        Stateanimator = Catmoveani.transform.Find("StateCat").GetComponent<Animator>();

        if (Stateanimator != null)
        {
            Debug.Log("자식 오브젝트의 애니메이션 컴포넌트를 성공적으로 가져왔습니다.");
        }
        else
        {
            Debug.Log("자식 오브젝트에 애니메이션 컴포넌트가 없습니다.");
        }
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
                Catmoveani.SetTrigger("NextAnimation");
                Stateanimator.SetTrigger("NextAnimation");
            }
        }
    }

    // 첫 번째 애니메이션이 끝났음을 알리는 함수 (애니메이션 이벤트에 의해 호출될 수 있습니다)
    public void OnFirstAnimationDone()
    {
        isFirstAnimationDone = true;
        if (isFirstAnimationDone == true)
        {
            Stateanimator.SetTrigger("StateChange");
        }
    }

    // 마지막 애니메이션에서 호출될 함수 (애니메이션 이벤트에 의해 호출됩니다)
    private void DestroyObject()
    {
        // GameObject를 파괴합니다.
        Destroy(gameObject);
    }
}
