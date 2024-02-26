using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static DataManager Instance;

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
