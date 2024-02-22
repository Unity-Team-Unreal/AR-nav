using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    // 각 버튼과 씬 이름을 연결하는 클래스
    [System.Serializable]
    public class ButtonSceneMapping
    {
        public Button button; // 씬 전환을 위한 버튼
        public string sceneName; // 버튼 클릭 시 이동할 씬의 이름
    }

    public ButtonSceneMapping[] buttonSceneMappings; // 버튼과 씬 이름을 연결하는 매핑 정보의 배열


    private void Start()
    {
        // 각 버튼에 씬 전환 이벤트를 연결
        foreach (var mapping in buttonSceneMappings)
        {
            mapping.button.onClick.AddListener(() => SwitchScene(mapping.sceneName));
        }
    }


    // 지정한 씬으로 전환하는 메서드
    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
