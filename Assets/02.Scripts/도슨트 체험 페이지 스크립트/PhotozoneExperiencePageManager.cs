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

    // �̹��� ���°� ����� �� ȣ��Ǵ� �޼���
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        HandleAddedImages(arg.added);
        HandleUpdatedImages(arg.updated);
        HandleRemovedImages(arg.removed);
    }

    // �� �̹����� ������ �� ȣ��Ǵ� �޼���
    void HandleAddedImages(List<ARTrackedImage> addedImages)
    {
        foreach (ARTrackedImage trackedImage in addedImages)
        {
            // �̹��� �̸��� ����Ͽ� 3D ������Ʈ�� �ε��ϰ� �ν��Ͻ�ȭ
            string imageName = trackedImage.referenceImage.name;
            GameObject prefab = Resources.Load<GameObject>(imageName);

            // �ν��Ͻ�ȭ
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
            }
        }
    }

    // �̹����� ������ �� ȣ��Ǵ� �޼���
    void HandleUpdatedImages(List<ARTrackedImage> updatedImages)
    {
        foreach (ARTrackedImage trackedImage in updatedImages)
        {
            // �̹����� ������ �� 3D ������Ʈ�� ��ġ �� ȸ���� ������Ʈ
            if (trackedImage.transform.childCount > 0)
            {
                Transform child = trackedImage.transform.GetChild(0);
                child.position = trackedImage.transform.position;
                child.rotation = trackedImage.transform.rotation;
                child.gameObject.SetActive(true);
            }
        }
    }

    // �̹����� ���ŵ� �� ȣ��Ǵ� �޼���
    void HandleRemovedImages(List<ARTrackedImage> removedImages)
    {
        foreach (ARTrackedImage trackedImage in removedImages)
        {
            // �̹����� ���ŵ� �� 3D ������Ʈ�� ����
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    //��Ȱ��ȭ�� ȣ��Ǵ� �Լ�
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
