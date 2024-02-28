using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// ����Ʈ ü�� �������� ����Ʈ �Կ� ȭ�� ����
/// ����Ʈ �Կ� ȭ���� Image Detection�� �̿��Ͽ� 3D ������Ʈ�� ����
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class DocentExperiencePageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    List<GameObject> objs = new List<GameObject>();

    public static GameObject obj;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }

    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage trackedImage in arg.added)
        {
            string imageName = trackedImage.referenceImage.name;

            GameObject prefab = Resources.Load<GameObject>(imageName);

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.name = imageName;
                obj.transform.SetParent(trackedImage.transform);
                // AR ȯ�濡�� obj�� ���Ե��� �� Animator����� �����ͼ� DocentExperiencePageButton Ŭ������ �ִ� Animator�� ���� arDocnet��� �ʵ��� ����� �ϴ� �ڵ�
                DocentExperiencePageButton.arDocent = obj.GetComponent<Animator>();
                objs.Add(obj);
            }
        }

        foreach (ARTrackedImage trackedImage in arg.updated)
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                string imageName = trackedImage.referenceImage.name;

                print(objs.Find(x => x.name == imageName));

                obj = objs.Find(x => x.name == imageName);

                obj.SetActive(false);

            }
        }

        foreach (ARTrackedImage trackedImage in arg.removed)
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
