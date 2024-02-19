using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DocentExperiencePageButton : MonoBehaviour
{
    static string previousSceneName;

    [SerializeField] Button backButton;
    [SerializeField] Button returnButton;

    [SerializeField] Slider docentTimeLine;

    void Start()
    {
        previousSceneName = PlayerPrefs.GetString("PreviousScene");

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
        // 바로 전에 있던 씬으로 이동 => 바로 전에 있던 씬의 이름을 가져와야 한다.
        SceneManager.LoadScene(previousSceneName);
        PlayerPrefs.DeleteKey("PreviousScene");
    }

    void OnTimeLineReturn()
    {
        docentTimeLine.value = 0;
    }
}
