using UnityEngine;

public class DataManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static DataManager Instance;

    // ������ ������
    public ContentsData contentsData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
