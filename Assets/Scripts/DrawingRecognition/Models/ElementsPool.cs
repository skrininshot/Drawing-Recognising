using System.Collections.Generic;
using UnityEngine;

namespace DrawingRecognition.Models
{
    public class ElementsPool<T> where T : MonoBehaviour
    {
        private readonly T _elementPrefab;
        private readonly Transform _contentContainer;
        private readonly List<T> _pool;

        public ElementsPool(T elementPrefab, int size, Transform contentContainer)
        {
            _elementPrefab = elementPrefab;
            _contentContainer = contentContainer;

            _pool = new List<T>();

            for (int i = 0; i < size; i++) CreateElement();
        }

        private void CreateElement()
        {
            var element = Object.Instantiate(_elementPrefab, _contentContainer);
            element.gameObject.SetActive(false);

            _pool.Add(element);
        }

        public T Spawn()
        {
            if (_pool.Count == 0) CreateElement();

            var element = _pool[^1];
            _pool.RemoveAt(_pool.Count - 1);
            element.gameObject.SetActive(true);

            return element;
        }

        public void Despawn(T element)
        {
            _pool.Add(element);
            element.gameObject.SetActive(false);
        }
    }
}