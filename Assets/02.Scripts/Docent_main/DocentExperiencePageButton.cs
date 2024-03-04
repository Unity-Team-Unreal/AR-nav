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

    public static Animator arDocent;

    //[SerializeField] Button backButton;
    [SerializeField] Button returnButton;

    [SerializeField] Slider docentTimeLine;

    void Start()
    {
        previousSceneName = PlayerPrefs.GetString("PreviousScene");


        returnButton.onClick.AddListener(OnTimeLineReturn);
    }

    void Update()
    {

        if (DocentExperiencePageManager.obj != null)
        {
            // ���� �ִϸ��̼� ���� ���� ��������
            AnimatorStateInfo stateInfo = arDocent.GetCurrentAnimatorStateInfo(0);

            // �ִϸ��̼��� ����ȭ�� �ð��� ����Ͽ� �����̴� �� ����
            float normalizedTime = stateInfo.normalizedTime;

            docentTimeLine.value = normalizedTime;
        }
    }

    /// <summary>
    /// �ǵ����� ��ư�� ������ �� �Ͼ�� �̺�Ʈ
    /// </summary>
    void OnTimeLineReturn()
    {
        // �ִϸ��̼ǰ� �����̵� �� �ʱ�ȭ
        arDocent.Play("metarig|Walk", -1, 0f);
        docentTimeLine.value = 0;
    }
}
