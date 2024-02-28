using System;
using System.Collections;
using UnityEngine;

public class CatHandler : MonoBehaviour
{
    private Animator animator;
    private Animator Stateanimator;
    private bool firstAnimationDone = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Stateanimator = animator.transform.Find("StateCat").GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
        Stateanimator.Play("Walk");
    }

    private void Update()
    {
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Å¬¸¯");
            animator.Play("NextAnimation");
            Stateanimator.SetTrigger("NextAnimation");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
        Stateanimator.SetTrigger("StateChange");
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}