using System.Collections.Generic;
using UnityEngine;

// ������Ʈ Ǯ�� �����ϱ� ���� �������̽�
public interface IObjectPool
{
    // ������Ʈ Ǯ���� ������Ʈ�� �����ɴϴ�.
    GameObject Get();

    // ����� ���� ������Ʈ�� ������Ʈ Ǯ�� ��ȯ�մϴ�.
    void ReturnToPool(GameObject objectToReturn);
}

// IObjectPool �������̽��� ������ ObjectPool Ŭ����
public class ObjectPool : MonoBehaviour, IObjectPool
{
    // ������Ʈ Ǯ�� �ν��Ͻ��� �����ɴϴ�. �� �ν��Ͻ��� �ٸ� ��ũ��Ʈ���� ���˴ϴ�.
    public static IObjectPool Instance { get; private set; }
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> objects = new Queue<GameObject>();

    // ������Ʈ Ǯ�� ������ �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // ������Ʈ Ǯ�� �ν��Ͻ��� ���� ������Ʈ Ǯ�� �����մϴ�.
        Instance = this;
    }

    // ������Ʈ Ǯ���� ������Ʈ�� ������ ��ȯ�ϴ� �޼���
    public GameObject Get()
    {
        // ������Ʈ Ǯ�� ������Ʈ�� ���� ���, ���ο� ������Ʈ�� �߰��մϴ�.
        if (objects.Count == 0)
        {
            AddObjects(1);
        }

        // ������Ʈ Ǯ���� ������Ʈ�� ������ ��ȯ�մϴ�.
        return objects.Dequeue();
    }

    // ����� ���� ������Ʈ�� ������Ʈ Ǯ�� ��ȯ�ϴ� �޼���
    public void ReturnToPool(GameObject objectToReturn)
    {
        // ����� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    // ������Ʈ Ǯ�� ���ο� ������Ʈ�� �߰��ϴ� �޼���
    private void AddObjects(int count)
    {
        // ������ ������ŭ ������Ʈ�� �����Ͽ� ������Ʈ Ǯ�� �߰��մϴ�.
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab);
            newObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}