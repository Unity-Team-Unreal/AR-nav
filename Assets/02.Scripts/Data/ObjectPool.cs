using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀을 관리하기 위한 인터페이스
public interface IObjectPool
{
    // 오브젝트 풀에서 오브젝트를 가져옵니다.
    GameObject Get();

    // 사용이 끝난 오브젝트를 오브젝트 풀에 반환합니다.
    void ReturnToPool(GameObject objectToReturn);
}

// IObjectPool 인터페이스를 구현한 ObjectPool 클래스
public class ObjectPool : MonoBehaviour, IObjectPool
{
    // 오브젝트 풀의 인스턴스를 가져옵니다. 이 인스턴스는 다른 스크립트에서 사용됩니다.
    public static IObjectPool Instance { get; private set; }
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> objects = new Queue<GameObject>();

    // 오브젝트 풀이 생성될 때 호출되는 메서드
    private void Awake()
    {
        // 오브젝트 풀의 인스턴스를 현재 오브젝트 풀로 설정합니다.
        Instance = this;
    }

    // 오브젝트 풀에서 오브젝트를 가져와 반환하는 메서드
    public GameObject Get()
    {
        // 오브젝트 풀에 오브젝트가 없는 경우, 새로운 오브젝트를 추가합니다.
        if (objects.Count == 0)
        {
            AddObjects(1);
        }

        // 오브젝트 풀에서 오브젝트를 가져와 반환합니다.
        return objects.Dequeue();
    }

    // 사용이 끝난 오브젝트를 오브젝트 풀에 반환하는 메서드
    public void ReturnToPool(GameObject objectToReturn)
    {
        // 사용이 끝난 오브젝트를 비활성화하고 오브젝트 풀에 반환합니다.
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    // 오브젝트 풀에 새로운 오브젝트를 추가하는 메서드
    private void AddObjects(int count)
    {
        // 지정된 개수만큼 오브젝트를 생성하여 오브젝트 풀에 추가합니다.
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab);
            newObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}