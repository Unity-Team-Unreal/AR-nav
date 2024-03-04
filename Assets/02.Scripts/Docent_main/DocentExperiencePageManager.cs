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
        imageManager.trackedImagesChanged += OnImageTrackedEvent; // ȭ�鿡�� ImageTracked�� ��� Ȱ��ȭ�ǰ� �Ѵ�.
    }

    /// <summary>
    /// �̹����� ���ص��ϰ� ���� �� AR ������Ʈ�� ������, ���� ���� �� AR ������Ʈ�� ������� ��
    /// </summary>
    /// <param name="arg"></param>
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage trackedImage in arg.added) // ARTrackedImage���� �߰��� �� ��
        {
            string imageName = trackedImage.referenceImage.name;
            // Resources ���� ���� �ִ� referemceImage�� name�� ���� �̸��� Prefab�� �����ͼ� GameObject prefab�� �ǰ� �Ѵ�.
            GameObject prefab = Resources.Load<GameObject>(imageName);

            if (prefab != null)
            {
                // ARȯ�濡 prefab�� �����ϰ� ���Ե� prefab�� obj�� �ȴ�.
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.name = imageName;
                obj.transform.SetParent(trackedImage.transform);
                // AR ȯ�濡�� obj�� ���Ե��� �� Animator����� �����ͼ� DocentExperiencePageButton Ŭ������ �ִ� Animator�� ���� arDocnet��� �ʵ��� ����� �ϴ� �ڵ�
                DocentExperiencePageButton.arDocent = obj.GetComponent<Animator>();
                objs.Add(obj); // objs ����Ʈ�� �߰�
            }
        }

        foreach (ARTrackedImage trackedImage in arg.updated) // ARTrackedImage���� ������Ʈ �� ��
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited) // AR ������Ʈ�� ���� �̹����� ��� ���� ���� ��
            {
                string imageName = trackedImage.referenceImage.name;

                obj = objs.Find(x => x.name == imageName);

                obj.SetActive(false); // AR ȯ�濡 �����ִ� ������Ʈ�� ��Ȱ��ȭ

            }
        }

        foreach (ARTrackedImage trackedImage in arg.removed) // ARTrakedImage���� ����� ��
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent; // ȭ�鿡�� ImageTracked�� ���� ��Ȱ��ȭ
    }
}
