using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawingRecognition.Views
{
    public class LibraryElementView : MonoBehaviour
    {
        public string Text => nameText.text;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Button button;

        private Action<LibraryElementView> _onClick;

        public void SetElement(string text, Action<LibraryElementView> onClick)
        {
            nameText.text = text;
            _onClick = onClick;
            button.onClick.AddListener(() => _onClick?.Invoke(this));
        }

        private void OnDestroy()
        {
            if (_onClick != null) button.onClick.RemoveListener(() => _onClick?.Invoke(this));
        }
    }
}