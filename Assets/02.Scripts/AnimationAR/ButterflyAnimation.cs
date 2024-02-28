using System.Collections;
using UnityEngine;

public class ButterflyAnimation : MonoBehaviour
{
    // �ִϸ��̼� ������Ʈ
    private Animation legacyAnimation;

    // ù ��° �ִϸ��̼� ���� ����
    private bool firstAnimationDone = false;

    private void Awake()
    {
        legacyAnimation = GetComponent<Animation>();
    }

    private void Start()
    {
        // "Movebutterfly" �ִϸ��̼� ����
        legacyAnimation.Play("Movebutterfly");

        StartCoroutine(CheckAnimationStatus());
    }

    private void Update()
    {
        // ù ��° �ִϸ��̼� ���� && ���콺 Ŭ�� �Ǵ� ��ġ
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Ŭ��");

            // �� ��° �ִϸ��̼� ���
            StartCoroutine(PlaySecondAnimationAndDestroy());
        }
    }


    // �ִϸ��̼� ���� Ȯ��
    private IEnumerator CheckAnimationStatus()
    {
        //�ִϸ��̼� ��� ��
        while (legacyAnimation.isPlaying)
        {
            yield return null;
        }

        // ù ��° �ִϸ��̼� ���� ǥ��
        firstAnimationDone = true;
    }

    // �� ��° �ִϸ��̼� ��� �� ����
    private IEnumerator PlaySecondAnimationAndDestroy()
    {
        // "Exitbutterfly" �ִϸ��̼� ����
        legacyAnimation.Play("Exitbutterfly");

        // �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(legacyAnimation["Exitbutterfly"].length);

        // ���� ������Ʈ ����
        Destroy(this.gameObject);
    }
}