using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// 도슨트 체험 페이지의 도슨트 촬영 화면 구현
/// 도슨트 촬영 화면은 Image Detection을 이용하여 3D 오브젝트를 구현
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class DocentExperiencePageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    List<GameObject> objs = new List<GameObject>();

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

                GameObject obj = objs.Find(x => x.name == imageName);

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
