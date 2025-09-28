using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RInputField : InputField, IRComponent
    {
        private Action<RInputField> onUpdated;
        public void OnUpdate()
        {
            onUpdated?.Invoke(this);
        }

        public void Init(string name, UnityAction<string> onValueChanged, int width, int height, int fontSize,
            string defaultValue, Color textColor, Color bgColor, Sprite bgSprite, Action<RInputField> onUpdated = null)
        {
            this.onUpdated = onUpdated;
            var img = gameObject.AddComponent<Image>();
            targetGraphic = img;
            if (bgSprite)
            {
                img.sprite = bgSprite;
                if (bgSprite.border != Vector4.zero)
                    img.type = Image.Type.Sliced;
            }

            img.color = bgColor;
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            textComponent = txt;
            txt.transform.SetParent(transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = new Vector2(-10, 0);
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleLeft;
            txt.color = textColor;
            txt.fontSize = fontSize;
            var goPlaceholder = new GameObject("Placeholder", typeof(Text)).GetComponent<Text>();
            placeholder = goPlaceholder;
            goPlaceholder.transform.SetParent(transform, false);
            var rtPlaceholder = goPlaceholder.GetComponent<RectTransform>();
            rtPlaceholder.anchorMin = Vector2.zero;
            rtPlaceholder.anchorMax = Vector2.one;
            rtPlaceholder.sizeDelta = new Vector2(-10, 0);
            goPlaceholder.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            goPlaceholder.alignment = TextAnchor.MiddleLeft;
            goPlaceholder.color = Color.gray;
            goPlaceholder.fontSize = fontSize;
            goPlaceholder.text = $"{name}";
            text = defaultValue;
            this.onValueChanged.AddListener(onValueChanged);
        }
    }
}