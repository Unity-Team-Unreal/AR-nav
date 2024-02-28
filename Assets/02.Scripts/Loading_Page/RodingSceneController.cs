using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �ε��� ��Ʈ�ѷ�
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;  // �ε��� UI   

    private void Start()
    {
        //�񵿱� ��� �� �ε�
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // "1.Home Page" ���� �񵿱� ������� �ε��մϴ�.
        AsyncOperation operation = SceneManager.LoadSceneAsync("1.Home_Page");

        //�� ��Ȱ��ȭ
        operation.allowSceneActivation = false;

        // �ð� ���� ����
        float timer = 0.0f;

        //�� �ε� �Ϸ�ñ��� �ݺ�
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // �ε��� 80% �̻��� ���
            if (progress >= 0.8f)
            {
                timer += Time.deltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.8f, 1f, timer / 3f); //3�� ������
                if (timer > 3f)
                {
                    //�� Ȱ��ȭ
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                //80% �̸��� ��� �ε� ������� �״�� ǥ��
                loadingBar.fillAmount = progress;
            }
            yield return null;
        }
    }
}
