using UnityEngine;

public class ContentsDataManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static ContentsDataManager Instance;

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
