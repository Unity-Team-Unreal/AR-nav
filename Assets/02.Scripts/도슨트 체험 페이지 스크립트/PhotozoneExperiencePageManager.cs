using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PhotozoneExperiencePageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }

    // 이미지 상태가 변경될 때 호출되는 메서드
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        HandleAddedImages(arg.added);
        HandleUpdatedImages(arg.updated);
        HandleRemovedImages(arg.removed);
    }

    // 새 이미지가 감지될 때 호출되는 메서드
    void HandleAddedImages(List<ARTrackedImage> addedImages)
    {
        foreach (ARTrackedImage trackedImage in addedImages)
        {
            // 이미지 이름을 사용하여 3D 오브젝트를 로드하고 인스턴스화
            string imageName = trackedImage.referenceImage.name;
            GameObject prefab = Resources.Load<GameObject>(imageName);

            // 인스턴스화
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
            }
        }
    }

    // 이미지가 추적될 때 호출되는 메서드
    void HandleUpdatedImages(List<ARTrackedImage> updatedImages)
    {
        foreach (ARTrackedImage trackedImage in updatedImages)
        {
            // 이미지가 추적될 때 3D 오브젝트의 위치 및 회전을 업데이트
            if (trackedImage.transform.childCount > 0)
            {
                Transform child = trackedImage.transform.GetChild(0);
                child.position = trackedImage.transform.position;
                child.rotation = trackedImage.transform.rotation;
                child.gameObject.SetActive(true);
            }
        }
    }

    // 이미지가 제거될 때 호출되는 메서드
    void HandleRemovedImages(List<ARTrackedImage> removedImages)
    {
        foreach (ARTrackedImage trackedImage in removedImages)
        {
            // 이미지가 제거될 때 3D 오브젝트를 숨김
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    //비활성화시 호출되는 함수
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
