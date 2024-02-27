using System.Collections;
using UnityEngine;

public class LifecycleHandler : MonoBehaviour
{
    private Animator animator;
    private bool firstAnimationDone = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("FirstAnimation");
    }

    public void OnFirstAnimationDone()
    {
        firstAnimationDone = true;
    }

    private void Update()
    {
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Å¬¸¯");
            animator.Play("NextAnimation");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}