using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀을 관리하기 위한 인터페이스
public interface IObjectPool
{
    // 오브젝트 풀에서 오브젝트를 가져옵니다.
    GameObject Get(string prefabName);

    // 사용이 끝난 오브젝트를 오브젝트 풀에 반환합니다.
    void ReturnToPool(GameObject objectToReturn);
}

// IObjectPool 인터페이스를 구현한 ObjectPool 클래스
public class ObjectPool : MonoBehaviour, IObjectPool
{
    public static IObjectPool Instance { get; private set; }
    private Queue<GameObject> objects = new Queue<GameObject>();
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance = this;
        LoadPrefabsFromResources();
    }

    private void LoadPrefabsFromResources()
    {
        // "Resources/Prefabs" 경로에 있는 모든 프리팹을 불러옵니다.
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        foreach (var prefab in loadedPrefabs)
        {
            prefabs.Add(prefab.name, prefab);
        }
    }

    public GameObject Get(string prefabName)
    {
        if (objects.Count == 0)
        {
            AddObjects(prefabName, 1);
        }

        return objects.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects(string prefabName, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // 목록에서 원하는 프리팹을 찾아 인스턴스를 생성합니다.
            var newObject = Instantiate(prefabs[prefabName]);
            newObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}