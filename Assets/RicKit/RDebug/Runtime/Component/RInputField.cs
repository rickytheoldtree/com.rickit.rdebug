using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RInputField : RComponent
    {
        public void Init(string name, UnityAction<string> onValueChanged, int width, int height, int fontSize,
            string defaultValue, Color textColor, Color bgColor, Sprite bgSprite)
        {
            var img = gameObject.AddComponent<Image>();
            var inputField = gameObject.AddComponent<InputField>();
            if (bgSprite)
            {
                img.sprite = bgSprite;
                if (bgSprite.border != Vector4.zero)
                    img.type = Image.Type.Sliced;
            }

            img.color = bgColor;
            inputField.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            inputField.textComponent = txt;
            txt.transform.SetParent(inputField.transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = new Vector2(-10, 0);
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleLeft;
            txt.color = textColor;
            txt.fontSize = fontSize;
            var placeholder = new GameObject("Placeholder", typeof(Text)).GetComponent<Text>();
            inputField.placeholder = placeholder;
            placeholder.transform.SetParent(inputField.transform, false);
            var rtPlaceholder = placeholder.GetComponent<RectTransform>();
            rtPlaceholder.anchorMin = Vector2.zero;
            rtPlaceholder.anchorMax = Vector2.one;
            rtPlaceholder.sizeDelta = new Vector2(-10, 0);
            placeholder.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            placeholder.alignment = TextAnchor.MiddleLeft;
            placeholder.color = Color.gray;
            placeholder.fontSize = fontSize;
            placeholder.text = $"{name}";
            inputField.text = defaultValue;
            inputField.onValueChanged.AddListener(onValueChanged);
        }
    }
}