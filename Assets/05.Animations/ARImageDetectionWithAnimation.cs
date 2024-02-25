using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageDetectionWithAnimation : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    private GameObject spawnedCat;
    private Animator catAnimator;
    private float lastInputTime;

    private void Awake()
    {
        trackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void Update()
    {
        if (spawnedCat != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                catAnimator.SetBool("Trigger", true);
                lastInputTime = Time.time;
            }
            else if (Time.time - lastInputTime > 10)
            {
                catAnimator.SetTrigger("Exit");
                Destroy(spawnedCat, catAnimator.GetCurrentAnimatorStateInfo(0).length + 1f);
                spawnedCat = null;
            }
        }
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            // �̹����� �νĵǸ� ����� ������Ʈ�� �ִϸ��̼��� �۵�
            spawnedCat = trackedImage.transform.Find("������5").gameObject;
            if (spawnedCat != null)
            {
                catAnimator = spawnedCat.GetComponent<Animator>();
                catAnimator.SetTrigger("Enter");
                lastInputTime = Time.time;
            }
        }
    }
}