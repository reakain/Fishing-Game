using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class UIFishBar : MonoBehaviour
    {

        public static UIFishBar instance { get; private set; }

        public Image mask;
        float originalSize;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            originalSize = mask.rectTransform.rect.height;
            gameObject.SetActive(false);
        }

        public void SetValue(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * value);
        }

    }
}