using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public AnimationClip legacyClip;
    private Animation animation;

    void Start()
    {
        // Animation ������Ʈ�� ���ɴϴ�.
        animation = GetComponent<Animation>();

        // ���Ž� �ִϸ��̼� Ŭ���� �߰��մϴ�.
        animation.AddClip(legacyClip, legacyClip.name);
    }

    // �� �Լ��� �ִϸ����� ��Ʈ�ѷ����� �ִϸ��̼��� ������ �� ȣ��˴ϴ�.
    public void OnAnimationEnd()
    {
        // ���Ž� �ִϸ��̼� Ŭ���� ����մϴ�.
        animation.Play(legacyClip.name);
    }
}