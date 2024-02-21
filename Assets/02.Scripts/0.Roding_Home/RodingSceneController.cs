using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;  // �ε��� �̹���

    private void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // ���� ������ �񵿱� �ε�
        AsyncOperation operation = SceneManager.LoadSceneAsync("1.Home");
        operation.allowSceneActivation = false;

        float timer = 0.0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // �ε��� 80% ����Ǿ��� ��, 5�ʰ� õõ�� ����
            if (progress >= 0.8f)
            {
                timer += Time.deltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.8f, 1f, timer / 3f);
                if (timer > 3f)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                loadingBar.fillAmount = progress;
            }

            yield return null;
        }
    }
}
