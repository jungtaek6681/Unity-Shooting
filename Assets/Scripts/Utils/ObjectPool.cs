using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] int size;
    [SerializeField] int capacity;

    private Stack<PooledObject> objectPool;

    private void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        objectPool = new Stack<PooledObject>(capacity);
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            instance.transform.parent = transform;
            objectPool.Push(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count > 0)
        {
            PooledObject instance = objectPool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(prefab);
            instance.Pool = this;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        if (objectPool.Count < capacity)
        {
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            objectPool.Push(instance);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
}
