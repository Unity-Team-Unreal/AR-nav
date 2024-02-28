using UnityEngine;

public class ContentsDataManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ContentsDataManager Instance;

    // 전달할 데이터
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
