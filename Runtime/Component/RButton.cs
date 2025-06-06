using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RButton : RComponent
    {
        public void Init(string name, UnityAction onClick,int width, int height, int fontSize,
             Color bgColor, Color textColor, Sprite bgSprite)
        {
            var button = gameObject.AddComponent<Button>();
            var img = button.targetGraphic.GetComponent<Image>();
            if (bgSprite)
            {
                img.sprite = bgSprite;
                if (bgSprite.border != Vector4.zero)
                    img.type = Image.Type.Sliced;
            }

            img.color = bgColor;
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            txt.transform.SetParent(button.transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = Vector2.zero;
            txt.text = name;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = textColor;
            txt.fontSize = fontSize;
            button.onClick.AddListener(onClick);
        }
    }
}