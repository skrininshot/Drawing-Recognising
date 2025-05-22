using System;
using UnityEngine;
using DrawingRecognition.Views;

namespace DrawingRecognition.Controllers
{
    public class DialogueMenuSystem : MonoBehaviour
    {
        [SerializeField] private DialogueMenuView dialogueMenuView;
        [SerializeField] private ControlsHandler controlsHandler;
    
        private Action<string> _onComplete;
    
        private void Start()
        {
            dialogueMenuView.gameObject.SetActive(false);
        }

        public void ShowDialogueMenu(string text, Action<string> onComplete, string placeholderText = "")
        {
            _onComplete = onComplete;
            controlsHandler.enabled = false;
        
            dialogueMenuView.gameObject.SetActive(true);
            dialogueMenuView.SetDialogueMenu(text, OnDialogueMenuComplete, placeholderText);
        }

        private void OnDialogueMenuComplete(string text)
        {
            _onComplete.Invoke(text);
            controlsHandler.enabled = true;
        }
    }
}