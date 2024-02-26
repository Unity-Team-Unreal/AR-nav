using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AR ������Ʈ�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ
public class AnimationCat : MonoBehaviour
{
    private Animator Catmoveani; // Animator ������Ʈ�� ���� ����
    private Animator Stateanimator;
    private bool isFirstAnimationDone = false; // ù ��° �ִϸ��̼��� ���������� ����

    // ������Ʈ�� Animator ������Ʈ�� �����ϴ� �Լ�
    void Awake()
    {
        Catmoveani = GetComponent<Animator>();
        Stateanimator = Catmoveani.transform.Find("StateCat").GetComponent<Animator>();

        if (Stateanimator != null)
        {
            Debug.Log("�ڽ� ������Ʈ�� �ִϸ��̼� ������Ʈ�� ���������� �����Խ��ϴ�.");
        }
        else
        {
            Debug.Log("�ڽ� ������Ʈ�� �ִϸ��̼� ������Ʈ�� �����ϴ�.");
        }
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
                Catmoveani.SetTrigger("NextAnimation");
                Stateanimator.SetTrigger("NextAnimation");
            }
        }
    }

    // ù ��° �ִϸ��̼��� �������� �˸��� �Լ� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ��� �� �ֽ��ϴ�)
    public void OnFirstAnimationDone()
    {
        isFirstAnimationDone = true;
        if (isFirstAnimationDone == true)
        {
            Stateanimator.SetTrigger("StateChange");
        }
    }

    // ������ �ִϸ��̼ǿ��� ȣ��� �Լ� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ��˴ϴ�)
    private void DestroyObject()
    {
        // GameObject�� �ı��մϴ�.
        Destroy(gameObject);
    }
}
