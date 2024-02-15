using UnityEngine;
using UnityEngine.SceneManagement;

public class Backbutton : MonoBehaviour
{
    public string previousSceneName; // ���� ���� �̸�

    // ���� �ε��ϰ� ���� ���� �̸��� ������Ʈ�ϴ� �޼���
    public void LoadScene(string sceneName)
    {
        // ���� ���� �̸��� ���� ���� �̸����� ����
        previousSceneName = SceneManager.GetActiveScene().name;

        // ������ ������ ��ȯ
        SceneManager.LoadScene(sceneName);
    }

    // ���� ������ ���ư��� �޼���
    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // ���� ������ ��ȯ
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            // ���� ���� ���� ��쿡�� �⺻ ������ ����
            Debug.Log("No previous scene");
        }
    }
}