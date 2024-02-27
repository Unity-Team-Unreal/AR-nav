using System.Collections.Generic;
using UnityEngine;

// ������Ʈ Ǯ�� �����ϱ� ���� �������̽�
public interface IObjectPool
{
    // ������Ʈ Ǯ���� ������Ʈ�� �����ɴϴ�.
    GameObject Get(string prefabName);

    // ����� ���� ������Ʈ�� ������Ʈ Ǯ�� ��ȯ�մϴ�.
    void ReturnToPool(GameObject objectToReturn);
}

// IObjectPool �������̽��� ������ ObjectPool Ŭ����
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
        // "Resources/Prefabs" ��ο� �ִ� ��� �������� �ҷ��ɴϴ�.
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
            // ��Ͽ��� ���ϴ� �������� ã�� �ν��Ͻ��� �����մϴ�.
            var newObject = Instantiate(prefabs[prefabName]);
            newObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}