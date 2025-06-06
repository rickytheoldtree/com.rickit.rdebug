using UnityEngine;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RLabel : MonoBehaviour, IHaveTag
    {
        private Text textComponent;
        public string Tag { get; set; }
        public string Text
        {
            get => textComponent.text;
            set => textComponent.text = value;
        }
        public void Init(string name, int width, int height, int fontSize, Color textColor, Color bgColor, Sprite bgSprite)
        {
            var img = gameObject.AddComponent<Image>();
            if (bgSprite)
            {
                img.sprite = bgSprite;
                if (bgSprite.border != Vector4.zero)
                    img.type = Image.Type.Sliced;
            }

            img.color = bgColor;
            var rtLabel = gameObject.GetComponent<RectTransform>();
            rtLabel.sizeDelta = new Vector2(width, height);
            var goText = new GameObject("Text", typeof(Text));
            goText.transform.SetParent(transform, false);
            var rtText = goText.GetComponent<RectTransform>();
            rtText.anchorMin = Vector2.zero;
            rtText.anchorMax = Vector2.one;
            rtText.sizeDelta = Vector2.zero;
            textComponent = goText.GetComponent<Text>();
            textComponent.text = name;
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.alignment = TextAnchor.MiddleLeft;
            textComponent.color = textColor;
            textComponent.fontSize = fontSize;
        }
    }
}