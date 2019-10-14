using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIEmoteBubble : MonoBehaviour
{
    public static UIEmoteBubble instance { get; private set; }
    SpriteAtlas emoteSprites;
    public Image emoteBubble;
    public Image emoteMask;
    float originalSize;
    public float emoteSpeed = 5f;
    float currentY = 0f;
    public float holdTimeWindow = 1f;
    float holdTimer = 0f;
    EmoteState currentState = EmoteState.None;

    enum EmoteState
    {
        None,
        Up,
        Hold,
        Down
    }

    private void Awake()
    {
        emoteSprites = Resources.Load<SpriteAtlas>("Emotes");
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = emoteMask.rectTransform.rect.height;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case EmoteState.Up:
                currentY = Mathf.Clamp(currentY + originalSize * emoteSpeed * Time.deltaTime, 0f, originalSize);
                emoteMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentY);
                if (currentY == originalSize)
                {
                    holdTimer = 0f;
                    currentState = EmoteState.Hold;
                }
                break;
            case EmoteState.Hold:
                holdTimer += Time.deltaTime;
                if (holdTimer >= holdTimeWindow) { currentState = EmoteState.Down; }
                break;
            case EmoteState.Down:
                currentY = Mathf.Clamp(currentY - originalSize * emoteSpeed * Time.deltaTime, 0f, originalSize);
                emoteMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentY);
                if (currentY == 0f)
                {
                    currentState = EmoteState.None;
                    gameObject.SetActive(false);
                }
                break;
            case EmoteState.None:
                break;
        }
    }

    public void PlayEmote(string name)
    {
        emoteBubble.sprite = emoteSprites.GetSprite(name);
        currentY = 0f;
        emoteMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentY);
        gameObject.SetActive(true);
        currentState = EmoteState.Up;
    }
}
