using System.Collections.Generic;

public class ObjectPool<T> where T : struct
{
    private readonly int _poolSize;
    private readonly Queue<T> _availableObjects = new Queue<T>();
    public int PoolSize { get { return _poolSize; } }
    public int AvailableObjectsCount { get { return _availableObjects.Count; } }
    public ObjectPool(int poolSize = 10)
    {
        _poolSize = poolSize;
    }
    public T GetFromPool()
    {
        if (_availableObjects.Count > 0)
        {
            return _availableObjects.Dequeue();
        }
        else
        {
            return new T();
        }
    }
    public void AddToPool(T obj)
    {
        if (_availableObjects.Count < _poolSize)
        {
            _availableObjects.Enqueue(obj);
        }
        else
        {
            obj = default(T);
        }
    }

    public T NewElement()
    {
        return new T();
    }
}
