using UnityEngine;
using UnityEngine.UI;

namespace DrawingRecognition.Views
{
    public class RecognitionHistoryView : MonoBehaviour
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private Image iconPrefab;

        public void AddSymbolIcon(Sprite iconSprite)
        {
            var icon = Instantiate(iconPrefab, contentContainer);
            icon.sprite = iconSprite;
        }
    }
}