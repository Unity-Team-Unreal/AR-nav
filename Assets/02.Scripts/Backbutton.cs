using UnityEngine;
using UnityEngine.SceneManagement;

public class Backbutton : MonoBehaviour
{
    public string previousSceneName; // 이전 씬의 이름

    // 씬을 로드하고 이전 씬의 이름을 업데이트하는 메서드
    public void LoadScene(string sceneName)
    {
        // 현재 씬의 이름을 이전 씬의 이름으로 설정
        previousSceneName = SceneManager.GetActiveScene().name;

        // 지정한 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }

    // 이전 씬으로 돌아가는 메서드
    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // 이전 씬으로 전환
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            // 이전 씬이 없는 경우에는 기본 동작을 수행
            Debug.Log("No previous scene");
        }
    }
}