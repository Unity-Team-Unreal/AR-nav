using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    // �� ��ư�� �� �̸��� �����ϴ� Ŭ����
    [System.Serializable]
    public class ButtonSceneMapping
    {
        public Button button; // �� ��ȯ�� ���� ��ư
        public string sceneName; // ��ư Ŭ�� �� �̵��� ���� �̸�
    }

    public ButtonSceneMapping[] buttonSceneMappings; // ��ư�� �� �̸��� �����ϴ� ���� ������ �迭


    private void Start()
    {
        // �� ��ư�� �� ��ȯ �̺�Ʈ�� ����
        foreach (var mapping in buttonSceneMappings)
        {
            mapping.button.onClick.AddListener(() => SwitchScene(mapping.sceneName));
        }
    }


    // ������ ������ ��ȯ�ϴ� �޼���
    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
