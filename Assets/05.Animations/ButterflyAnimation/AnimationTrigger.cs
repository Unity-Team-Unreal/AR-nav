using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public AnimationClip legacyClip;
    private Animation animation;

    void Start()
    {
        // Animation 컴포넌트를 얻어옵니다.
        animation = GetComponent<Animation>();

        // 레거시 애니메이션 클립을 추가합니다.
        animation.AddClip(legacyClip, legacyClip.name);
    }

    // 이 함수는 애니메이터 컨트롤러에서 애니메이션이 끝났을 때 호출됩니다.
    public void OnAnimationEnd()
    {
        // 레거시 애니메이션 클립을 재생합니다.
        animation.Play(legacyClip.name);
    }
}