using System;
using UnityEngine;

namespace MagicPigGames {
    [Serializable]
    public class VerticalProgressBar : ProgressBar {       
        protected override float SizeMin => rectTransform.sizeDelta.y * sizeMin; 
        protected override float SizeMax => rectTransform.sizeDelta.y * sizeMax; 
        protected override float CurrentOverlaySize => overlayBar.sizeDelta.y; 
        protected override void SetBarValue(float value) 
        {
            var sizeDelta = overlayBar.sizeDelta;
            sizeDelta = new Vector2(sizeDelta.x, value);
            overlayBar.sizeDelta = sizeDelta;
        }
        
        
        protected override void CheckOverlayBarRectTransform()
        {
            if (overlayBar == null) return;
            if (overlayBar.anchorMin == new Vector2(0, 1)
                && overlayBar.anchorMax == new Vector2(1, 1)) return;

            overlayBar.anchorMin = new Vector2(0, 1); 
            overlayBar.anchorMax = new Vector2(1, 1); 
        }
    }
}