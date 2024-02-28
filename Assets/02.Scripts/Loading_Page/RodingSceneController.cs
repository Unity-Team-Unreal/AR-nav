using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 로딩씬 컨트롤러
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;  // 로딩바 UI   

    private void Start()
    {
        //비동기 방식 씬 로드
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // "1.Home Page" 씬을 비동기 방식으로 로드합니다.
        AsyncOperation operation = SceneManager.LoadSceneAsync("1.Home_Page");

        //씬 비활성화
        operation.allowSceneActivation = false;

        // 시간 측정 변수
        float timer = 0.0f;

        //씬 로딩 완료시까지 반복
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 로딩이 80% 이상일 경우
            if (progress >= 0.8f)
            {
                timer += Time.deltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.8f, 1f, timer / 3f); //3초 딜레이
                if (timer > 3f)
                {
                    //씬 활성화
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                //80% 미만일 경우 로딩 진행률을 그대로 표시
                loadingBar.fillAmount = progress;
            }
            yield return null;
        }
    }
}
