using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AR ������Ʈ�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ
public class AnimationTrain : MonoBehaviour
{
    private Animator trainmoveani; // Animator ������Ʈ�� ���� ����
    private bool isFirstAnimationDone = false; // ù ��° �ִϸ��̼��� ���������� ����

    // ������Ʈ�� Animator ������Ʈ�� �����ϴ� �Լ�
    void Awake()
    {
        trainmoveani = GetComponent<Animator>();
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
                trainmoveani.SetTrigger("NextAnimation");
            }
            else
            {
                Debug.Log("�ȳ���");
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
