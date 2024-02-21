using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Particle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem topParticle; // 터치 또는 클릭 시에만 재생되는 파티클
    [SerializeField]
    private ParticleSystem[] otherParticles; // 항상 재생되는 파티클들

    [Range(100,500)]public float ParticlesCounts = 250; // topParticle의 count개수
    private ParticleSystem.EmissionModule touchEmission;  // topParticle의 Emission 모듈

    private int touchCount = 0; // 터치 또는 클릭 이벤트의 횟수

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
        touchEmission = topParticle.emission; // topParticle의 Emission 모듈을 가져옴

        // otherParticles의 모든 파티클을 재생
        foreach (var particle in otherParticles)
        {
            particle.Play();
        }
    }

    private void HandleTouchOrClick()
    {
        // 터치 또는 클릭 이벤트가 발생하고, 이벤트의 횟수가 3 미만일 경우
        if (touchCount < 3 && IsTouchOrClickEvent())
        {
            touchCount++; // 이벤트의 횟수를 증가
            ActivateTopParticle(); // Emission Rate와 이벤트의 횟수를 초기화하는 코루틴 시작
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

    // 지정된 시간 후에 Emission Rate와 이벤트의 횟수를 초기화하는 코루틴
    private IEnumerator ResetAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        ResetEmissionRate();

        // 이벤트의 횟수가 3일 경우
        if (touchCount == 3)
        {
            ResetTouchCount(); // 이벤트의 횟수를 0으로 초기화
        }
    }

    private void ResetEmissionRate()
    {
        touchEmission.rateOverTime = 0f; // Emission Rate를 0으로 설정
    }

    private void ResetTouchCount()
    {
        touchCount = 0; // 이벤트의 횟수를 0으로 초기화
    }
}