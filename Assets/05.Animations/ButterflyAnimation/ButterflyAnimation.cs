using System.Collections;
using UnityEngine;

public class ButterflyAnimation : MonoBehaviour
{
    private Animation legacyAnimation;
    private bool firstAnimationDone = false;

    private void Awake()
    {
        legacyAnimation = GetComponent<Animation>();
    }

    private void Start()
    {
        legacyAnimation.Play("Movebutterfly");
        StartCoroutine(CheckAnimationStatus());
    }

    private void Update()
    {
        if (firstAnimationDone && Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Å¬¸¯");
            StartCoroutine(PlaySecondAnimationAndDestroy());
        }
    }


    private IEnumerator CheckAnimationStatus()
    {
        while (legacyAnimation.isPlaying)
        {
            yield return null;
        }

        firstAnimationDone = true;
    }

    private IEnumerator PlaySecondAnimationAndDestroy()
    {
        legacyAnimation.Play("Exitbutterfly");
        yield return new WaitForSeconds(legacyAnimation["Exitbutterfly"].length);
        Destroy(this.gameObject);
    }
}