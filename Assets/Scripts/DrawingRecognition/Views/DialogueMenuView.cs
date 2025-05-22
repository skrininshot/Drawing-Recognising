using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawingRecognition.Views
{
    public class DialogueMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text placeholder;
        [SerializeField] private Button acceptButton;

        private string _defaultPlaceholderText = "";

        private Action<string> _onComplete;

        private void Start()
        {
            _defaultPlaceholderText = placeholder.text;
        }

        private void OnEnable()
        {
            acceptButton.onClick.AddListener(OnAcceptButton);
            inputField.text = string.Empty;
            inputField.ActivateInputField();
            acceptButton.Select();
        }

        public void SetDialogueMenu(string text, Action<string> onComplete, string placeholderText = "")
        {
            headerText.text = text;

            placeholder.text = (placeholderText == string.Empty) ? _defaultPlaceholderText : placeholderText;

            _onComplete = onComplete;
        }

        private void OnAcceptButton()
        {
            _onComplete.Invoke(inputField.text);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            acceptButton.onClick.RemoveListener(OnAcceptButton);
        }
    }
}