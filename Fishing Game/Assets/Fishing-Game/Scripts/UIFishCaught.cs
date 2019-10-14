using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class UIFishCaught : MonoBehaviour
    {
        public static UIFishCaught instance { get; private set; }

        public Image fishSprite;
        public TMPro.TextMeshProUGUI fishName;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            GetComponentInChildren<Button>().onClick.AddListener(CloseCaughtFish);
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowCaughtFish(Fish fish)
        {
            fishName.text = fish.type;
            fishSprite.sprite = Resources.Load<Sprite>("Fish/" + fish.source);
            gameObject.SetActive(true);
        }

        public void CloseCaughtFish()
        {
            gameObject.SetActive(false);
        }
    }
}