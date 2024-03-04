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

    public static GameObject obj;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent; // 화면에서 ImageTracked가 계속 활성화되게 한다.
    }

    /// <summary>
    /// 이미지를 디텍딩하고 있을 때 AR 오브젝트가 나오고, 하지 않을 때 AR 오브젝트가 사라지게 함
    /// </summary>
    /// <param name="arg"></param>
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage trackedImage in arg.added) // ARTrackedImage에서 추가를 할 때
        {
            string imageName = trackedImage.referenceImage.name;
            // Resources 폴더 내에 있는 referemceImage의 name과 같은 이름의 Prefab을 가져와서 GameObject prefab이 되게 한다.
            GameObject prefab = Resources.Load<GameObject>(imageName);

            if (prefab != null)
            {
                // AR환경에 prefab을 삽입하고 삽입된 prefab은 obj가 된다.
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.name = imageName;
                obj.transform.SetParent(trackedImage.transform);
                // AR 환경에서 obj가 삽입됐을 때 Animator기능을 가져와서 DocentExperiencePageButton 클래스에 있는 Animator를 가진 arDocnet라는 필드의 기능을 하는 코드
                DocentExperiencePageButton.arDocent = obj.GetComponent<Animator>();
                objs.Add(obj); // objs 리스트에 추가
            }
        }

        foreach (ARTrackedImage trackedImage in arg.updated) // ARTrackedImage에서 업데이트 할 때
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited) // AR 오브젝트가 나올 이미지를 찍고 있지 않을 때
            {
                string imageName = trackedImage.referenceImage.name;

                obj = objs.Find(x => x.name == imageName);

                obj.SetActive(false); // AR 환경에 나와있는 오브젝트를 비활성화

            }
        }

        foreach (ARTrackedImage trackedImage in arg.removed) // ARTrakedImage에서 사라질 때
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent; // 화면에서 ImageTracked가 점차 비활성화
    }
}
