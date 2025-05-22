using System.Collections.Generic;
using DrawingRecognition.Models;
using DrawingRecognition.Controllers;
using UnityEngine;

namespace DrawingRecognition.Views
{
    public class LibraryView : MonoBehaviour
    {
        [SerializeField] private DrawingRecognitionController drawingRecognition;

        [SerializeField] private Transform contentContainer;
        [SerializeField] private LibraryElementView elementPrefab;
        [SerializeField] private int poolSize;

        private readonly List<LibraryElementView> _elements = new();
        private ElementsPool<LibraryElementView> _elementsPool;

        private void Start()
        {
            _elementsPool = new ElementsPool<LibraryElementView>(elementPrefab, poolSize, contentContainer);

            UpdateElements();
        }

        public void UpdateElements()
        {
            foreach (var element in _elements) _elementsPool.Despawn(element);

            _elements.Clear();

            var currentLibrarySymbols = drawingRecognition.GetCurrentLibrary().characterList;

            foreach (var symbol in currentLibrarySymbols)
            {
                if (symbol.name == "Empty") continue;

                var element = _elementsPool.Spawn();
                element.SetElement(symbol.name, OnClickElement);
                _elements.Add(element);
            }
        }

        private void OnClickElement(LibraryElementView element)
        {
            var symbol = drawingRecognition.GetCurrentLibrary().characterList
                .Find((t) => t.name == element.Text);

            drawingRecognition.DisplayCharacter(symbol);
        }
    }
}