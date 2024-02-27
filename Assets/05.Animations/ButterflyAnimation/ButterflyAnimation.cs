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
        StartCoroutine(ClickNextAnimation());
    }

    private string ClickNextAnimation()
    {
        while (true)
        {
            if (firstAnimationDone && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Å¬¸¯");
                StartCoroutine(PlaySecondAnimationAndDestroy());
            }
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