using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// 도슨트 체험 페이지의 버튼 구현과 도슨트 촬영 화면 구현
/// 도슨트 촬영 화면은 Image Detection을 이용하여 3D 오브젝트를 구현
/// </summary>
public class DocentExperiencePageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    [SerializeField] Button backButton;
    [SerializeField] Button returnButton;

    [SerializeField] Slider docentTimeLine;

    [SerializeField] TextMeshProUGUI testText;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }

    void Start()
    {
        PlayerPrefs.GetString("PreviousScene");

        backButton.onClick.AddListener(OnBackPage);
        returnButton.onClick.AddListener(OnTimeLineReturn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackPage();
        }
    }

    void OnBackPage()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
        // 바로 전에 있던 씬으로 이동 => 바로 전에 있던 씬의 이름을 가져와야 한다.
        SceneManager.LoadScene("");
    }

    void OnTimeLineReturn()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
        docentTimeLine.value = 0;
    }

    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        testText.text = "이미지가 트랙되었습니다.";

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
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }
}
