using System.Collections;
using UnityEngine;

/// <summary>
/// 나비 애니메이션 스크립트
/// 
/// </summary>
public class ButterflyAnimation : MonoBehaviour
{
    // 애니메이션 컴포넌트
    private Animation legacyAnimation;

    // 첫 번째 애니메이션 재생 완료 상태
    private bool firstAnimationDone = false;

    private void Awake()
    {
        legacyAnimation = GetComponent<Animation>();
    }

    private void Start()
    {
        // "Movebutterfly" 애니메이션 시작
        legacyAnimation.Play("Movebutterfly");

        StartCoroutine(CheckAnimationStatus());
    }

    private void Update()
    {
        // 첫 번째 애니메이션 종료 && 마우스 클릭 또는 터치
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // 두 번째 애니메이션 재생
            StartCoroutine(PlaySecondAnimationAndDestroy());
        }
    }


    // 애니메이션 상태 확인
    private IEnumerator CheckAnimationStatus()
    {
        //애니메이션 재생 중
        while (legacyAnimation.isPlaying)
        {
            yield return null;
        }

        // 첫 번째 애니메이션 종료 표시
        firstAnimationDone = true;
    }

    // 두 번째 애니메이션 재생 및 삭제
    private IEnumerator PlaySecondAnimationAndDestroy()
    {
        // "Exitbutterfly" 애니메이션 시작
        legacyAnimation.Play("Exitbutterfly");

        // 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(legacyAnimation["Exitbutterfly"].length);

        // 게임 오브젝트 삭제
        Destroy(this.gameObject);
    }
}