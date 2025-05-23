using System;
using DrawingRecognition.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawingRecognition.Controllers
{
    public class ControlsHandler : MonoBehaviour
    {
        [Header("Recognition Settings")]
        [SerializeField] private DrawingRecognitionController drawingRecognition;
        [SerializeField] private DialogueMenuSystem dialogueMenuSystem;
        [SerializeField] private double recognitionTreshold;

        [Header("Buttons")]
        [SerializeField] private Button clearDrawingButton;
        [SerializeField] private Button  recognizeDrawingButton;
        [SerializeField] private Button saveSymbolButton;
        [SerializeField] private Button  selectLibraryButton;
        [SerializeField] private Button  clearCurrentLibraryButton;

        [Header("Info Output")] 
        [SerializeField] private TMP_Text recognizedSymbolText;
        [SerializeField] private TMP_Text selectedLibraryText;
        [SerializeField] private RecognitionHistoryView recognitionHistoryView;
        [SerializeField] private LibraryView currentLibrarySymbolsView;
        
        private string _recognizedSymbolTextDefault;
        private string _selectedLibraryTextDefault;

        private void Start()
        {
            _recognizedSymbolTextDefault = recognizedSymbolText.text;
            _selectedLibraryTextDefault = selectedLibraryText.text;
            
            UpdateSelectedLibraryText();
        }

        private void OnEnable()
        {
            clearDrawingButton.onClick.AddListener(ClearDrawing);
            recognizeDrawingButton.onClick.AddListener(RecognizeDrawing);
            saveSymbolButton.onClick.AddListener(SaveSymbol);
            selectLibraryButton.onClick.AddListener(SelectLibrary);
            clearCurrentLibraryButton.onClick.AddListener(ClearCurrentLibrary);
        }

        private void OnDisable()
        {
            clearDrawingButton.onClick.RemoveListener(ClearDrawing);
            recognizeDrawingButton.onClick.RemoveListener(RecognizeDrawing);
            saveSymbolButton.onClick.RemoveListener(SaveSymbol);
            selectLibraryButton.onClick.RemoveListener(SelectLibrary);
            clearCurrentLibraryButton.onClick.RemoveListener(ClearCurrentLibrary);
        }

        private void Update()
        {
            CheckControls();
        }
        
        private void CheckControls() 
        {
            if (Input.GetKeyDown(KeyCode.C)) ClearDrawing();
            if (Input.GetKeyDown(KeyCode.F)) RecognizeDrawing();
            if (Input.GetKeyDown(KeyCode.S)) SaveSymbol();
            if (Input.GetKeyDown(KeyCode.Q)) SelectLibrary();
            if (Input.GetKeyDown(KeyCode.O)) ClearCurrentLibrary();
        }

        private void ClearDrawing()
        {
            drawingRecognition.ClearDrawing();
        }
        
        private void RecognizeDrawing()
        {
            var matchList = drawingRecognition.GetMatchList();
            var recognizedSymbolName = "Unknown";
            var percentFormatText = "";
            var tresholdPercent = recognitionTreshold * 100;

            if (matchList[0].Value >= tresholdPercent && matchList[0].Key.name != "Empty")
            {
                recognizedSymbolName = matchList[0].Key.name;
                percentFormatText = matchList[0].Value.ToString("F1") + "%";
            }
            
            recognizedSymbolText.text = _recognizedSymbolTextDefault
                .Replace("{name}", recognizedSymbolName)
                .Replace("{percent}", percentFormatText);
            
            var sprite = Resources.Load<Sprite>($"SymbolIcons/{recognizedSymbolName}");
            
            if (sprite != null)
                recognitionHistoryView.AddSymbolIcon(sprite);
        }
            
        private void SaveSymbol()
        {
            dialogueMenuSystem.ShowDialogueMenu("Укажите название символа", (name) =>
            {
                drawingRecognition.AddDrawingToLib(name);
                            
                currentLibrarySymbolsView.UpdateElements();
                
                Debug.Log($"Добавлен символ: {name}");
            });
        }
                
        private void SelectLibrary()
        {
            dialogueMenuSystem.ShowDialogueMenu("Укажите индекс библиотеки", (name) =>
            {
                var index = Convert.ToInt32(name);
                drawingRecognition.SetLibrary(index);
                var currentLibraryName = drawingRecognition.GetCurrentLibrary().name;

                UpdateSelectedLibraryText();
                
                currentLibrarySymbolsView.UpdateElements();
                
                Debug.Log($"Выбрана библиотека символов: {name} ({currentLibraryName})");
            },
            "Впишите число");
        }

        private void UpdateSelectedLibraryText()
        {
            var currentLibrary = drawingRecognition.GetCurrentLibrary();
            var index = drawingRecognition.libraryList.IndexOf(currentLibrary);
            
            selectedLibraryText.text = _selectedLibraryTextDefault
                .Replace("{n}", $"{index}")
                .Replace("{lib_name}", $"{currentLibrary.name}");
        }
                
        private void ClearCurrentLibrary()
        {
            var currentLibrary = drawingRecognition.GetCurrentLibrary();
            var libraryIndex = drawingRecognition.GetLibraryList().IndexOf(currentLibrary);
            currentLibrary.ClearLibrary();
            
            currentLibrarySymbolsView.UpdateElements();
                    
            Debug.Log($"Библиотека очищена: {libraryIndex}({currentLibrary.name})");
        }
    }
}