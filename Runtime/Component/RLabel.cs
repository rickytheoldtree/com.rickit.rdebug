using UnityEngine;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RLabel : RComponent
    {
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
            var txt = goText.GetComponent<Text>();
            txt.text = name;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleLeft;
            txt.color = textColor;
            txt.fontSize = fontSize;
        }
    }
}