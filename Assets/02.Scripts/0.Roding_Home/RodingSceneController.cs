using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;  // 로딩바 이미지

    private void Start()
    {
        // 코루틴 시작
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // 다음 씬으로 비동기 로드
        AsyncOperation operation = SceneManager.LoadSceneAsync("1.Home");
        operation.allowSceneActivation = false;

        float timer = 0.0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 로딩이 80% 진행되었을 때, 5초간 천천히 진행
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
