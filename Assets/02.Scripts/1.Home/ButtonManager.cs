using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ��ư Ŭ�� �� ���� ��ȯ�ϴ� ����� �����ϴ� ��ũ��Ʈ
/// </summary>
public class ButtonManager : MonoBehaviour
{

    // �� ��ư�� �� �̸��� �����ϴ� Ŭ����
    [System.Serializable]
    public class ButtonSceneMapping
    {
        public Button button; // �� ��ȯ�� ���� ��ư
        public string sceneName; // ��ư Ŭ�� �� �̵��� ���� �̸�
    }

    // ��ư�� �� �̸��� �����ϴ� ���� ������ �迭
    public ButtonSceneMapping[] buttonSceneMappings; 


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
