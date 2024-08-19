using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PoolBase<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAtion;
        private readonly Action<T> _returnAction;
        private Queue<T> _pool = new Queue<T>();
        private List<T> _active = new List<T>();
        protected static GameObject _parentPool;
        public PoolBase(Func<T> preloadFunc,Action<T> getAction,Action<T> returnAction,int preloadCount)
        {
            _preloadFunc = preloadFunc;
            _getAtion = getAction;
            _returnAction = returnAction;
            if (preloadFunc == null)
            {
                Debug.LogError("PreloadFunc is null");
                return;
            }
            _parentPool = Object.Instantiate(new GameObject());
            for (int i = 0; i < preloadCount; i++)
            {
                Return(preloadFunc());
            }
            
        }
        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAtion?.Invoke(item);
            _active.Add(item);
            return item;
        }

        public void Return(T item)
        {
            _returnAction?.Invoke(item);
            _pool.Enqueue(item);
            _active.Remove(item);
        }

        public void ReturnAll()
        {
            foreach (var item in _active.ToArray())
            {
                Return(item);
            }
        }
    }
}

