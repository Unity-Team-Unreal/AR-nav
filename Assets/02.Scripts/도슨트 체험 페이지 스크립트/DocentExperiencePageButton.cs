using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ����Ʈ ü�� �������� ��ư �� AR ���� �����̵� ����
/// </summary>
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
        // �ٷ� ���� �ִ� ������ �̵� => �ٷ� ���� �ִ� ���� �̸��� �����;� �Ѵ�.
        SceneManager.LoadScene(previousSceneName);
        PlayerPrefs.DeleteKey("PreviousScene");
    }

    void OnTimeLineReturn()
    {
        docentTimeLine.value = 0;
    }
}
