using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Particle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem topParticle; // ��ġ �Ǵ� Ŭ�� �ÿ��� ����Ǵ� ��ƼŬ
    [SerializeField]
    private ParticleSystem[] otherParticles; // �׻� ����Ǵ� ��ƼŬ��

    [Range(100,500)]public float ParticlesCounts = 250; // topParticle�� count����
    private ParticleSystem.EmissionModule touchEmission;  // topParticle�� Emission ���

    private int touchCount = 0; // ��ġ �Ǵ� Ŭ�� �̺�Ʈ�� Ƚ��

    private void Start()
    {
        InitializeParticles();
    }

    private void Update()
    {
        HandleTouchOrClick();
    }

    private void InitializeParticles()
    {
        touchEmission = topParticle.emission; // topParticle�� Emission ����� ������

        // otherParticles�� ��� ��ƼŬ�� ���
        foreach (var particle in otherParticles)
        {
            particle.Play();
        }
    }

    private void HandleTouchOrClick()
    {
        // ��ġ �Ǵ� Ŭ�� �̺�Ʈ�� �߻��ϰ�, �̺�Ʈ�� Ƚ���� 3 �̸��� ���
        if (touchCount < 3 && IsTouchOrClickEvent())
        {
            touchCount++; // �̺�Ʈ�� Ƚ���� ����
            ActivateTopParticle(); // Emission Rate�� �̺�Ʈ�� Ƚ���� �ʱ�ȭ�ϴ� �ڷ�ƾ ����
        }
    }

    private bool IsTouchOrClickEvent()
    {
        return Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    }

    private void ActivateTopParticle()
    {
        touchEmission.rateOverTime = ParticlesCounts;
        StartCoroutine(ResetAfterSeconds(1f));
    }

    // ������ �ð� �Ŀ� Emission Rate�� �̺�Ʈ�� Ƚ���� �ʱ�ȭ�ϴ� �ڷ�ƾ
    private IEnumerator ResetAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        ResetEmissionRate();

        // �̺�Ʈ�� Ƚ���� 3�� ���
        if (touchCount == 3)
        {
            ResetTouchCount(); // �̺�Ʈ�� Ƚ���� 0���� �ʱ�ȭ
        }
    }

    private void ResetEmissionRate()
    {
        touchEmission.rateOverTime = 0f; // Emission Rate�� 0���� ����
    }

    private void ResetTouchCount()
    {
        touchCount = 0; // �̺�Ʈ�� Ƚ���� 0���� �ʱ�ȭ
    }
}