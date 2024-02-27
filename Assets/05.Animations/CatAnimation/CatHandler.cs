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
        StartCoroutine(ClickNextAnimation());
    }

    private IEnumerator ClickNextAnimation()
    {
        while (true)
        {
            if (firstAnimationDone && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Å¬¸¯");
                animator.Play("NextAnimation");
                Stateanimator.SetTrigger("NextAnimation");
                StartCoroutine(DestroyAfterAnimation());
            }
            yield return new WaitForSeconds(1);
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