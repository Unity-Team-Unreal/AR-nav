using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AR ������Ʈ�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ
public class AnimationButterfly : MonoBehaviour
{
    private Animator animator; 
    private bool isFirstAnimationDone = false;

    // ������Ʈ�� Animator ������Ʈ�� �����ϴ� �Լ�
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // AR �̹��� ��Ī���� ������Ʈ�� ������ �� ȣ��Ǵ� �Լ�
    // (ImageTrackingObjectManager���� ������ �� �ֽ��ϴ�)
    public void OnObjectCreated()
    {
        // ù ��° �ִϸ��̼��� �����մϴ�.
        PlayFirstAnimation();
    }

    // ù ��° �ִϸ��̼��� ����ϴ� �Լ�
    private void PlayFirstAnimation()
    {
        // ù ��° �ִϸ��̼� Ʈ���Ÿ� �����մϴ�.
        animator.SetTrigger("FirstAnimation");
    }

    // Update �Լ��� ����� �Է��� �����ϱ� ���� ����ؼ� ȣ��˴ϴ�.
    void Update()
    {
        // ����ڰ� ȭ���� ��ġ�ϰų� Ŭ���ߴ��� ����
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // ù ��° �ִϸ��̼��� �����ٸ� ���� �ִϸ��̼��� ����
            if (isFirstAnimationDone)
            {
                Debug.Log("Ŭ��");
                AnimationTrigger animationTrigger = GetComponent<AnimationTrigger>();
                animationTrigger.OnAnimationEnd();
            }
        }
    }

    // ù ��° �ִϸ��̼��� �������� �˸��� �Լ� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ��� �� �ֽ��ϴ�)
    public void OnFirstAnimationDone()
    {
        isFirstAnimationDone = true;
    }

    // ������ �ִϸ��̼ǿ��� ȣ��� �Լ� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ��˴ϴ�)
    private void DestroyObject()
    {
        // GameObject�� �ı��մϴ�.
        Destroy(gameObject);
    }
}
