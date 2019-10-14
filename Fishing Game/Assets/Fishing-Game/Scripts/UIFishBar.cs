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

        public Image cursor;
        Vector2 offMax;
        Vector2 offMin;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            originalSize = mask.rectTransform.rect.height;
            //gameObject.SetActive(false);

            offMax = new Vector2(cursor.rectTransform.offsetMax.x, cursor.rectTransform.offsetMax.y);
            offMin = new Vector2(cursor.rectTransform.offsetMin.x, cursor.rectTransform.offsetMin.y);

        }

        public void SetCatchWindow(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * value);
        }

        public void MoveCuror(float value)
        {
            cursor.rectTransform.offsetMax = new Vector2(offMax.x, offMax.y - offMax.y * (value));
            cursor.rectTransform.offsetMin = new Vector2(offMin.x, offMin.y + offMin.y * (value));
        }

    }
}