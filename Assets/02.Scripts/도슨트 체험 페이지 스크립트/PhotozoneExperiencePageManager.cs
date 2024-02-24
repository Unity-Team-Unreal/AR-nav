using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class PhotozoneExperiencePageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;

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
                obj.transform.SetParent(trackedImage.transform);
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
